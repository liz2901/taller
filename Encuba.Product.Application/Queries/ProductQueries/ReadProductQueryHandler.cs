using Encuba.Product.Application.Dtos.Responses;
using Encuba.Product.Domain.Dtos;
using Encuba.Product.Domain.Interfaces.Repositories;
using MediatR;

namespace Encuba.Product.Application.Queries.ProductQueries;

public class ReadProductQueryHandler(IProductRepository productRepository)
    : IRequestHandler<ReadProductQuery, EntityResponse<ProductResponse>>
{
    public async Task<EntityResponse<ProductResponse>> Handle(ReadProductQuery query,
        CancellationToken cancellationToken)
    {
        var product = await productRepository.GetById(query.Id);
        var response = new ProductResponse(product.Id, product.Name, product.Description, product.Price);
        return EntityResponse.Success<ProductResponse>(response);
    }
}