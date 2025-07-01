using Encuba.Product.Domain.Seed;

namespace Encuba.Product.Domain.Entities;

public class ShoppingCart : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }

    protected ShoppingCart()
    {
        Id = Guid.NewGuid();
    }

    public ShoppingCart(Guid userId, Guid productId, Guid orderId, DateTime createdAt) : this()
    {
        UserId = userId;
        ProductId = productId;
        OrderId = orderId;
        CreatedAt = createdAt;
    }
}