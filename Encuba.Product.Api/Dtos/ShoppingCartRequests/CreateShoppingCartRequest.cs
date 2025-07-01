using Encuba.Product.Application.Commands.ShoppingCartCommands;

namespace Encuba.Product.Api.Dtos.ShoppingCartRequests;

public record CreateShoppingCartRequest(
    Guid UserId,
    List<Guid> ProductIds)
{
    public CreateShoppingCartCommand ToApplicationRequest()
    {
        return new CreateShoppingCartCommand(UserId, ProductIds);
    }
}