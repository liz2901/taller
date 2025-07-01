
using FluentValidation;

namespace Encuba.Product.Domain.Dtos;

public static class EntityResponseUtils
{
    public static List<string> GetMessageError(Exception exception)
    {
        var errors = new List<string>();

        if (exception.InnerException == null)
            return errors;

        var validationException = exception.InnerException as ValidationException;
        if (validationException != null)
        {
            errors.AddRange(validationException.Errors.Select(error => error.ToString()));
        }

        if (exception.InnerException is not ValidationException)
        {
            errors.Add(exception.InnerException?.Message!);
        }


        return errors;
    }

    public static string GenerateMsg(string msg, params object[] args)
    {
        return string.Format(msg, args);
    }
}