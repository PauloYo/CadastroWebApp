using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CadastroWebApp.Data;
using CadastroWebApp.Models;
using System.Text.Json;

namespace CadastroWebApp.Controllers
{
    public class PedidosController : Controller
    {
        private readonly PedidoRepository _pedidoRepo;
        private readonly ClienteRepository _clienteRepo;
        private readonly ProdutoRepository _produtoRepo;

        public PedidosController(PedidoRepository pedidoRepo, ClienteRepository clienteRepo, ProdutoRepository produtoRepo)
        {
            _pedidoRepo = pedidoRepo;
            _clienteRepo = clienteRepo;
            _produtoRepo = produtoRepo;
        }

        public IActionResult Index(int page = 1, string search = "")
        {
            const int pageSize = 10;
            var (pedidos, totalCount) = _pedidoRepo.GetPedidosPaginados(page, pageSize, search);
            
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.Search = search;
            ViewBag.TotalCount = totalCount;
            
            return View(pedidos);
        }

        public IActionResult Details(int id)
        {
            var pedido = _pedidoRepo.GetPedidoById(id);
            if (pedido == null)
                return NotFound();
                
            return View(pedido);
        }

        public IActionResult Create()
        {
            ViewBag.Clientes = GetClientesSelectList();
            ViewBag.Produtos = GetProdutosDisponiveis();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pedido pedido, string itensJson)
        {
            // Remove a validação do Cliente para usar ClienteId
            ModelState.Remove("Cliente");
            ModelState.Remove("Itens");
            
            if (ModelState.IsValid)
            {
                try
                {
                    // Processar itens do pedido
                    if (!string.IsNullOrEmpty(itensJson))
                    {
                        var itens = JsonSerializer.Deserialize<List<ItemPedidoDto>>(itensJson);
                        
                        foreach (var itemDto in itens ?? new List<ItemPedidoDto>())
                        {
                            var produto = _produtoRepo.GetProdutoById(itemDto.ProdutoId);
                            if (produto != null)
                            {
                                var item = new ItemPedido
                                {
                                    ProdutoId = itemDto.ProdutoId,
                                    Quantidade = itemDto.Quantidade,
                                    PrecoUnitario = produto.Preco,
                                    Observacao = itemDto.Observacoes
                                };
                                pedido.Itens.Add(item);
                            }
                        }
                    }
                    
                    _pedidoRepo.AddPedido(pedido);
                    TempData["Success"] = "Pedido cadastrado com sucesso!";
                    return RedirectToAction("Details", new { id = pedido.Id });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Erro ao cadastrar pedido: {ex.Message}");
                }
            }
            
            ViewBag.Clientes = GetClientesSelectList();
            ViewBag.Produtos = GetProdutosDisponiveis();
            return View(pedido);
        }

        public IActionResult Edit(int id)
        {
            var pedido = _pedidoRepo.GetPedidoById(id);
            if (pedido == null)
                return NotFound();
            
            ViewBag.Clientes = GetClientesSelectList();
            return View(pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Pedido pedido)
        {
            ModelState.Remove("Cliente");
            
            if (ModelState.IsValid)
            {
                try
                {
                    _pedidoRepo.UpdatePedido(pedido);
                    TempData["Success"] = "Pedido atualizado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Erro ao atualizar pedido: {ex.Message}");
                }
            }
            
            ViewBag.Clientes = GetClientesSelectList();
            return View(pedido);
        }

        public IActionResult Delete(int id)
        {
            var pedido = _pedidoRepo.GetPedidoById(id);
            if (pedido == null)
                return NotFound();
                
            return View(pedido);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _pedidoRepo.DeletePedido(id);
                TempData["Success"] = "Pedido excluído com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao excluir pedido: {ex.Message}";
            }
            
            return RedirectToAction("Index");
        }

        private SelectList GetClientesSelectList()
        {
            var clientes = _clienteRepo.GetClientes();
            return new SelectList(clientes, "Id", "Nome");
        }

        private List<Produto> GetProdutosDisponiveis()
        {
            return _produtoRepo.GetProdutos().Where(p => p.Disponivel).ToList();
        }

        [HttpPost]
        public IActionResult AdicionarItem(int pedidoId, int produtoId, int quantidade, string? observacao)
        {
            try
            {
                var produto = _produtoRepo.GetProdutoById(produtoId);
                if (produto == null)
                    return Json(new { success = false, message = "Produto não encontrado" });

                var item = new ItemPedido
                {
                    PedidoId = pedidoId,
                    ProdutoId = produtoId,
                    Quantidade = quantidade,
                    PrecoUnitario = produto.Preco,
                    Observacao = observacao
                };

                _pedidoRepo.AddItemPedido(item);
                return Json(new { success = true, message = "Item adicionado com sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult RemoverItem(int itemId)
        {
            try
            {
                _pedidoRepo.RemoveItemPedido(itemId);
                return Json(new { success = true, message = "Item removido com sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}