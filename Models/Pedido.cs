using System.ComponentModel.DataAnnotations;

namespace CadastroWebApp.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Selecione um cliente")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        
        [Required(ErrorMessage = "A data do pedido é obrigatória")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data do Pedido")]
        public DateTime DataPedido { get; set; } = DateTime.Now;
        
        [Required(ErrorMessage = "O valor total é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }
        
        [Required(ErrorMessage = "O status é obrigatório")]
        [StringLength(50, ErrorMessage = "O status deve ter no máximo 50 caracteres")]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Aguardando";
        
        [StringLength(500, ErrorMessage = "Observações do pedido")]
        [Display(Name = "Observações")]
        public string? Observacoes { get; set; }
        
        [Display(Name = "Número da Mesa")]
        [Range(1, 999, ErrorMessage = "Número da mesa deve estar entre 1 e 999")]
        public int? NumeroMesa { get; set; }
        
        [Display(Name = "Tipo de Entrega")]
        public string TipoEntrega { get; set; } = "Balcão";
        
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        
        [Display(Name = "Data de Modificação")]
        public DateTime? DataModificacao { get; set; }

        // Navegação
        public virtual Cliente Cliente { get; set; } = null!;
        public virtual ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

        // Propriedades calculadas
        public decimal ValorTotalCalculado => Itens?.Sum(i => i.Subtotal) ?? 0;
        public int QuantidadeItens => Itens?.Sum(i => i.Quantidade) ?? 0;
    }
    
    public static class StatusPedido
    {
        public const string Aguardando = "Aguardando";
        public const string Preparando = "Preparando";
        public const string Pronto = "Pronto";
        public const string Entregue = "Entregue";
        public const string Cancelado = "Cancelado";
        
        public static List<string> GetStatusList()
        {
            return new List<string> { Aguardando, Preparando, Pronto, Entregue, Cancelado };
        }
    }
    
    public static class TipoEntrega
    {
        public const string Balcao = "Balcão";
        public const string Mesa = "Mesa";
        public const string Delivery = "Delivery";
        
        public static List<string> GetTiposList()
        {
            return new List<string> { Balcao, Mesa, Delivery };
        }
    }
}
