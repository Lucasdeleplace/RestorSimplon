using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;
//Créer un constructeur d'application (builder) avec les arguments de ligne de commande.
var builder = WebApplication.CreateBuilder(args);
//Ajouter un contexte de base de données (`ClientsDb`) et le configurer pour utiliser SQLite avec un fichier nommé `RestoSimplon.db`.
builder.Services.AddDbContext<ClientsDb>(opt => opt.UseSqlite("Data Source=RestoSimplon.db"));

//Ajoute du support des contrôleurs avec une configuration JSON pour gérer les références dans les sérialisations.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
//Ajoute des outils pour documenter l'API avec Swagger (exploration et génération de documentation).
builder.Services.AddEndpointsApiExplorer();
//Définir une documentation pour l'API (titre, version, description, contact, etc.).
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RestorSimplonAPI",
        Version = "v1",
        Description = "Une API pour gérer des commandes",
        Contact = new OpenApiContact
        {
            Name = "Legroupe",
            Email = "legroupejla@example.com",
            Url = new Uri("https://cheerful-travesseiro-ea025c.netlify.app/")
        }
    });

    c.EnableAnnotations();
});
var app = builder.Build();

//Construire l'application avec les services configurés.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
        c.RoutePrefix = "";
    });
}
//Configurer le routage
app.UseRouting();
app.MapControllers();

// configuration des routes et endPoint 
RouteGroupBuilder clients = app.MapGroup("/clients").WithTags("Clients");
RouteGroupBuilder categories = app.MapGroup("/categories").WithTags("Categories");
RouteGroupBuilder items = app.MapGroup("/items").WithTags("Items");
RouteGroupBuilder orders = app.MapGroup("/orders").WithTags("Orders");

// ------ Route Client ------ //
// utilisation de swagger afin de déterminer le role de chaques routes pour les clients 
clients.MapGet("/", GetAllClients)
   .WithMetadata(new SwaggerOperationAttribute(
        summary: "Récupère tous les éléments clients",
        description: "Renvois une liste de tous les éléments clients."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'éléments clients trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun élément clients non trouvés"));
clients.MapGet("/{id}", GetClientbyId)
   .WithMetadata(new SwaggerOperationAttribute(
        summary: "Récupère tous les éléments d'un client via son ID",
        description: "Renvois une liste de tout les éléments d'un client."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'éléments client trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun élément client non trouvés"));

clients.MapPost("/", CreateClients)
       .WithMetadata(new SwaggerOperationAttribute(
        summary: "Créer tous les éléments client",
        description: "Créer une liste de tous les éléments client."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Création d'éléments clients trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun création clients non trouvés"));
clients.MapPut("/{id}", UpdateClients)
     .WithMetadata(new SwaggerOperationAttribute(
        summary: "mettre à jour tous les éléments client",
        description: "mets à jour une liste de tous les éléments client."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Mise à jour d'éléments clients trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun mise à jour clients non trouvés"));
clients.MapDelete("/{id}", DeleteClients)
.WithMetadata(new SwaggerOperationAttribute(
        summary: "supprimer tous les éléments client",
        description: "Supprimer une liste de tous les éléments client."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Suppression d'éléments clients trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Supression clients non trouvés"));
clients.MapGet("/{id}/with-order", GetClientWithOrder)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Récupère tous les éléments client avec ses commandes",
        description: "Recupere une liste de tous les éléments client avec leurs commandes."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'éléments clients avec leurs commandes trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun clients avec leurs commandes non trouvés"));

// ------ Route Category ------ //
categories.MapGet("/", GetAllCategories)
.WithMetadata(new SwaggerOperationAttribute(
        summary: "Récupère tous les éléments catégories",
        description: "Renvois une liste de tous les éléments catégories."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'éléments catégories trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun élément catégories non trouvés"));
categories.MapGet("/{id}", GetCategoriesbyId)
.WithMetadata(new SwaggerOperationAttribute(
        summary: "Récupère tous les éléments d'un catégorie via son ID",
        description: "Renvois une liste de tout les éléments d'un catégorie."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'éléments catégorie trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun élément catégorie non trouvés"));
categories.MapPost("/", CreateCategories)
 .WithMetadata(new SwaggerOperationAttribute(
        summary: "Créer tous les éléments catégories",
        description: "Créer une liste de tous les éléments catégories."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Création d'éléments catégories trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun création catégories non trouvés"));
categories.MapPut("/{id}", UpdateCategories)
 .WithMetadata(new SwaggerOperationAttribute(
        summary: "mettre à jour tous les éléments catégories",
        description: "mets à jour une liste de tous les éléments catégories."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Mise à jour d'éléments catégories trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Mise à jour catégories non trouvés"));
categories.MapDelete("/{id}", DeleteCategories)
    .WithMetadata(new SwaggerOperationAttribute(
    summary: "supprimer tous les éléments catégories",
        description: "Supprimer une liste de tous les éléments catégories."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Suppression d'éléments catégories trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Supression catégories non trouvés"));
categories.MapGet("/{id}/with-items", GetCategoryWithItems)
       .WithMetadata(new SwaggerOperationAttribute(
        summary: "Récupère tous les éléments catégories avec ses items",
        description: "Récupère une liste de tous les éléments catégories avec leurs items."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'éléments catégories avec leurs items trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucune catégories avec leurs items non trouvés"));

// ------ Route Items ------ //
items.MapGet("/", GetAllItems)
.WithMetadata(new SwaggerOperationAttribute(
        summary: "Récupère tous les éléments items",
        description: "Renvois une liste de tous les éléments items."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'élément items trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun élément items non trouvés"));
items.MapGet("/{id}", GetItemsbyId)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Récupère tous les éléments d'un item via son ID",
        description: "Renvois une liste de tout les éléments d'un item."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'éléments item trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun élément item non trouvés"));
items.MapPost("/", CreateItems)
 .WithMetadata(new SwaggerOperationAttribute(
        summary: "Créer tous les éléments items",
        description: "Créer une liste de tous les éléments items."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Création d'éléments items trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun création items non trouvés"));
items.MapPut("/{id}", UpdateItems)
     .WithMetadata(new SwaggerOperationAttribute(
        summary: "mettre à jour tous les éléments items",
        description: "mets à jour une liste de tous les éléments item."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Mise à jour d'éléments item trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Mise à jour item non trouvés"));
items.MapDelete("/{id}", DeleteItems)
     .WithMetadata(new SwaggerOperationAttribute(
    summary: "supprimer tous les éléments item",
        description: "Supprimer une liste de tous les éléments item."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Suppression d'éléments item trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Supression item non trouvés"));
items.MapGet("/{id}/with-category", GetItemWithCategory)
         .WithMetadata(new SwaggerOperationAttribute(
        summary: "Récupère tous les éléments items avec ses catégories",
        description: "Récupère une liste de tous les éléments items avec leurs catégories."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'éléments items avec leurs catégories trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucune items avec leurs catégories non trouvés"));

// ------ Route Order ------ //   
orders.MapGet("/", GetAllOrder)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Récupère tous les éléments commandes",
        description: "Renvois une liste de tous les éléments commandes."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'élément commandes trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun élément commandes non trouvés"));
orders.MapGet("/{id}", GetOrderbyId)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Récupère tous les éléments d'une commande via son ID",
        description: "Renvois une liste de tout les éléments d'une commande."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Liste d'éléments commande trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun élément commande non trouvés"));
orders.MapPost("/", CreateOrder)
     .WithMetadata(new SwaggerOperationAttribute(
        summary: "Créer tous les commande items",
        description: "Créer une liste de tous les commande items."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Création d'éléments catégorie trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Aucun création catégorie non trouvés"));
orders.MapPut("/{id}", UpdateOrder)
     .WithMetadata(new SwaggerOperationAttribute(
        summary: "mettre à jour tous les éléments commande",
        description: "mets à jour une liste de tous les éléments commande."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Mise à jour d'éléments commande trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Mise à jour commande non trouvés"));
orders.MapDelete("/{id}", DeleteOrder)
   .WithMetadata(new SwaggerOperationAttribute(
    summary: "supprimer tous les éléments commandes",
        description: "Supprimer une liste de tous les éléments commandes."))
    .WithMetadata(new SwaggerResponseAttribute(200, "Suppression d'éléments commandes trouvés"))
    .WithMetadata(new SwaggerResponseAttribute(404, "Supression commandes non trouvés"));

app.Run();
// Fonction pour afficher tout les clients
static async Task<IResult> GetAllClients(ClientsDb db)
{
    // Exécute une requête pour récupérer tous les enregistrements de la table Clients
    // et les retourne sous forme de tableau.
    return TypedResults.Ok(await db.Clients.ToArrayAsync());
}
// Fonction pour afficher un client par son Id.
static async Task<IResult> GetClientbyId(int id, ClientsDb db)
{
    // Cherche un enregistrement dans la table Clients correspondant à l'ID.
    return await db.Clients.FindAsync(id)
        // Vérifie si le résultat n'est pas null 
        is Clients clients
            // Si un client est trouvé, retourne une réponse HTTP 200 avec les données du client.
            ? TypedResults.Ok(clients)
            // Si aucun client n'est trouvé, retourne une réponse HTTP 404.
            : TypedResults.NotFound();
}
// Fonction pour créer un client 
static async Task<IResult> CreateClients(Clients clients, ClientsDb db)
{
    // ajout une nouvelle instance Clients à la base de données 
    db.Clients.Add(clients);
    // Enregistre les modifiactions apportées dans la base de données 
    await db.SaveChangesAsync();
    // Retourne une réponse si la création est un succès 
    return TypedResults.Created($"/clients/{clients.Id}", clients);
}
// Fonction pour modifier un client
static async Task<IResult> UpdateClients(int id, Clients inputClients, ClientsDb db)
{
    // Recherche un client dans la base de données en utilisant L'Id.
    var client = await db.Clients.FindAsync(id);
    // Vérifie si le client existe. Sinon, retourne un message d'erreur.
    if (client is null) return TypedResults.NotFound();

    // met à jour par rapport aux inputs renseigner dans clients
    client.First_Name = inputClients.First_Name;
    client.Last_Name = inputClients.Last_Name;
    client.Address = inputClients.Address;
    client.Phone_Number = inputClients.Phone_Number;

    // Enregistre les modifiactions apportées dans la base de données 
    await db.SaveChangesAsync();
    // Retourne une réponse si la mise à jour est un succès 
    return TypedResults.NoContent();
}

// Fonction pour supprimer un client
static async Task<IResult> DeleteClients(int id, ClientsDb db)
{
    // Recherche un client dans la base de données en utilisant L'Id.
    if (await db.Clients.FindAsync(id) is Clients clients)
    {
        // supprime le client de la base de données
        db.Clients.Remove(clients);
        // Enregistre la suppression apportées dans la base de données
        await db.SaveChangesAsync();
        // Retourne une réponse si la suppresion est un succès
        return TypedResults.NoContent();
    }
    // Retourne une réponse d'erreur si la suppresion n'est pas succès
    return TypedResults.NotFound();
}
// Fonction pour afficher un client avec ses commandes
static async Task<IResult> GetClientWithOrder(int id, ClientsDb db)
{
    // Recherche toutes les commandes associées au client dans la base de données en utilisant L'Id.
    var orders = await db.Orders.Where(o => o.ClientId == id).ToListAsync();
    // Recherche un client dans la base de données en utilisant L'Id.
    var client = await db.Clients.FindAsync(id);
    // vérifiacation du client si il n'est pas trouver retour d'un message d'erreur
    if (client is null) return TypedResults.NotFound();
    // Associe les commandes récupérées au client.
    client.Orders = orders;
    // récupère tous les OrderItems 
    var itemOrder = await db.OrderItems.ToListAsync();
    // asocie les articles de commande à leurs commandes respectives
    foreach (var order in orders)
    {
        order.OrderItems = itemOrder.Where(oi => oi.OrderId == order.Id).ToList();
    }
    // Liste pour stocker les articles associés aux commandes.
    var items = new List<Items>();
    // parcourt les commandes pour récupérer les articles correspondants 
    foreach (var order in orders)
    {
        foreach (var orderItem in order.OrderItems)
        {
            // Recherche les détails de chaque article à l'aide de son ID
            var item = await db.Items.FindAsync(orderItem.ItemId);
            // Si l'article existe, il est ajouté à la liste.
            if (item != null)
            {
                items.Add(item);
            }
        }
    }
    // Retourne une réponse contenant les détails du client et des articles.
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

