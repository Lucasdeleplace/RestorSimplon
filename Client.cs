public class Clients
{
    public int Id { get; set; }
    public string? First_Name { get; set; }
    public string? Last_Name { get; set; }
    public string? Address { get; set; }
    public string? Phone_Number { get; set; }
    public ICollection<Order> Orders { get; set; }
}
//public class ClientsDTO
//{
//    public int Id { get; set; }
//    public string? First_Name { get; set; }
//    public string? Last_Name { get; set; }
//    public string? Address { get; set; }
//    public string? Phone_Number { get; set; }
//}