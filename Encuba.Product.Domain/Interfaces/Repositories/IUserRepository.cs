using Encuba.Product.Domain.Entities;
using Encuba.Product.Domain.Seed;

namespace Encuba.Product.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    User Add(User user);
    User Update(User user);
    
    Task<User> GetByUserAsync(string userName);
}