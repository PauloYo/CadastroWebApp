using CadastroWebApp.Models;

namespace CadastroWebApp.Data
{
    public class ClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Cliente> GetClientes()
        {
            return _context.Clientes.OrderBy(c => c.Nome).ToList();
        }

        public (List<Cliente> clientes, int totalCount) GetClientesPaginados(int page, int pageSize, string search = "")
        {
            var query = _context.Clientes.AsQueryable();
            
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Nome.Contains(search) || (c.Email != null && c.Email.Contains(search)));
            }

            var totalCount = query.Count();
            var clientes = query
                .OrderBy(c => c.Nome)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (clientes, totalCount);
        }

        public Cliente? GetClienteById(int id)
        {
            return _context.Clientes.Find(id);
        }

        public void AddCliente(Cliente cliente)
        {
            cliente.DataCadastro = DateTime.Now;
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }

        public void UpdateCliente(Cliente cliente)
        {
            cliente.DataModificacao = DateTime.Now;
            _context.Clientes.Update(cliente);
            _context.SaveChanges();
        }

        public void DeleteCliente(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
            }
        }
    }
}
