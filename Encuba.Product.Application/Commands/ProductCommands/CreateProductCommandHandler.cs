using Encuba.Product.Domain.Dtos;
using Encuba.Product.Domain.Interfaces.Repositories;
using MediatR;

namespace Encuba.Product.Application.Commands.ProductCommands;

public class CreateProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<CreateProductCommand, EntityResponse<bool>>
{
    public async Task<EntityResponse<bool>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Domain.Entities.Product(command.Name, command.Description, command.Price);
        productRepository.Add(product);
        
        await productRepository.UnitOfWork.SaveEntityAsync(cancellationToken);
        
        return EntityResponse.Success(true);
    }
}