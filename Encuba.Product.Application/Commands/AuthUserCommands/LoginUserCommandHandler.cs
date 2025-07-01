using  Encuba.Product.Application.Dtos.Responses;
using Encuba.Product.Domain.Dtos;
using  Encuba.Product.Domain.Entities;
using  Encuba.Product.Domain.Interfaces.Cryptography;
using  Encuba.Product.Domain.Interfaces.Repositories;
using  Encuba.Product.Domain.Seed;
using MediatR;
using Microsoft.Extensions.Logging;

namespace  Encuba.Product.Application.Commands.AuthUserCommands;

public class LoginUserCommandHandler(
    ILogger<LoginUserCommandHandler> logger,
    IBCryptCryptographyHelper bcryptCryptographyHelper,
    IUserRepository userRepository,
    IPublicAccessTokenRepository publicAccessTokenRepository,
    IUserPublicAccessTokenRepository userPublicAccessTokenRepository
) : IRequestHandler<LoginUserCommand, EntityResponse<PublicAccessTokenResponse>>
{
    public async Task<EntityResponse<PublicAccessTokenResponse>> Handle(LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = await ValidateUser(command);

        if (!user.IsSuccess)
        {
            return EntityResponse<PublicAccessTokenResponse>.Error(user.EntityErrorResponse.Message);
        }

        PublicAccessToken publicAccessToken;
        if (user.Value.UserType == Scopes.PublicAccessTokenScopes.Client)
        {
            publicAccessToken = PublicAccessToken.CreateNewClientPublicAccessToken();
        }
        else
        {
            if (user.Value.UserType == Scopes.PublicAccessTokenScopes.Worker)
            {
                publicAccessToken = PublicAccessToken.CreateNewWorkerPublicAccessToken();
            }
            else
            {
                if (user.Value.UserType == Scopes.PublicAccessTokenScopes.Administrator)
                {
                    publicAccessToken = PublicAccessToken.CreateNewAdministratorPublicAccessToken();
                }
                else
                {
                    return EntityResponse<PublicAccessTokenResponse>.Error("Error al iniciar sesión");
                }
            }
        }

        userRepository.Update(user.Value);
        publicAccessTokenRepository.Add(publicAccessToken);
        userPublicAccessTokenRepository.Add(
            new UserPublicAccessToken(user.Value.Id,
                publicAccessToken.Id, command.ClientIp, command.UserAgent));

        await publicAccessTokenRepository.UnitOfWork.SaveEntityAsync(cancellationToken);

        var response = new PublicAccessTokenResponse(publicAccessToken.AccessToken, publicAccessToken.RefreshToken,
            publicAccessToken.Scope, publicAccessToken.ExpiresIn);

        return EntityResponse.Success(response);
    }

    private async Task<EntityResponse<User>> ValidateUser(LoginUserCommand command)
    {

        var dbUsers = await userRepository.GetByUserAsync(command.Username);

        if (dbUsers is not { Status: true })
        {
            logger.LogInformation("El usuario está inactivo {@user}", dbUsers);

            return EntityResponse<User>.Error("El usuario está inactivo");
        }

        var validatePasswordResponse = ValidatePassword(command, dbUsers);
        return !validatePasswordResponse.IsSuccess
            ? EntityResponse<User>.Error(validatePasswordResponse)
            : EntityResponse.Success(dbUsers);
    }

    private EntityResponse<bool> ValidatePassword(LoginUserCommand command, User user)
    {
        var isCorrectPassword = bcryptCryptographyHelper.VerifyBcryptHash(command.Password, user.Password);

        if (isCorrectPassword) return EntityResponse.Success(true);
        logger.LogInformation("La contraseña no es válida {@user}", user);

        return EntityResponse<bool>.Error("Las credenciales ingresadas son incorrectas");
    }
}