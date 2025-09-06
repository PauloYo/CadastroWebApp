using CadastroWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroWebApp.Data
{
    public class PedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Pedido> GetPedidos()
        {
            return _context.Pedidos.Include(p => p.Cliente).OrderByDescending(p => p.DataPedido).ToList();
        }

        public (List<Pedido> pedidos, int totalCount) GetPedidosPaginados(int page, int pageSize, string search = "")
        {
            var query = _context.Pedidos.Include(p => p.Cliente).AsQueryable();
            
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Cliente!.Nome.Contains(search) || p.Status.Contains(search));
            }

            var totalCount = query.Count();
            var pedidos = query
                .OrderByDescending(p => p.DataPedido)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (pedidos, totalCount);
        }

        public Pedido? GetPedidoById(int id)
        {
            return _context.Pedidos.Include(p => p.Cliente).FirstOrDefault(p => p.Id == id);
        }

        public void AddPedido(Pedido pedido)
        {
            pedido.DataPedido = DateTime.Now;
            pedido.DataCadastro = DateTime.Now;
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
        }

        public void UpdatePedido(Pedido pedido)
        {
            pedido.DataModificacao = DateTime.Now;
            _context.Pedidos.Update(pedido);
            _context.SaveChanges();
        }

        public void DeletePedido(int id)
        {
            var pedido = _context.Pedidos.Find(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                _context.SaveChanges();
            }
        }
    }
}
