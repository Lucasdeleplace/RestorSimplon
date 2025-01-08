using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ClientsDb>(opt => opt.UseSqlite("Data Source=RestoSimplon.db"));

var app = builder.Build();


RouteGroupBuilder clients = app.MapGroup("/clients");

clients.MapGet("/", GetAllClients);

clients.MapGet("/{id}", GetClientbyId);

clients.MapPost("/", CreateClients);

clients.MapPut("/{id}", UpdateClients);

clients.MapDelete("/{id}", DeleteClients);
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