using Microsoft.EntityFrameworkCore;

namespace BestFood.Modules.Users.Domain.Users.Database;

public interface IUsersDbContext
{
    public DbSet<User> Users { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}