using Encuba.Product.Application.Commands.RedisCacheCommand;
using Encuba.Product.Application.Dtos.Responses;

namespace Encuba.Product.Api.Dtos.ShoppingCartRequests;

public record CreateShoppingCartCacheRequest(
    Guid UserId,
    int Quantity,
    List<ProductResponse> ProductResponses)
{
    public CreateShoppingCartCacheCommand ToApplicationRequest()
    {
        return new CreateShoppingCartCacheCommand(UserId, Quantity, ProductResponses);
    }
}