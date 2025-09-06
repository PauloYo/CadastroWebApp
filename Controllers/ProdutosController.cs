using Microsoft.AspNetCore.Mvc;
using CadastroWebApp.Data;
using CadastroWebApp.Models;

namespace CadastroWebApp.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ProdutoRepository _produtoRepo;

        public ProdutosController(ProdutoRepository produtoRepo)
        {
            _produtoRepo = produtoRepo;
        }

        public IActionResult Index(int page = 1, string search = "", string categoria = "")
        {
            const int pageSize = 12;
            var (produtos, totalCount) = _produtoRepo.GetProdutosPaginados(page, pageSize, search, categoria);
            
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.Search = search;
            ViewBag.Categoria = categoria;
            ViewBag.TotalCount = totalCount;
            ViewBag.Categorias = _produtoRepo.GetCategorias();
            
            return View(produtos);
        }

        public IActionResult Details(int id)
        {
            var produto = _produtoRepo.GetProdutoById(id);
            if (produto == null)
                return NotFound();
                
            return View(produto);
        }

        public IActionResult Create()
        {
            ViewBag.Categorias = GetCategoriasSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _produtoRepo.AddProduto(produto);
                    TempData["Success"] = "Produto cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Erro ao cadastrar produto: {ex.Message}");
                }
            }
            
            ViewBag.Categorias = GetCategoriasSelectList();
            return View(produto);
        }

        public IActionResult Edit(int id)
        {
            var produto = _produtoRepo.GetProdutoById(id);
            if (produto == null)
                return NotFound();
            
            ViewBag.Categorias = GetCategoriasSelectList();
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _produtoRepo.UpdateProduto(produto);
                    TempData["Success"] = "Produto atualizado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Erro ao atualizar produto: {ex.Message}");
                }
            }
            
            ViewBag.Categorias = GetCategoriasSelectList();
            return View(produto);
        }

        public IActionResult Delete(int id)
        {
            var produto = _produtoRepo.GetProdutoById(id);
            if (produto == null)
                return NotFound();
                
            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _produtoRepo.DeleteProduto(id);
                TempData["Success"] = "Produto removido do cardápio com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao remover produto: {ex.Message}";
            }
            
            return RedirectToAction("Index");
        }

        public IActionResult Cardapio(string categoria = "")
        {
            List<Produto> produtos;
            
            if (string.IsNullOrEmpty(categoria))
            {
                produtos = _produtoRepo.GetProdutos();
            }
            else
            {
                produtos = _produtoRepo.GetProdutosPorCategoria(categoria);
            }
            
            ViewBag.Categorias = _produtoRepo.GetCategorias();
            ViewBag.CategoriaAtual = categoria;
            
            return View(produtos);
        }

        private List<string> GetCategoriasSelectList()
        {
            return new List<string>
            {
                "Lanches",
                "Bebidas",
                "Sobremesas",
                "Pratos Executivos",
                "Saladas",
                "Petiscos",
                "Sucos",
                "Cafés",
                "Doces",
                "Salgados"
            };
        }
    }
}
