using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CadastroWebApp.Data;
using CadastroWebApp.Models;

namespace CadastroWebApp.Controllers
{
    public class PedidosController : Controller
    {
        private readonly PedidoRepository _pedidoRepo;
        private readonly ClienteRepository _clienteRepo;

        public PedidosController(PedidoRepository pedidoRepo, ClienteRepository clienteRepo)
        {
            _pedidoRepo = pedidoRepo;
            _clienteRepo = clienteRepo;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pedido pedido)
        {
            // Remove a validação do Cliente para usar ClienteId
            ModelState.Remove("Cliente");
            
            if (ModelState.IsValid)
            {
                try
                {
                    _pedidoRepo.AddPedido(pedido);
                    TempData["Success"] = "Pedido cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Erro ao cadastrar pedido: {ex.Message}");
                }
            }
            
            ViewBag.Clientes = GetClientesSelectList();
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
    }
}