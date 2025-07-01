using Encuba.Product.Application.Queries.ShoppingCartQueries;

namespace Encuba.Product.Api.Dtos.ShoppingCartRequests;

public record ReadShoppingCartCacheRequest
{
    public ReadShoppingCartCacheQuery ToApplicationRequest(string id)
    {
        return new ReadShoppingCartCacheQuery(id);
    }
}