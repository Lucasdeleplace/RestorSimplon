using System.Text.Json.Serialization;

public class Categories
{
    public int Id { get; set; }
    public string? Category_Name { get; set; }
    public ICollection<Items> Items { get; set; }
}
//public class CategoriesDTO
//{
//    public int Id { get; set; }
//    public string? Category_Name { get; set; }
//}