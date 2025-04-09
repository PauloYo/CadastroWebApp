using Microsoft.Data.SqlClient; 
using CadastroWebApp.Models;
using CadastroWebApp.Data;

namespace CadastroWebApp.Data
{
    public class PedidoRepository
    {
        private readonly string _connectionString;
        private readonly ClienteRepository _clienteRepo;

        public PedidoRepository(string connectionString, ClienteRepository clienteRepo)
        {
            _connectionString = connectionString;
            _clienteRepo = clienteRepo;
        }

        public List<Pedido> GetPedidos()
        {
            var pedidos = new List<Pedido>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Pedido ORDER BY Nome";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pedidos.Add(new Pedido
                        {
                            Id = reader.GetInt32(0),
                            Cliente = _clienteRepo.GetClienteById(reader.GetInt32(1)), // Assuming ClienteId is the second column
                            DataPedido = reader.GetDateTime(2),
                            ValorTotal = reader.GetDecimal(3),
                            Status = reader.GetString(4),
                            Descricao = reader.IsDBNull(5) ? null : reader.GetString(5)
                        });
                    }
                }
            }

            return pedidos;
        }

        public Pedido GetPedidoById(int id)
        {
            Pedido pedido = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Pedido WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pedido = new Pedido
                            {
                                Id = reader.GetInt32(0),
                                Cliente = _clienteRepo.GetClienteById(reader.GetInt32(1)), // Assuming ClienteId is the second column
                                DataPedido = reader.GetDateTime(2),
                                ValorTotal = reader.GetDecimal(3),
                                Status = reader.GetString(4),
                                Descricao = reader.IsDBNull(5) ? null : reader.GetString(5)
                            };
                        }
                    }
                }
            }

            return pedido;
        }

        public void AddPedido(Pedido pedido)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Pedido (ClienteId, DataPedido, ValorTotal, Status, Descricao) VALUES (@ClienteId, @DataPedido, @ValorTotal, @Status, @Descricao)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClienteId", pedido.Cliente.Id);
                    cmd.Parameters.AddWithValue("@DataPedido", pedido.DataPedido);
                    cmd.Parameters.AddWithValue("@ValorTotal", pedido.ValorTotal);
                    cmd.Parameters.AddWithValue("@Status", pedido.Status);
                    cmd.Parameters.AddWithValue("@Descricao", (object)pedido.Descricao ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeletePedido(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Pedido WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
