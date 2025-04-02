namespace CadastroWebApp.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Genero { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}