using Encuba.Product.Domain.Dtos;
using Encuba.Product.Domain.Entities;
using Encuba.Product.Domain.Interfaces.Repositories;
using MediatR;

namespace Encuba.Product.Application.Commands.ShoppingCartCommands;

public class CreateShoppingCartCommandHandler(IShoppingCartRepository shoppingCartRepository)
    : IRequestHandler<CreateShoppingCartCommand, EntityResponse<bool>>
{
    public async Task<EntityResponse<bool>> Handle(CreateShoppingCartCommand command,
        CancellationToken cancellationToken)
    {
        var orderId = Guid.NewGuid();
        var carts = new List<ShoppingCart>();
        carts = command.ProductIds.Select(x => new ShoppingCart(command.UserId, x, orderId, DateTime.Now))
            .ToList();
        shoppingCartRepository.AddRange(carts);
        await shoppingCartRepository.UnitOfWork.SaveEntityAsync(cancellationToken);

        return EntityResponse.Success(true);
    }
}