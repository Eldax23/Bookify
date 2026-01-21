using Bookify.Domain.Users;

namespace Bookify.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<User> , IUserRepository
{
    // the repository base class already implements the IUserRepository so there is no need for further changes.
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}