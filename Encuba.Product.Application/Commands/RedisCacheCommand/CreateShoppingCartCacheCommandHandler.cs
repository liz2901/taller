using Encuba.Product.Application.Dtos.Requests;
using Encuba.Product.Domain.Dtos;
using Encuba.Product.Domain.Interfaces.Services;
using MediatR;

namespace Encuba.Product.Application.Commands.RedisCacheCommand;

public class CreateShoppingCartCacheCommandHandler(ICacheRepository cacheRepository)
    : IRequestHandler<CreateShoppingCartCacheCommand, EntityResponse<bool>>
{
    public async Task<EntityResponse<bool>> Handle(CreateShoppingCartCacheCommand command,
        CancellationToken cancellationToken)
    {
        var cart = new ProductCacheResponse(command.UserId, command.Quantity, command.ProductResponses);
        await cacheRepository.AddItemAsync(command.UserId.ToString(), cart);
        return EntityResponse.Success(true);
    }
}