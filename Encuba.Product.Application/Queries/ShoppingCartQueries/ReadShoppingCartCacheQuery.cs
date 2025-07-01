using Encuba.Product.Application.Dtos.Requests;
using Encuba.Product.Domain.Dtos;
using MediatR;

namespace Encuba.Product.Application.Queries.ShoppingCartQueries;

public record ReadShoppingCartCacheQuery(string Id) : IRequest<EntityResponse<ProductCacheResponse>>;