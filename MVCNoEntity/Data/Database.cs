using MVCNoEntity.Models;
using System.Data.SqlClient;

namespace MVCNoEntity.Data;

public class Database
{
    static IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
    private static readonly string connectionString = configuration.GetConnectionString("SQL_CRUD");

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
                    Produto product = new Produto();
                    product.Id = (int)reader["Id"];
                    product.Name = (string)reader["Nome"];
                    product.Price = Convert.ToSingle(reader["Price"]);
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
            string query = "INSERT INTO Produtos(Nome, Price) VALUES(@Name, @Price)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", produto.Name);
                command.Parameters.AddWithValue("@Price", produto.Price);
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
            string query = "SELECT Id, Nome, Price FROM Produtos WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Produto product = new Produto();
                    product.Id = (int)reader["Id"];
                    product.Name = (string)reader["Nome"];
                    product.Price = Convert.ToSingle(reader["Price"]);
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
