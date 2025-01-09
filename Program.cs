using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ClientsDb>(opt => opt.UseSqlite("Data Source=RestoSimplon.db"));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// ------ Route Client ------ //
RouteGroupBuilder clients = app.MapGroup("/clients");
RouteGroupBuilder categories = app.MapGroup("/categories");
RouteGroupBuilder items = app.MapGroup("/items");
RouteGroupBuilder orders = app.MapGroup("/orders");

clients.MapGet("/", GetAllClients);
clients.MapGet("/{id}", GetClientbyId);
clients.MapPost("/", CreateClients);
clients.MapPut("/{id}", UpdateClients);
clients.MapDelete("/{id}", DeleteClients);

// ------ Route Category ------ //
categories.MapGet("/", GetAllCategories);
categories.MapGet("/{id}", GetCategoriesbyId);
categories.MapPost("/", CreateCategories);
categories.MapPut("/{id}", UpdateCategories);
categories.MapDelete("/{id}", DeleteCategories);
categories.MapGet("/{id}/with-items", GetCategoryWithItems);

// ------ Route Items ------ //
items.MapGet("/", GetAllItems);
items.MapGet("/{id}", GetItemsbyId);
items.MapPost("/", CreateItems);
items.MapPut("/{id}", UpdateItems);
items.MapDelete("/{id}", DeleteItems);
items.MapGet("/{id}/with-category", GetItemWithCategory);

// ------ Route Order ------ //   
orders.MapGet("/", GetAllOrder);
orders.MapGet("/{id}", GetOrderbyId);
orders.MapPost("/", CreateOrder);
orders.MapPut("/{id}", UpdateOrder);

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

static async Task<IResult> GetItemWithCategory(int id, ClientsDb db)
{
    var item = await db.Items.Include(i => i.Category).FirstOrDefaultAsync(i => i.Id == id);
    return item is not null ? TypedResults.Ok(item) : TypedResults.NotFound();
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
    db.Orders.Add(order);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/orders/{order.Id}", order);
}

static async Task<IResult> UpdateOrder(int id, Order inputOrder, ClientsDb db)
{
    var order = await db.Orders.FindAsync(id);
    if (order is null) return TypedResults.NotFound();
    order.ClientId = inputOrder.ClientId;
    order.ItemId = inputOrder.ItemId;
    order.Quantity = inputOrder.Quantity;
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
