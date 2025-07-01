using  Encuba.Product.Application.Dtos.Responses;
using Encuba.Product.Domain.Dtos;
using  Encuba.Product.Domain.Entities;
using  Encuba.Product.Domain.Interfaces.Repositories;
using  Encuba.Product.Domain.Seed;
using MediatR;

namespace  Encuba.Product.Application.Commands.AuthUserCommands;

public class
    RefreshTokenCommandHandler(IPublicAccessTokenRepository publicAccessTokenRepository)
    : IRequestHandler<RefreshTokenCommand, EntityResponse<PublicAccessTokenResponse>>
{
    public async Task<EntityResponse<PublicAccessTokenResponse>> Handle(RefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        var publicAccessTokenResponse = await ValidateRefreshToken(command);
        if (!publicAccessTokenResponse.IsSuccess)
        {
            return EntityResponse<PublicAccessTokenResponse>.Error(publicAccessTokenResponse);
        }

        // Verify if the token already expired, if expiresIn is less than current date -> it is expired
        if (publicAccessTokenResponse.Value.ExpiresIn < DateTime.UtcNow)
            return EntityResponse<PublicAccessTokenResponse>.Error(
                $"The access token: {publicAccessTokenResponse.Value.AccessToken} has expired.");

        var result = await GenerateNewToken(publicAccessTokenResponse.Value);

        return EntityResponse.Success(result);
    }

    private async Task<PublicAccessTokenResponse> GenerateNewToken(PublicAccessToken publicAccessToken)
    {
        var newPublicAccessToken = PublicAccessToken.CreateNewClientPublicAccessToken();

        publicAccessToken.AccessToken = newPublicAccessToken.AccessToken;
        publicAccessToken.ExpiresIn = newPublicAccessToken.ExpiresIn;

        publicAccessTokenRepository.Update(publicAccessToken);
        await publicAccessTokenRepository.UnitOfWork.SaveEntityAsync();

        return new PublicAccessTokenResponse(publicAccessToken.AccessToken, publicAccessToken.RefreshToken,
            publicAccessToken.Scope, publicAccessToken.ExpiresIn);
    }

    private async Task<EntityResponse<PublicAccessToken>> ValidateRefreshToken(RefreshTokenCommand command)
    {

        var dbPublicAccessToken = await publicAccessTokenRepository.GetByRefreshToken(command.RefreshToken, Scopes.PublicAccessTokenScopes.Client);

        return dbPublicAccessToken == null
            ? EntityResponse<PublicAccessToken>.Error("The refresh token is invalid")
            : EntityResponse.Success(dbPublicAccessToken);
    }
}