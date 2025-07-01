using Encuba.Product.Domain.Entities;
using Encuba.Product.Domain.Interfaces.Repositories;
using Encuba.Product.Domain.Seed;
using Microsoft.EntityFrameworkCore;

namespace Encuba.Product.Infrastructure.Repositories;

public class UserRepository(SecurityDbContext dbContext)
    : IUserRepository
{
    public IUnitOfWork UnitOfWork => dbContext;

    public User Add(User user)
    {
        return dbContext.Users.Add(user).Entity;
    }

    public User Update(User user)
    {
        return dbContext.Users.Update(user).Entity;
    }

    public async Task<User> GetByUserAsync(string userName)
    {
        return (await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName || u.Email == userName))!;
    }
}