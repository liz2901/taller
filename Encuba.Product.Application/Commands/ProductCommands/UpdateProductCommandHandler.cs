using Encuba.Product.Domain.Dtos;
using Encuba.Product.Domain.Interfaces.Repositories;
using MediatR;

namespace Encuba.Product.Application.Commands.ProductCommands;

public class UpdateProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<UpdateProductCommand, EntityResponse<bool>>
{
    public async Task<EntityResponse<bool>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(command.Id);
        if (product is null)
        {
            return EntityResponse<bool>.Error($"No existen productos con el id {command.Id}");
        }

        await UpdateProduct(command, cancellationToken, product);

        return EntityResponse.Success(true);
    }

    private async Task UpdateProduct(UpdateProductCommand command, CancellationToken cancellationToken,
        Domain.Entities.Product product)
    {
        product.Name = command.Name;
        product.Description = command.Description;
        product.Price = command.Price;
        productRepository.Update(product);
        await productRepository.UnitOfWork.SaveEntityAsync(cancellationToken);
    }
}