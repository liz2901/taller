using Encuba.Product.Application.Dtos.Requests;
using Encuba.Product.Domain.Dtos;
using Encuba.Product.Domain.Interfaces.Services;
using MediatR;

namespace Encuba.Product.Application.Queries.ShoppingCartQueries;

public class ReadShoppingCartCacheQueryHandler(ICacheRepository cacheRepository)
    : IRequestHandler<ReadShoppingCartCacheQuery, EntityResponse<ProductCacheResponse>>
{
    public async Task<EntityResponse<ProductCacheResponse>> Handle(ReadShoppingCartCacheQuery request,
        CancellationToken cancellationToken)
    {
        var cache = await cacheRepository.GetItemAsync<ProductCacheResponse>(request.Id);
        return cache == null
            ? EntityResponse<ProductCacheResponse>.Error("No existe en cache los productos del usuario")
            : EntityResponse.Success(cache);
    }
}