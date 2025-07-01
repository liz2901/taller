using Encuba.Product.Domain.Dtos;
using Encuba.Product.Domain.Interfaces.Repositories;
using MediatR;

namespace Encuba.Product.Application.Commands.ProductCommands;

public class DeleteProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<DeleteProductCommand, EntityResponse<bool>>
{
    public async Task<EntityResponse<bool>> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(command.Id);
        productRepository.Delete(product);
        await productRepository.UnitOfWork.SaveEntityAsync(cancellationToken);
        return EntityResponse.Success(true);
    }
}