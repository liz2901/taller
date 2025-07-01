using Encuba.Product.Application.Dtos.Responses;
using Encuba.Product.Domain.Dtos;
using MediatR;

namespace Encuba.Product.Application.Queries.ProductQueries;

public record ReadProductQuery(Guid Id) : IRequest<EntityResponse<ProductResponse>>;
