using Encuba.Product.Domain.Dtos;
using MediatR;

namespace Encuba.Product.Application.Commands.ShoppingCartCommands;

public record CreateShoppingCartCommand(
    Guid UserId,
    List<Guid> ProductIds) : IRequest<EntityResponse<bool>>;