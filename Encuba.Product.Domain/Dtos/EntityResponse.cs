namespace Encuba.Product.Domain.Dtos;

public class EntityResponse
{
    public bool IsSuccess { get; set; } = true;
    public bool HasError { get; set; } = false;
    public EntityErrorResponse EntityErrorResponse { get; set; }

    public static EntityResponse<T> Success<T>(T value)
    {
        return new EntityResponse<T>
        {
            IsSuccess = true,
            HasError = false,
            Value = value,
            EntityErrorResponse = null!
        };
    }
}

public class EntityResponse<T> : EntityResponse
{
    public T Value { get; set; }

    public static EntityResponse<T> Error(string message)
    {
        return new EntityResponse<T>
        {
            IsSuccess = false,
            HasError = true,
            EntityErrorResponse = new EntityErrorResponse
            {
                Code = -1,
                Message = message,
                Errors = new List<string>()
            }
        };
    }

    public static EntityResponse<T> Error(EntityResponse entityResponse)
    {
        return new EntityResponse<T>
        {
            IsSuccess = false,
            HasError = true,
            EntityErrorResponse = entityResponse.EntityErrorResponse
        };
    }

    public static EntityResponse<T> Error(Exception exception)
    {
        return new EntityResponse<T>
        {
            IsSuccess = false,
            HasError = true,
            EntityErrorResponse = new EntityErrorResponse
            {
                Code = exception.GetHashCode(),
                Message = exception.Message,
            }
        };
    }
}