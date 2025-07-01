namespace Encuba.Product.Application.Dtos.Responses;

public record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price);
