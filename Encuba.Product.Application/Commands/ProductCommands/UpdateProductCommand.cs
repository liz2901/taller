using Encuba.Product.Domain.Dtos;
using MediatR;

namespace Encuba.Product.Application.Commands.ProductCommands;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price) : IRequest<EntityResponse<bool>>;