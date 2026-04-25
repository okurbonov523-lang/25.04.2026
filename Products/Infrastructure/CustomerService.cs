using System.Data;
using Npgsql;

public class CustomerService
{
    const string connectionString = "Host=localhost;Database=postgres;Username=postgres;Password=sherowv77";

    public void GetCustomersRegisteredBetween(DateTime startDate, DateTime endDate)
    {
        using var connection = new NpgsqlConnection(connectionString);
        using var adapter = new NpgsqlDataAdapter(
            "SELECT * FROM customers WHERE registration_date BETWEEN @start AND @end ORDER BY registration_date", connection);
        adapter.SelectCommand.Parameters.AddWithValue("@start", startDate);
        adapter.SelectCommand.Parameters.AddWithValue("@end", endDate);
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