using Encuba.Product.Application.Commands.ProductCommands;

namespace Encuba.Product.Api.Dtos.ProductRequests;

public record UpdateProductRequest(
    string Name,
    string Description,
    decimal Price)
{
    public UpdateProductCommand ToApplicationRequest(Guid id)
    {
        return new UpdateProductCommand(id, Name, Description, Price);
    }
}