using System.ComponentModel.DataAnnotations;

namespace CadastroWebApp.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;
        
        [EmailAddress(ErrorMessage = "Digite um email válido")]
        [StringLength(100, ErrorMessage = "O email deve ter no máximo 100 caracteres")]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }
        
        [StringLength(20, ErrorMessage = "O gênero deve ter no máximo 20 caracteres")]
        [Display(Name = "Gênero")]
        public string? Genero { get; set; }
        
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }
        
        [Display(Name = "Data de Modificação")]
        public DateTime? DataModificacao { get; set; }
        
        // Propriedades calculadas para exibição
        [Display(Name = "Idade")]
        public int? Idade
        {
            get
            {
                if (!DataNascimento.HasValue) return null;
                var hoje = DateTime.Today;
                var idade = hoje.Year - DataNascimento.Value.Year;
                if (DataNascimento.Value.Date > hoje.AddYears(-idade)) idade--;
                return idade;
            }
        }
    }
}