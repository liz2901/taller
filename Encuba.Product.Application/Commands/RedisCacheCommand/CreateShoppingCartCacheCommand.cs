using Encuba.Product.Application.Dtos.Responses;
using Encuba.Product.Domain.Dtos;
using MediatR;

namespace Encuba.Product.Application.Commands.RedisCacheCommand;

public record CreateShoppingCartCacheCommand(
    Guid UserId,
    int Quantity,
    List<ProductResponse> ProductResponses
    ) : IRequest<EntityResponse<bool>>
{
    
}