using System.Data;
using Domain;
using Npgsql;

namespace Infrastructure;

public class ProductService
{
    const string connectionString = "Host = localhost; Database = postgres; Username = postgres; Password = sherowv77";

    public List<Products> GetAllProdcust()
    {
        var products = new List<Products>();
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        var sql = "Select * from Products";

        using var command = new NpgsqlCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var pr = new Products
            {
                Name = reader.GetString(0),
                Description = reader.GetString(1),
                Price = reader.GetDecimal(2),
                Category = reader.GetString(3),
                StockQuantity = reader.GetInt32(4),
                Manufacturer = reader.GetString(5)
            };
            products.Add(pr);
        }
        return products;
    }

    public void GetProductsByCategory(string category)
    {
        using var connection = new NpgsqlConnection(connectionString);
        using var adapter = new NpgsqlDataAdapter("SELECT * FROM products WHERE category = @category", connection);
        adapter.SelectCommand.Parameters.AddWithValue("@category", category);
        var ds = new DataSet();
        adapter.Fill(ds);
        DataTable table = ds.Tables[0];
        foreach (DataColumn column in table.Columns)
        {
            System.Console.WriteLine($"{column.ColumnName}");
        }
        System.Console.WriteLine();
        foreach (DataRow row in table.Rows)
        {
            foreach (var item in row.ItemArray)
            {
                System.Console.WriteLine(item);
            }
            System.Console.WriteLine();
        }
    }


    public void GetUniqueManufacturers()
    {
        using var connection = new NpgsqlConnection(connectionString);
        using var adapter = new NpgsqlDataAdapter("SELECT DISTINCT manufacturer FROM products", connection);
        var ds = new DataSet();
        adapter.Fill(ds);
        DataTable table = ds.Tables[0];
        foreach (DataColumn column in table.Columns)
        {
            System.Console.WriteLine($"{column.ColumnName}");
        }
        System.Console.WriteLine();
        foreach (DataRow row in table.Rows)
        {
            foreach (var item in row.ItemArray)
            {
                System.Console.WriteLine(item);
            }
            System.Console.WriteLine();
        }
    }


}
