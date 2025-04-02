using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient; 
using CadastroWebApp.Models;

namespace CadastroWebApp.Data
{
    public class ClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Cliente> GetClientes()
        {
            var clientes = new List<Cliente>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Cliente ORDER BY Nome";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(new Cliente
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Email = reader.IsDBNull(2) ? null : reader.GetString(2),
                            DataNascimento = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                            Genero = reader.IsDBNull(4) ? null : reader.GetString(4),
                            DataCadastro = reader.GetDateTime(5)
                        });
                    }
                }
            }

            return clientes;
        }

        public void AddCliente(Cliente cliente)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Cliente (Nome, Email, DataNascimento, Genero) VALUES (@Nome, @Email, @DataNascimento, @Genero)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@Email", (object?)cliente.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DataNascimento", (object?)cliente.DataNascimento ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Genero", (object?)cliente.Genero ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCliente(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Cliente WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}