using FarmControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmControl.Infrastructure.AccessRepositories;
public class FarmControlContext : DbContext
{
    public FarmControlContext(DbContextOptions<FarmControlContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FarmControlContext).Assembly);
    }
}
