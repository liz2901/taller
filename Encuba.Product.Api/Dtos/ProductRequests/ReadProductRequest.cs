using Encuba.Product.Application.Queries.ProductQueries;

namespace Encuba.Product.Api.Dtos.ProductRequests;

public class ReadProductRequest
{
    public ReadProductQuery ToApplicationRequest(Guid id)
    {
        return new ReadProductQuery(id);
    }
}