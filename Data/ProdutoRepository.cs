using CadastroWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroWebApp.Data
{
    public class ProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Produto> GetProdutos()
        {
            return _context.Produtos
                .Where(p => p.Disponivel)
                .OrderBy(p => p.Categoria)
                .ThenBy(p => p.Nome)
                .ToList();
        }

        public (List<Produto> produtos, int totalCount) GetProdutosPaginados(int page, int pageSize, string search = "", string categoria = "")
        {
            var query = _context.Produtos.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Nome.Contains(search) || p.Descricao.Contains(search));
            }

            if (!string.IsNullOrEmpty(categoria))
            {
                query = query.Where(p => p.Categoria == categoria);
            }

            var totalCount = query.Count();
            var produtos = query
                .OrderBy(p => p.Categoria)
                .ThenBy(p => p.Nome)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (produtos, totalCount);
        }

        public Produto? GetProdutoById(int id)
        {
            return _context.Produtos.FirstOrDefault(p => p.Id == id);
        }

        public List<string> GetCategorias()
        {
            return _context.Produtos
                .Where(p => p.Disponivel)
                .Select(p => p.Categoria)
                .Distinct()
                .OrderBy(c => c)
                .ToList();
        }

        public void AddProduto(Produto produto)
        {
            produto.DataCadastro = DateTime.UtcNow;
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        public void UpdateProduto(Produto produto)
        {
            produto.DataModificacao = DateTime.UtcNow;
            _context.Produtos.Update(produto);
            _context.SaveChanges();
        }

        public void DeleteProduto(int id)
        {
            var produto = GetProdutoById(id);
            if (produto != null)
            {
                // Soft delete - marca como indisponível
                produto.Disponivel = false;
                produto.DataModificacao = DateTime.UtcNow;
                _context.SaveChanges();
            }
        }

        public List<Produto> GetProdutosPorCategoria(string categoria)
        {
            return _context.Produtos
                .Where(p => p.Categoria == categoria && p.Disponivel)
                .OrderBy(p => p.Nome)
                .ToList();
        }
    }
}
