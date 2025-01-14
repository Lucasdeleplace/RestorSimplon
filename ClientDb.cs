using System.Text;
using Microsoft.EntityFrameworkCore;

//Créer une classe ClientsDb qui hérite de DbContext sur la base de données Entity Framework Core.
class ClientsDb : DbContext
{
    //Définir un constructeur pour `ClientsDb`
    // Le constructeur accepte un paramètre options (de type `DbContextOptions<ClientsDb>`).
    public ClientsDb(DbContextOptions<ClientsDb> options) : base(options) { }
    //- Représente la table des clients dans la base de données.Chaque client correspond à une instance
    //de la classe Clients.
    public DbSet<Clients> Clients => Set<Clients>();
    public DbSet<Categories> Categories => Set<Categories>();
    public DbSet<Items> Items => Set<Items>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Définir les relations pour Categories :
        modelBuilder.Entity<Categories>()
            //Une catégorie  peut avoir plusieurs articles
            .HasMany(c => c.Items)
            //Un article appartient à une catégorie (relation 1:N).
            .WithOne()
            //La clé étrangère pour cette relation est CategoryId.
            .HasForeignKey(i => i.CategoryId)
            //chaque article doit obligatoirement avoir une catégorie.
            .IsRequired();

        //Définir les relations pour OrderItem :
        modelBuilder.Entity<OrderItem>()
            //Un article de commande est lié à un seul article.
            .HasOne<Items>()
            //Un article peut être lié à plusieurs articles de commande (relation Many to One).
            .WithMany()
            //La clé étrangère pour cette relation est ItemId.
            .HasForeignKey(oi => oi.ItemId);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<Order>()
            .HasOne<Clients>()
            .WithMany()
            .HasForeignKey(o => o.ClientId);
    }
}
