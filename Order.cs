public class Order
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public double Total { get; set; }
    //public bool Status { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}


