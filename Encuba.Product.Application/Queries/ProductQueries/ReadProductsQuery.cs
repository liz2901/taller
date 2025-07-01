using Encuba.Product.Application.Dtos.Responses;
using Encuba.Product.Domain.Dtos;
using MediatR;

namespace Encuba.Product.Application.Queries.ProductQueries;

public record ReadProductsQuery : IRequest<EntityResponse<IEnumerable<ProductResponse>>>;
