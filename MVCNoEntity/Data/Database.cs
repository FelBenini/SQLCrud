using MVCNoEntity.Models;
using System.Data.SqlClient;

namespace MVCNoEntity.Data;

public class Database
{
    static IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
    static readonly string connectionString = configuration.GetConnectionString("SQL_CRUD");

    public static List<Produto> getAllProducts()
    {
        List<Produto> produtos = new List<Produto>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Produtos";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : "Sem descrição";
                    Produto product = new Produto();
                    product.Id = (int)reader["Id"];
                    product.Name = (string)reader["Nome"];
                    product.Price = Convert.ToSingle(reader["Price"]);
                    product.Description = description;
                    produtos.Add(product);
                }
                reader.Close();
            }
        }
        return produtos;
    }

    public static void AddProduct(Produto produto)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Produtos(Nome, Price, Description) VALUES(@Name, @Price, @Description)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", produto.Name);
                command.Parameters.AddWithValue("@Price", produto.Price);
                command.Parameters.AddWithValue("@Description", produto.Description);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public static void UpdateProduct(Produto produto)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Produtos SET Nome=@Nome, Price=@Price, Description=@Description WHERE Id=@Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nome", produto.Name);
                command.Parameters.AddWithValue("@Price", produto.Price);
                command.Parameters.AddWithValue("@Description", produto.Description);
                command.Parameters.AddWithValue("@Id", produto.Id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public static void DeleteProduct(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Produtos WHERE Id=@Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }

    public static Produto GetSingleProdut(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Produtos WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : "Sem descrição";
                    Produto product = new Produto();
                    product.Id = (int)reader["Id"];
                    product.Name = (string)reader["Nome"];
                    product.Price = Convert.ToSingle(reader["Price"]);
                    product.Description = description;
                    return product;
                }
                reader.Close();
            }
            return null;
        }
    }

    public static void Excluir(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Produtos WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
