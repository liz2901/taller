using Encuba.Product.Domain.Seed;

namespace Encuba.Product.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    
    private readonly List<ShoppingCart> _shoppingCarts;
    public IReadOnlyCollection<ShoppingCart> ShoppingCarts => _shoppingCarts;
    protected Product()
    {
        Id = Guid.NewGuid();
        _shoppingCarts = new List<ShoppingCart> { };
    }

    public Product(string name, string description, decimal price)
    : this()
    {
        Name = name;
        Description = description;
        Price = price;
    }
}