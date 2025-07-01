using  Encuba.Product.Application.Dtos.Responses;
using Encuba.Product.Domain.Dtos;
using MediatR;

namespace  Encuba.Product.Application.Commands.AuthUserCommands;

public record LoginUserCommand(
    string Username,
    string Password,
    string UserAgent,
    string ClientIp) : IRequest<EntityResponse<PublicAccessTokenResponse>>;