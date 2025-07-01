using  Encuba.Product.Application.Dtos.Responses;
using Encuba.Product.Domain.Dtos;
using MediatR;

namespace  Encuba.Product.Application.Commands.UserCommands;

public record CreateUserCommand(
    string UserName,
    string UserType,
    string FirstName,
    string SecondName,
    string FirstLastName,
    string SecondLastName,
    string Password,
    string Email,
    bool AcceptedTermsAndCondition
) : IRequest<EntityResponse<ObjectIdResponse>>;