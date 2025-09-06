using System.ComponentModel.DataAnnotations;

namespace CadastroWebApp.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório")]
        [Range(0.01, 9999.99, ErrorMessage = "O preço deve estar entre R$ 0,01 e R$ 9.999,99")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória")]
        [StringLength(50, ErrorMessage = "A categoria deve ter no máximo 50 caracteres")]
        public string Categoria { get; set; } = string.Empty;

        public bool Disponivel { get; set; } = true;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public DateTime? DataModificacao { get; set; }

        // Navegação
        public virtual ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
    }
}
