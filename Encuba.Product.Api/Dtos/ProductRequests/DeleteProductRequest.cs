using Encuba.Product.Application.Commands.ProductCommands;

namespace Encuba.Product.Api.Dtos.ProductRequests;

public record DeleteProductRequest
{
    public DeleteProductCommand ToApplicationRequest(Guid id)
    {
        return new DeleteProductCommand(id);
    }
}