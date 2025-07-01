using Encuba.Product.Domain.Dtos;
using MediatR;

namespace Encuba.Product.Application.Commands.ProductCommands;

public record DeleteProductCommand(Guid Id) : IRequest<EntityResponse<bool>>;