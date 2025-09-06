using System.ComponentModel.DataAnnotations;

namespace CadastroWebApp.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Selecione um cliente")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        
        public Cliente? Cliente { get; set; }
        
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
        public string Status { get; set; } = "Pendente";
        
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }
        
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }
        
        [Display(Name = "Data de Modificação")]
        public DateTime? DataModificacao { get; set; }
    }
    
    public static class StatusPedido
    {
        public const string Pendente = "Pendente";
        public const string Processando = "Processando";
        public const string Enviado = "Enviado";
        public const string Entregue = "Entregue";
        public const string Cancelado = "Cancelado";
        
        public static List<string> GetStatusList()
        {
            return new List<string>
            {
                Pendente,
                Processando,
                Enviado,
                Entregue,
                Cancelado
            };
        }
    }
}