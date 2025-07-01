using  Encuba.Product.Application.Dtos.Responses;
using Encuba.Product.Domain.Dtos;
using  Encuba.Product.Domain.Entities;
using  Encuba.Product.Domain.Interfaces.Cryptography;
using  Encuba.Product.Domain.Interfaces.Repositories;
using  Encuba.Product.Domain.Seed;
using MediatR;

namespace  Encuba.Product.Application.Commands.UserCommands;

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IBCryptCryptographyHelper bCryptCryptographyHelper)
    : IRequestHandler<CreateUserCommand, EntityResponse<ObjectIdResponse>>
{
    public async Task<EntityResponse<ObjectIdResponse>> Handle(CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var isExistUser = await ValidateUser(command);
        if (!isExistUser.IsSuccess)
        {
            return EntityResponse<ObjectIdResponse>.Error(isExistUser.EntityErrorResponse.Message);
        }

        var newUser = await CreateUser(command, cancellationToken);
        return EntityResponse.Success(new ObjectIdResponse(newUser.Id));
    }

    private async Task<User> CreateUser(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var password = bCryptCryptographyHelper.HashWithBcrypt(command.Password, BCryptWorkFactor.Value);
        var newUser = new User(command.UserName, command.UserType, command.FirstName, command.SecondName, command.FirstLastName,
            command.SecondLastName, password,
            command.Email, true, command.AcceptedTermsAndCondition, DateTime.UtcNow);
        userRepository.Add(newUser);
        await userRepository.UnitOfWork.SaveEntityAsync(cancellationToken);
        return newUser;
    }

    #region Private methods

    private async Task<EntityResponse<bool>> ValidateUser(CreateUserCommand command)
    {
        var doesEmailExist = await userRepository.GetByUserAsync(command.Email);

        return doesEmailExist != null
            ? EntityResponse<bool>.Error($"El correo {command.Email} ya existe")
            : EntityResponse.Success(true);
    }

    #endregion
}