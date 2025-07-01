using Encuba.Product.Application.Dtos.Responses;

namespace Encuba.Product.Application.Dtos.Requests;

public record ProductCacheResponse(
    Guid UserId,
    int Quantity,
    List<ProductResponse> ProductResponses
);