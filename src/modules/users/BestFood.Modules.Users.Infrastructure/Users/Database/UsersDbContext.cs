using BestFood.Modules.Users.Domain.Users;
using BestFood.Modules.Users.Domain.Users.Database;
using Microsoft.EntityFrameworkCore;

namespace BestFood.Modules.Users.Infrastructure.Users.Database;

public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options), IUsersDbContext
{
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.HasDefaultSchema(Schemas.Users);
}