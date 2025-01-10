using Microsoft.EntityFrameworkCore;

class ClientsDb : DbContext
{
    public ClientsDb(DbContextOptions<ClientsDb> options) : base(options) { }
    public DbSet<Clients> Clients => Set<Clients>();
    public DbSet<Categories> Categories => Set<Categories>();
    public DbSet<Items> Items => Set<Items>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Items>()
            .HasOne(i => i.Category)
            .WithMany()
            .HasForeignKey(i => i.CategoryId)
            .IsRequired();

        modelBuilder.Entity<OrderItem>()
            .HasOne<Items>()
            .WithMany()
            .HasForeignKey(oi => oi.ItemId);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);
    }


}
