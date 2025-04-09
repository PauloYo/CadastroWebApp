using Microsoft.AspNetCore.Mvc;
using CadastroWebApp.Data;
using CadastroWebApp.Models;

namespace CadastroWebApp.Controllers
{
    public class PedidoController : Controller
    {
        private readonly PedidoRepository _pedidoRepo;
        private readonly ClienteRepository _clienteRepo;

        public PedidoController(PedidoRepository pedidoRepo, ClienteRepository clienteRepo)
        {
            _pedidoRepo = pedidoRepo;
            _clienteRepo = clienteRepo;
        }
        
        public IActionResult Index()
        {
            List<Pedido> pedidos = _pedidoRepo.GetPedidos();
            return View(pedidos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _pedidoRepo.AddPedido(pedido);
                return RedirectToAction("Index");
            }
            return View(pedido);
        }

        public IActionResult Delete(int id)
        {
            _pedidoRepo.DeletePedido(id);
            return RedirectToAction("Index");
        }
    }
}