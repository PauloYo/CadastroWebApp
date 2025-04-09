using Microsoft.AspNetCore.Mvc;
using CadastroWebApp.Data;
using CadastroWebApp.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace CadastroWebApp.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClienteRepository _clienteRepo;

        public ClientesController(IConfiguration configuration)
        {
            _clienteRepo = new ClienteRepository(configuration.GetConnectionString("DefaultConnection"));
        }

        public IActionResult Index()
        {
            List<Cliente> clientes = _clienteRepo.GetClientes();
            return View(clientes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteRepo.AddCliente(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        public IActionResult Delete(int id)
        {
            _clienteRepo.DeleteCliente(id);
            return RedirectToAction("Index");
        }
    }
}