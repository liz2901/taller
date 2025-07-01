using Encuba.Product.Domain.Entities;
using Encuba.Product.Domain.Seed;

namespace Encuba.Product.Domain.Interfaces.Repositories;

public interface IShoppingCartRepository : IRepository<ShoppingCart>
{
    void AddRange(List<ShoppingCart> shoppingCarts);
}