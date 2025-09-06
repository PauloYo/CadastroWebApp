using Microsoft.AspNetCore.Mvc;
using CadastroWebApp.Data;
using CadastroWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace CadastroWebApp.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClienteRepository _clienteRepo;

        public ClientesController(ClienteRepository clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        public IActionResult Index(int page = 1, string search = "")
        {
            const int pageSize = 10;
            var (clientes, totalCount) = _clienteRepo.GetClientesPaginados(page, pageSize, search);
            
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.Search = search;
            ViewBag.TotalCount = totalCount;
            
            return View(clientes);
        }

        public IActionResult Details(int id)
        {
            var cliente = _clienteRepo.GetClienteById(id);
            if (cliente == null)
                return NotFound();
                
            return View(cliente);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _clienteRepo.AddCliente(cliente);
                    TempData["Success"] = "Cliente cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Erro ao cadastrar cliente: {ex.Message}");
                }
            }
            return View(cliente);
        }

        public IActionResult Edit(int id)
        {
            var cliente = _clienteRepo.GetClienteById(id);
            if (cliente == null)
                return NotFound();
                
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _clienteRepo.UpdateCliente(cliente);
                    TempData["Success"] = "Cliente atualizado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Erro ao atualizar cliente: {ex.Message}");
                }
            }
            return View(cliente);
        }

        public IActionResult Delete(int id)
        {
            var cliente = _clienteRepo.GetClienteById(id);
            if (cliente == null)
                return NotFound();
                
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _clienteRepo.DeleteCliente(id);
                TempData["Success"] = "Cliente excluído com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao excluir cliente: {ex.Message}";
            }
            
            return RedirectToAction("Index");
        }
    }
}