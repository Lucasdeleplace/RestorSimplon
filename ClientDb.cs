using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

class ClientsDb : DbContext
{
    public ClientsDb(DbContextOptions<ClientsDb> options) : base(options) { }
    public DbSet<Clients> Clients => Set<Clients>();

}