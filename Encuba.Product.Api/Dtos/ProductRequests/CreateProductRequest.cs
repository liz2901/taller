using Encuba.Product.Application.Commands.ProductCommands;

namespace Encuba.Product.Api.Dtos.ProductRequests;

public record CreateProductRequest(
    string Name,
    string Description,
    decimal Price)
{
    public CreateProductCommand ToApplicationRequest()
    {
        return new CreateProductCommand(Name, Description, Price);
    }
}