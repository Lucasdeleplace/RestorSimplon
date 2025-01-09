public class Order
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public Clients Client { get; set; }
    public int ItemId { get; set; }
    public Items Item { get; set; }
    public int Quantity { get; set; }
    public double Total { get; set; }
    public DateTime Date { get; set; }
    public bool Status { get; set; }

}

