﻿using System.Text.Json.Serialization;

public class Items
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
}
//public class ItemsDTO
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }
//    public double Price { get; set; }
//    public int CategoryId { get; set; }
//}