using Encuba.Product.Application.Queries.ProductQueries;

namespace Encuba.Product.Api.Dtos.ProductRequests;

public class ReadProductsRequest
{
    public ReadProductsQuery ToApplicationRequest()
    {
        return new ReadProductsQuery();
    }
}