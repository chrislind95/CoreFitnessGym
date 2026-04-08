using Infrastructure.Identity;
using Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public class PersistenceContext(DbContextOptions<PersistenceContext> options) : IdentityDbContext<AppUser, IdentityRole, string>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceContext).Assembly);
    }

    public DbSet<ContactRequestEntity> ContactRequests => Set<ContactRequestEntity>();
}
