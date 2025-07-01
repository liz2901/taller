using Encuba.Product.Domain.Dtos;
using MediatR;

namespace Encuba.Product.Application.Commands.ProductCommands;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price
) : IRequest<EntityResponse<bool>>;