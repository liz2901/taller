
using Encuba.Product.Domain.Dtos;

namespace Encuba.Product.Domain.Interfaces.Services;

public interface IJwtTokenService
{
    public string GenerateJwtToken(JwtPayloadDto payload);
}