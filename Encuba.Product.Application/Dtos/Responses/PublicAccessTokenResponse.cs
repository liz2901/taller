using Encuba.Product.Domain.Dtos;
using MediatR;

namespace  Encuba.Product.Application.Dtos.Responses;

public record PublicAccessTokenResponse(
    string AccessToken,
    string RefreshToken,
    string Scope,
    DateTime ExpiresIn): IRequest<EntityResponse<PublicAccessTokenResponse>>;