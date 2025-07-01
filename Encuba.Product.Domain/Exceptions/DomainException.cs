using System.Net;

namespace Encuba.Product.Domain.Exceptions;

public class DomainException : Exception
{
    public HttpStatusCode? HttpStatusCode { get; private set; }

    public DomainException()
    {
    }

    public DomainException(string message)
        : base(message)
    {
    }

    public DomainException(string message, HttpStatusCode? httpStatusCode)
        : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }

    public DomainException(string message, Exception exception)
        : base(message, exception)
    {
    }

    public DomainException(string message, HttpStatusCode? httpStatusCode, Exception exception)
        : base(message, exception)
    {
        HttpStatusCode = httpStatusCode;
    }
}