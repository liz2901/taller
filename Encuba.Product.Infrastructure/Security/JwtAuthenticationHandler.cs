using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Encuba.Product.Infrastructure.Security;

public class JwtAuthenticationSchemaOptions : AuthenticationSchemeOptions
{
}

public class JwtAuthenticationHandler(
    IOptionsMonitor<JwtAuthenticationSchemaOptions> options,
    ILoggerFactory loggerFactory,
    UrlEncoder encoder,
    ISystemClock clock,
    IOptions<JwtAuthenticationSettings> jwtAuthSettings)
    : AuthenticationHandler<JwtAuthenticationSchemaOptions>(options,
        loggerFactory, encoder, clock)
{
    #region Public Methods

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var value))
            return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));

        string authorizationHeader = value!;

        var authenticateResult = ValidateAuthorizationHeader(authorizationHeader!);

        return Task.FromResult(authenticateResult ?? ValidateAuthenticationToken(authorizationHeader!));
    }

    #endregion

    #region Constructor & Properties

    private readonly ILogger<JwtAuthenticationHandler> _logger = loggerFactory.CreateLogger<JwtAuthenticationHandler>();

    #endregion

    #region Private Methods

    private static AuthenticateResult ValidateAuthorizationHeader(string authorizationHeader)
    {
        if (string.IsNullOrEmpty(authorizationHeader)) return AuthenticateResult.NoResult();

        if (!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
            return AuthenticateResult.Fail("Unauthorized");

        var token = authorizationHeader.Substring("bearer".Length).Trim();

        return (string.IsNullOrEmpty(token)
            ? AuthenticateResult.Fail("Unauthorized")
            :
            // Return null if validation is True
            null)!;
    }

    private AuthenticateResult ValidateAuthenticationToken(string authorizationHeader)
    {
        try
        {
            var token = authorizationHeader.Substring("bearer".Length).Trim();
            return HandleToken(token, jwtAuthSettings.Value.Secret);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There was an error handling jwt token");
            return AuthenticateResult.Fail(ex.Message);
        }
    }

    private AuthenticateResult HandleToken(string token, string secret)
    {
        var validatedPrincipal = ValidateToken(token, secret);

        var principal = new GenericPrincipal(validatedPrincipal.Identity!, null);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }

    private static ClaimsPrincipal ValidateToken(string authToken, string secret)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateLifetime = false, // Because there is no expiration in the generated token
            ValidateAudience = false, // Because there is no audiance in the generated token
            ValidateIssuer = false, // Because there is no issuer in the generated token,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(secret)) // The same key as the one that generate the token
        };

        return tokenHandler.ValidateToken(authToken, validationParameters, out _);
    }

    #endregion
}