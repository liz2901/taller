using Encuba.Product.Application.Dtos.Responses;
using Encuba.Product.Domain.Dtos;
using Encuba.Product.Domain.Interfaces.Repositories;
using MediatR;

namespace Encuba.Product.Application.Queries.ProductQueries;

public class ReadProductsQueryHandler(IProductRepository productRepository)
    : IRequestHandler<ReadProductsQuery, EntityResponse<IEnumerable<ProductResponse>>>
{
    public async Task<EntityResponse<IEnumerable<ProductResponse>>> Handle(ReadProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAll();

        return EntityResponse.Success(products.Select(x => new ProductResponse(x.Id, x.Name, x.Description, x.Price)));
    }
}