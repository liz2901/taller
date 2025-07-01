using System.IdentityModel.Tokens.Jwt;
using System.Text;
using  Encuba.Product.Domain.Dtos;
using  Encuba.Product.Domain.Entities;
using  Encuba.Product.Domain.Interfaces.Repositories;
using  Encuba.Product.Domain.Seed;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace  Encuba.Product.Application.Commands.AuthJwtCommands;

public class CreateJwtCommandHandler(
    ILogger<CreateJwtCommandHandler> logger,
    IUserRepository userRepository,
    IPublicAccessTokenRepository publicAccessTokenRepository,
    IUserPublicAccessTokenRepository userPublicAccessTokenRepository
) : IRequestHandler<CreateJwtCommand, EntityResponse<JwtResponse>>
{
    public async Task<EntityResponse<JwtResponse>> Handle(CreateJwtCommand command,
        CancellationToken cancellationToken)
    {
        var publicAccessTokenResponse = await ValidatePublicAccessToken(command);
        if (!publicAccessTokenResponse.IsSuccess)
        {
            return EntityResponse<JwtResponse>.Error(publicAccessTokenResponse);
        }

        var payloadResponse = await CreateJwtPayload(command, publicAccessTokenResponse.Value);
        if (!payloadResponse.IsSuccess)
        {
            return EntityResponse<JwtResponse>.Error(payloadResponse);
        }

        var jwtToken = GenerateJwt(command, payloadResponse.Value);

        return EntityResponse.Success(new JwtResponse(jwtToken));
    }

    #region Private methods

    private async Task<EntityResponse<JwtPayloadDto>> CreateJwtPayload(CreateJwtCommand command,
        PublicAccessToken publicAccessToken)
    {
        JwtPayloadDto payload = null!;
        if (!publicAccessToken.Scope.Equals(Scopes.PublicAccessTokenScopes.Client,
                StringComparison.InvariantCultureIgnoreCase) && !publicAccessToken.Scope.Equals(
                Scopes.PublicAccessTokenScopes.Worker,
                StringComparison.InvariantCultureIgnoreCase)) return EntityResponse.Success(payload)!;
        
        var validManagerUserResponse = await ValidateUserPublicAccessToken(command, publicAccessToken);
        if (!validManagerUserResponse.IsSuccess)
        {
            return EntityResponse<JwtPayloadDto>.Error(validManagerUserResponse);
        }

        logger.LogInformation("Creating jwt for manager user {@User}", validManagerUserResponse);
        var fullName = GetUserFullName(validManagerUserResponse.Value);
        payload = new JwtPayloadDto(
            validManagerUserResponse.Value.Id,
            fullName,
            publicAccessToken.Scope);

        return EntityResponse.Success(payload)!;
    }

    private string GetUserFullName(User user)
    {
        var secondName = user.SecondName != null ? $" {user.SecondName}" : string.Empty;
        var secondLastName =
            user.SecondLastName != null ? $" {user.SecondLastName}" : string.Empty;

        var fullName =
            $"{user.FirstName}{secondName} {user.FirstLastName}{secondLastName}";
        return fullName;
    }

    private string GenerateJwt(CreateJwtCommand command, JwtPayloadDto payload)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(command.JwtSecret));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var secToken = new JwtSecurityToken(null, null, null, null,
            null, signingCredentials)
        {
            Payload =
            {
                [nameof(JwtPayloadDto.Id)] = payload.Id,
                [nameof(JwtPayloadDto.FullName)] = payload.FullName,
                [nameof(JwtPayloadDto.Scope)] = payload.Scope
            }
        };

        logger.LogInformation("Creating security token for {@Token}", secToken);

        var jwtToken = tokenHandler.WriteToken(secToken);
        return jwtToken;
    }

    private async Task<EntityResponse<User>> ValidateUserPublicAccessToken(CreateJwtCommand command,
        PublicAccessToken publicAccessToken)
    {
        var dbUserPublicAccessTokens = await userPublicAccessTokenRepository.GetByUserIdAsync(publicAccessToken.Id);

        if (dbUserPublicAccessTokens == null)
            return EntityResponse<User>.Error(
                $"There isn't user who is a partner or manager with this access token : {command.AccessToken}.");


        return EntityResponse.Success(dbUserPublicAccessTokens.User);
    }
    
    private async Task<EntityResponse<PublicAccessToken>> ValidatePublicAccessToken(CreateJwtCommand command)
    {
        var dbPublicAccessTokens = await publicAccessTokenRepository.GetByToken(command.AccessToken);

        if (dbPublicAccessTokens == null)
            return EntityResponse<PublicAccessToken>.Error(
                $"The access token: {command.AccessToken} is not valid. It not exist.");

        if (dbPublicAccessTokens.ExpiresIn < DateTime.UtcNow)
            return EntityResponse<PublicAccessToken>.Error(
                $"The access token: {command.AccessToken} has expired.");

        return EntityResponse.Success(dbPublicAccessTokens);
    }

    #endregion
}