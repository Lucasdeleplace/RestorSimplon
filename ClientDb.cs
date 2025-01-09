using Microsoft.EntityFrameworkCore;

class ClientsDb : DbContext
{
    public ClientsDb(DbContextOptions<ClientsDb> options) : base(options) { }
    public DbSet<Clients> Clients => Set<Clients>();
    public DbSet<Categories> Categories => Set<Categories>();
    public DbSet<Items> Items => Set<Items>();
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Items>()
            .HasOne(i => i.Category)
            .WithMany()
            .HasForeignKey(i => i.CategoryId)
            .IsRequired();
    }
}
