using System.ComponentModel.DataAnnotations;

namespace CadastroWebApp.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }

        [Required]
        public int PedidoId { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória")]
        [Range(1, 99, ErrorMessage = "A quantidade deve estar entre 1 e 99")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O preço unitário é obrigatório")]
        [Range(0.01, 9999.99, ErrorMessage = "O preço deve estar entre R$ 0,01 e R$ 9.999,99")]
        public decimal PrecoUnitario { get; set; }

        public decimal Subtotal => Quantidade * PrecoUnitario;

        [StringLength(200, ErrorMessage = "A observação deve ter no máximo 200 caracteres")]
        public string? Observacao { get; set; }

        // Navegação
        public virtual Pedido Pedido { get; set; } = null!;
        public virtual Produto Produto { get; set; } = null!;
    }
}
