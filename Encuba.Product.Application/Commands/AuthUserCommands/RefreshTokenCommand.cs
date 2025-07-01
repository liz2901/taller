using  Encuba.Product.Application.Dtos.Responses;
using Encuba.Product.Domain.Dtos;
using MediatR;

namespace  Encuba.Product.Application.Commands.AuthUserCommands;

public record RefreshTokenCommand(
    string RefreshToken) : IRequest<EntityResponse<PublicAccessTokenResponse>>;