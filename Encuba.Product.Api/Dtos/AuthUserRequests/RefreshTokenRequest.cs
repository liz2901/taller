using  Encuba.Product.Application.Commands.AuthUserCommands;

namespace  Encuba.Product.Api.Dtos.AuthUserRequests;

public record RefreshTokenRequest(string RefreshToken)
{
    public RefreshTokenCommand ToApplicationRequest()
    {
        return new RefreshTokenCommand(RefreshToken);
    }
}