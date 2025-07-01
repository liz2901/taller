using  Encuba.Product.Application.Commands.UserCommands;

namespace  Encuba.Product.Api.Dtos.UserRequests;

public record CreateUserRequest(
    string UserName,
    string UserType,
    string FirstName,
    string SecondName,
    string FirstLastName,
    string SecondLastName,
    string Password,
    string Email,
    bool AcceptedTermsAndCondition)
{
    public CreateUserCommand ToApplicationRequest()
    {
        return new CreateUserCommand(UserName, UserType, FirstName, SecondName, FirstLastName, SecondLastName, Password, Email,
            AcceptedTermsAndCondition);
    }
}