using System.Data;
using Npgsql;

public class OrderService
{
    const string connectionString = "Host=localhost;Database=postgres;Username=postgres;Password=sherowv77";

    public void GetOrderCountByCustomer()
    {
        using var connection = new NpgsqlConnection(connectionString);
        using var adapter = new NpgsqlDataAdapter(
            "SELECT c.id, c.first_name, c.last_name, COUNT(o.id) AS order_count " +
            "FROM customers c LEFT JOIN orders o ON c.id = o.customer_id " +
            "GROUP BY c.id, c.first_name, c.last_name", connection);
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

    public void GetCustomerWithMaxOrders()
    {
        using var connection = new NpgsqlConnection(connectionString);
        using var adapter = new NpgsqlDataAdapter(
            "SELECT c.id, c.first_name, c.last_name, COUNT(o.id) AS order_count " +
            "FROM customers c JOIN orders o ON c.id = o.customer_id " +
            "GROUP BY c.id, c.first_name, c.last_name " +
            "ORDER BY order_count DESC LIMIT 1", connection);
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

    public void GetOrdersAboveAverageTotalPrice()
    {
        using var connection = new NpgsqlConnection(connectionString);
        using var adapter = new NpgsqlDataAdapter(
            "SELECT * FROM orders WHERE total_price > (SELECT AVG(total_price) FROM orders)", connection);
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

    public void GetNewOrdersSortedByDate()
    {
        using var connection = new NpgsqlConnection(connectionString);
        using var adapter = new NpgsqlDataAdapter(
            "SELECT * FROM orders WHERE status = 'новый' ORDER BY order_date", connection);
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
