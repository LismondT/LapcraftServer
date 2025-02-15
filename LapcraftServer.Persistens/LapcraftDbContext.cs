using Microsoft.EntityFrameworkCore;

using LapcraftServer.Domain.Entities;
using LapcraftServer.Persistens.Configurations;

namespace LapcraftServer.Persistens;

public class LapcraftDbContext : DbContext
{
    public DbSet<User> Users{ get; set; }

    public LapcraftDbContext(DbContextOptions<LapcraftDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
    }
}
