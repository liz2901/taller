using  Encuba.Product.Application.Commands.AuthJwtCommands;

namespace  Encuba.Product.Api.Dtos.AuthJwtRequests;

public record CreateJwtRequest(string AccessToken)
{
    public CreateJwtCommand ToApplicationRequest(string secret)
    {
        return new CreateJwtCommand(AccessToken, secret);
    }
}