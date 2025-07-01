using  Encuba.Product.Domain.Dtos;
using MediatR;

namespace  Encuba.Product.Application.Commands.AuthJwtCommands;

public record CreateJwtCommand(
    string AccessToken,
    string JwtSecret) : IRequest<EntityResponse<JwtResponse>>;