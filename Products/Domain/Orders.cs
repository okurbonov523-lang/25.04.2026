namespace Domain;

public class Orders
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public DateTime OrderDate { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; }
}
