using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Encuba.Product.Domain.Dtos;
using Encuba.Product.Domain.Interfaces.Services;
using Encuba.Product.Domain.Seed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Encuba.Product.Infrastructure.Security;

public class JwtTokenService : IJwtTokenService
{
    private ILogger<JwtTokenService> _logger;
    private readonly JWT _jwtSecret;

    public JwtTokenService(ILogger<JwtTokenService> logger, IConfiguration config, IOptionsMonitor<JWT> jwtSecret)
    {
        _logger = logger;
        _jwtSecret = jwtSecret.CurrentValue;
    }

    public string GenerateJwtToken(JwtPayloadDto payload)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSecret.Secret!));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var secToken = new JwtSecurityToken(null, null, null, null,
            null, signingCredentials)
        {
            Payload =
            {
                [nameof(JwtPayloadDto.Id)] = payload.Id,
                [nameof(JwtPayloadDto.FullName)] = payload.FullName,
                [nameof(JwtPayloadDto.Scope)] = payload.Scope,
            }
        };

        _logger.LogInformation("Creating security token for {@Token}", secToken);

        var jwtToken = tokenHandler.WriteToken(secToken);
        return jwtToken;
    }
}