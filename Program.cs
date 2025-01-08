 using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ClientsDb>(opt => opt.UseSqlite("Data Source=RestoSimplon.db"));

var app = builder.Build();

// ------ Route Client ------ //
RouteGroupBuilder clients = app.MapGroup("/clients");
RouteGroupBuilder categories = app.MapGroup("/categories");
RouteGroupBuilder items = app.MapGroup("/items");

clients.MapGet("/", GetAllClients);

clients.MapGet("/{id}", GetClientbyId);

clients.MapPost("/", CreateClients);

clients.MapPut("/{id}", UpdateClients);

clients.MapDelete("/{id}", DeleteClients);
// Route category

categories.MapGet("/", GetAllCategories);

categories.MapGet("/{id}", GetCategoriesbyId);

categories.MapPost("/", CreateCategories);

categories.MapPut("/{id}", UpdateCategories);

categories.MapDelete("/{id}", DeleteCategories);

// ------ Route Items ------ //

items.MapGet("/", GetAllItems);

items.MapGet("/{id}", GetItemsbyId);

items.MapPost("/", CreateItems);

items.MapPut("/{id}", UpdateItems);

items.MapDelete("/{id}", DeleteItems);

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

static async Task<IResult> CreateItems(Items items, ClientsDb db)
{
    db.Items.Add(items);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/items/{items.Id}", items);
}

static async Task<IResult> UpdateItems(int id, Items inputItems, ClientsDb db)
{
    var item = await db.Items.FindAsync(id);
    Console.WriteLine(item);

    if (item is null) return TypedResults.NotFound();

    item.Name = inputItems.Name;
    item.Price = inputItems.Price;
    item.Category = inputItems.Category;

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