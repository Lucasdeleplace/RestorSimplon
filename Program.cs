using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ClientsDb>(opt => opt.UseSqlite("Data Source=RestoSimplon.db"));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RestorSimplonAPI",
        Version = "v1",
        Description = "Une API pour g�rer des commandes",
        Contact = new OpenApiContact
        {
            Name = "Legroupe",
            Email = "legroupejla@example.com",
            Url = new Uri("https://main.d1w2g83ft87ss3.amplifyapp.com/")
        }
    });

    c.EnableAnnotations();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
        c.RoutePrefix = "";
    });
}

app.UseRouting();
app.MapControllers();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

// ------ Route Client ------ //
RouteGroupBuilder clients = app.MapGroup("/clients").WithTags("Clients");
RouteGroupBuilder categories = app.MapGroup("/categories").WithTags("Categories");
RouteGroupBuilder items = app.MapGroup("/items").WithTags("Items");
RouteGroupBuilder orders = app.MapGroup("/orders").WithTags("Orders");
//RouteGroupBuilder orderItems = app.MapGroup("/orderItems");

clients.MapGet("/", GetAllClients)
   .WithMetadata(new SwaggerOperationAttribute(
        summary: "R�cup�re tous les �l�ments clients",
        description: "Renvois une liste de tous les �l�ments clients."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'�l�ments clients trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun �l�ment clients non trouv�s"));
clients.MapGet("/{id}", GetClientbyId)
   .WithMetadata(new SwaggerOperationAttribute(
        summary: "R�cup�re tous les �l�ments d'un client via son ID",
        description: "Renvois une liste de tout les �l�ments d'un client."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'�l�ments client trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun �l�ment client non trouv�s"));

clients.MapPost("/", CreateClients)
       .WithMetadata(new SwaggerOperationAttribute(
        summary: "Cr�er tous les �l�ments client",
        description: "Cr�er une liste de tous les �l�ments client."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Cr�ation d'�l�ments clients trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun cr�ation clients non trouv�s"));
clients.MapPut("/{id}", UpdateClients)
     .WithMetadata(new SwaggerOperationAttribute(
        summary: "mettre � jour tous les �l�ments client",
        description: "mets � jour une liste de tous les �l�ments client."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Mise � jour d'�l�ments clients trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun mise � jour clients non trouv�s"));
clients.MapDelete("/{id}", DeleteClients)
.WithMetadata(new SwaggerOperationAttribute(
        summary: "supprimer tous les �l�ments client",
        description: "Supprimer une liste de tous les �l�ments client."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Suppression d'�l�ments clients trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Supression clients non trouv�s"));
clients.MapGet("/{id}/with-order", GetClientWithOrder)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "R�cup�re tous les �l�ments client avec ses commandes",
        description: "Recupere une liste de tous les �l�ments client avec leurs commandes."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'�l�ments clients avec leurs commandes trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun clients avec leurs commandes non trouv�s"));

// ------ Route Category ------ //
categories.MapGet("/", GetAllCategories)
.WithMetadata(new SwaggerOperationAttribute(
        summary: "R�cup�re tous les �l�ments cat�gories",
        description: "Renvois une liste de tous les �l�ments cat�gories."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'�l�ments cat�gories trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun �l�ment cat�gories non trouv�s"));
categories.MapGet("/{id}", GetCategoriesbyId)
.WithMetadata(new SwaggerOperationAttribute(
        summary: "R�cup�re tous les �l�ments d'un cat�gorie via son ID",
        description: "Renvois une liste de tout les �l�ments d'un cat�gorie."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'�l�ments cat�gorie trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun �l�ment cat�gorie non trouv�s"));
categories.MapPost("/", CreateCategories)
 .WithMetadata(new SwaggerOperationAttribute(
        summary: "Cr�er tous les �l�ments cat�gories",
        description: "Cr�er une liste de tous les �l�ments cat�gories."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Cr�ation d'�l�ments cat�gories trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun cr�ation cat�gories non trouv�s"));
categories.MapPut("/{id}", UpdateCategories)
 .WithMetadata(new SwaggerOperationAttribute(
        summary: "mettre � jour tous les �l�ments cat�gories",
        description: "mets � jour une liste de tous les �l�ments cat�gories."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Mise � jour d'�l�ments cat�gories trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Mise � jour cat�gories non trouv�s"));
categories.MapDelete("/{id}", DeleteCategories)
    .WithMetadata(new SwaggerOperationAttribute(
    summary: "supprimer tous les �l�ments cat�gories",
        description: "Supprimer une liste de tous les �l�ments cat�gories."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Suppression d'�l�ments cat�gories trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Supression cat�gories non trouv�s"));
categories.MapGet("/{id}/with-items", GetCategoryWithItems)
       .WithMetadata(new SwaggerOperationAttribute(
        summary: "R�cup�re tous les �l�ments cat�gories avec ses items",
        description: "R�cup�re une liste de tous les �l�ments cat�gories avec leurs items."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'�l�ments cat�gories avec leurs items trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucune cat�gories avec leurs items non trouv�s"));

// ------ Route Items ------ //
items.MapGet("/", GetAllItems)
.WithMetadata(new SwaggerOperationAttribute(
        summary: "R�cup�re tous les �l�ments items",
        description: "Renvois une liste de tous les �l�ments items."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'�l�ment items trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun �l�ment items non trouv�s"));
items.MapGet("/{id}", GetItemsbyId)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "R�cup�re tous les �l�ments d'un item via son ID",
        description: "Renvois une liste de tout les �l�ments d'un item."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'�l�ments item trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun �l�ment item non trouv�s"));
items.MapPost("/", CreateItems)
 .WithMetadata(new SwaggerOperationAttribute(
        summary: "Cr�er tous les �l�ments items",
        description: "Cr�er une liste de tous les �l�ments items."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Cr�ation d'�l�ments items trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun cr�ation items non trouv�s"));
items.MapPut("/{id}", UpdateItems)
     .WithMetadata(new SwaggerOperationAttribute(
        summary: "mettre � jour tous les �l�ments items",
        description: "mets � jour une liste de tous les �l�ments item."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Mise � jour d'�l�ments item trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Mise � jour item non trouv�s"));
items.MapDelete("/{id}", DeleteItems)
     .WithMetadata(new SwaggerOperationAttribute(
    summary: "supprimer tous les �l�ments item",
        description: "Supprimer une liste de tous les �l�ments item."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Suppression d'�l�ments item trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Supression item non trouv�s"));
items.MapGet("/{id}/with-category", GetItemWithCategory)
         .WithMetadata(new SwaggerOperationAttribute(
        summary: "R�cup�re tous les �l�ments items avec ses cat�gories",
        description: "R�cup�re une liste de tous les �l�ments items avec leurs cat�gories."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'�l�ments items avec leurs cat�gories trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucune items avec leurs cat�gories non trouv�s"));

// ------ Route Order ------ //   
orders.MapGet("/", GetAllOrder)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "R�cup�re tous les �l�ments commandes",
        description: "Renvois une liste de tous les �l�ments commandes."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'�l�ment commandes trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun �l�ment commandes non trouv�s"));
orders.MapGet("/{id}", GetOrderbyId)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "R�cup�re tous les �l�ments d'une commande via son ID",
        description: "Renvois une liste de tout les �l�ments d'une commande."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'�l�ments commande trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun �l�ment commande non trouv�s"));
orders.MapPost("/", CreateOrder)
     .WithMetadata(new SwaggerOperationAttribute(
        summary: "Cr�er tous les commande items",
        description: "Cr�er une liste de tous les commande items."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Cr�ation d'�l�ments cat�gorie trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun cr�ation cat�gorie non trouv�s"));
orders.MapPut("/{id}", UpdateOrder)
     .WithMetadata(new SwaggerOperationAttribute(
        summary: "mettre � jour tous les �l�ments commande",
        description: "mets � jour une liste de tous les �l�ments commande."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Mise � jour d'�l�ments commande trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Mise � jour commande non trouv�s"));
orders.MapDelete("/{id}", DeleteOrder)
   .WithMetadata(new SwaggerOperationAttribute(
    summary: "supprimer tous les �l�ments commandes",
        description: "Supprimer une liste de tous les �l�ments commandes."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Suppression d'�l�ments commandes trouv�s"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Supression commandes non trouv�s"));
//// ------ Route OrderItem ------ //
//orderItems.MapGet("/", GetAllOrderItems);
//orderItems.MapGet("/{orderId}/{itemId}", GetOrderItemsbyId);
//orderItems.MapPost("/", CreateOrderItems);
//orderItems.MapPut("/{id}", UpdateOrderItems);
//orderItems.MapDelete("/{id}", DeleteOrderItems);

app.Run();

static async Task<IResult> GetAllClients(ClientsDb db)
{
    return TypedResults.Ok(await db.Clients.ToArrayAsync());
}

static async Task<IResult> GetClientbyId(int id, ClientsDb db)
{
    return await db.Clients.FindAsync(id)
        is Clients clients
            ? TypedResults.Ok(clients)
            : TypedResults.NotFound();
}

static async Task<IResult> CreateClients(Clients clients, ClientsDb db)
{
    db.Clients.Add(clients);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/clients/{clients.Id}", clients);
}

static async Task<IResult> UpdateClients(int id, Clients inputClients, ClientsDb db)
{
    var client = await db.Clients.FindAsync(id);
    if (client is null) return TypedResults.NotFound();

    client.First_Name = inputClients.First_Name;
    client.Last_Name = inputClients.Last_Name;
    client.Address = inputClients.Address;
    client.Phone_Number = inputClients.Phone_Number;

    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}

static async Task<IResult> DeleteClients(int id, ClientsDb db)
{
    if (await db.Clients.FindAsync(id) is Clients clients)
    {
        db.Clients.Remove(clients);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    return TypedResults.NotFound();
}

static async Task<IResult> GetClientWithOrder(int id, ClientsDb db)
{
    var orders = await db.Orders.Where(o => o.ClientId == id).ToListAsync();
    var client = await db.Clients.FindAsync(id);
    if (client is null) return TypedResults.NotFound();
    client.Orders = orders;
    var itemOrder = await db.OrderItems.ToListAsync();
    foreach (var order in orders)
    {
        order.OrderItems = itemOrder.Where(oi => oi.OrderId == order.Id).ToList();
    }
    var items = new List<Items>();
    foreach (var order in orders)
    {
        foreach (var orderItem in order.OrderItems)
        {
            var item = await db.Items.FindAsync(orderItem.ItemId);
            if (item != null)
            {
                items.Add(item);
            }
        }
    }
    return TypedResults.Ok(new { client, items });
}

// Function Categories
static async Task<IResult> GetAllCategories(ClientsDb db)
{
    return TypedResults.Ok(await db.Categories.ToArrayAsync());
}

static async Task<IResult> GetCategoriesbyId(int id, ClientsDb db)
{
    return await db.Categories.FindAsync(id)
        is Categories categories
            ? TypedResults.Ok(categories)
            : TypedResults.NotFound();
}

static async Task<IResult> GetCategoryWithItems(int id, ClientsDb db)
{
    //var category = await db.Categories.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == id);
    //return category is not null ? TypedResults.Ok(category) : TypedResults.NotFound();
    var items = await db.Items.Where(i => i.CategoryId == id).ToListAsync();
    return TypedResults.Ok(items);
}

static async Task<IResult> CreateCategories(Categories categories, ClientsDb db)
{
    db.Categories.Add(categories);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/categories/{categories.Id}", categories);
}

static async Task<IResult> UpdateCategories(int id, Categories inputCategories, ClientsDb db)
{
    var categorie = await db.Categories.FindAsync(id);
    if (categorie is null) return TypedResults.NotFound();

    categorie.Category_Name = inputCategories.Category_Name;
    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}

static async Task<IResult> DeleteCategories(int id, ClientsDb db)
{
    if (await db.Categories.FindAsync(id) is Categories categories)
    {
        db.Categories.Remove(categories);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    return TypedResults.NotFound();
}

// ------ Route Items ------ //
static async Task<IResult> GetAllItems(ClientsDb db)
{
    return TypedResults.Ok(await db.Items.ToArrayAsync());
}

static async Task<IResult> GetItemsbyId(int id, ClientsDb db)
{
    return await db.Items.FindAsync(id)
        is Items items
        ? TypedResults.Ok(items)
        : TypedResults.NotFound();
}

//// ------ Route OrderItem ------ //

//static async Task<IResult> GetAllOrderItems(ClientsDb db)
//{
//    return TypedResults.Ok(await db.OrderItems.ToArrayAsync());
//}

//static async Task<IResult> GetOrderItemsbyId(int orderId, int itemId, ClientsDb db)
//{
//    return await db.OrderItems.FindAsync(orderId, itemId)
//        is OrderItem orderItems
//        ? TypedResults.Ok(orderItems)
//        : TypedResults.NotFound();
//}


//static async Task<IResult> CreateOrderItems(OrderItem orderItems, ClientsDb db)
//{
//    db.OrderItems.Add(orderItems);
//    await db.SaveChangesAsync();
//    return TypedResults.Created($"/orderItems/{orderItems.Id}", orderItems);
//}


//static async Task<IResult> UpdateOrderItems(int id, OrderItem inputOrderItems, ClientsDb db)
//{
//    var orderItems = await db.OrderItems.FindAsync(id);
//    if (orderItems is null) return TypedResults.NotFound();
//    orderItems.OrderId = inputOrderItems.OrderId;
//    orderItems.ItemId = inputOrderItems.ItemId;
//    orderItems.Quantity = inputOrderItems.Quantity;
//    await db.SaveChangesAsync();
//    return TypedResults.NoContent();
//}

//static async Task<IResult> DeleteOrderItems(int id, ClientsDb db)
//{
//    if (await db.OrderItems.FindAsync(id) is OrderItem orderItems)
//    {
//        db.OrderItems.Remove(orderItems);
//        await db.SaveChangesAsync();
//        return TypedResults.NoContent();
//    }
//    return TypedResults.NotFound();
//}

static async Task<IResult> GetItemWithCategory(int id, ClientsDb db)
{
    var item = await db.Items.FindAsync(id);
    if (item == null) return TypedResults.NotFound();

    var categories = await db.Categories.Where(c => c.Id == item.CategoryId).ToListAsync();
    return TypedResults.Ok(new { categories });
}

static async Task<IResult> CreateItems(Items items, ClientsDb db)
{
    db.Items.Add(items);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/items/{items.Id}", items);
}

static async Task<IResult> UpdateItems(int id, Items inputItems, ClientsDb db)
{
    var item = await db.Items.FindAsync(id);
    if (item is null) return TypedResults.NotFound();

    item.Name = inputItems.Name;
    item.Price = inputItems.Price;
    item.CategoryId = inputItems.CategoryId;

    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}

static async Task<IResult> DeleteItems(int id, ClientsDb db)
{
    if (await db.Items.FindAsync(id) is Items items)
    {
        db.Items.Remove(items);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    return TypedResults.NotFound();
}

// ------ Route Order ------ //
static async Task<IResult> GetAllOrder(ClientsDb db)
{
    return TypedResults.Ok(await db.Orders.ToArrayAsync());
}

static async Task<IResult> GetOrderbyId(int id, ClientsDb db)
{
    return await db.Orders.FindAsync(id)
        is Order order
        ? TypedResults.Ok(order)
        : TypedResults.NotFound();
}

static async Task<IResult> CreateOrder(Order order, ClientsDb db)
{

    var clientExists = await db.Clients.AnyAsync(c => c.Id == order.ClientId);
    if (!clientExists)
    {
        return TypedResults.BadRequest($"Client with Id {order.ClientId} does not exist.");
    }

    var newOrder = new Order
    {
        ClientId = order.ClientId,
        Date = order.Date,
        OrderItems = new List<OrderItem>()
    };

    db.Orders.Add(newOrder);
    db.SaveChanges();

 
    foreach (var orderItem in order.OrderItems.ToList())
    {
        var item = await db.Items.FindAsync(orderItem.ItemId);
        if (item == null)
        {
            return TypedResults.BadRequest($"Item with Id {orderItem.ItemId} does not exist.");
        }

        var newOrderItem = new OrderItem
        {
            OrderId = order.Id,
            ItemId = item.Id,
            Quantity = orderItem.Quantity,
        };

        newOrder.OrderItems.Add(newOrderItem);
    }

    newOrder.Total = newOrder.OrderItems.Sum(oi => db.Items.Find(oi.ItemId)!.Price * oi.Quantity); 
    db.SaveChanges();
    return TypedResults.Created($"/orders/{order.Id}", newOrder);
  
}


static async Task<IResult> UpdateOrder(int id, Order inputOrder, ClientsDb db)
{
    var order = await db.Orders.FindAsync(id);
    if (order is null) return TypedResults.NotFound();
    order.ClientId = inputOrder.ClientId;
    await db.SaveChangesAsync();
    return TypedResults.NoContent();
}

static async Task<IResult> DeleteOrder(int id, ClientsDb db)
{
    if (await db.Orders.FindAsync(id) is Order order)
    {
        db.Orders.Remove(order);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    return TypedResults.NotFound();
}

