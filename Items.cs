using System.Text.Json.Serialization;

public class Items
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public Categories Category { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}
