using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Encuba.Product.Infrastructure.Security;

public class JwtAuthorizationActionFilter(
    ILogger<JwtAuthorizationActionFilter> logger,
    IHttpContextAccessor httpContextAccessor
) : IAuthorizationFilter
{
    #region Public Methods

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (!string.IsNullOrWhiteSpace(environment) && environment.Equals("Local"))
        {
            return;
        }

        var authorizeAttributes = context.ActionDescriptor
            .EndpointMetadata
            .OfType<JwtAuthorizeAttribute>()
            .ToList();

        if (authorizeAttributes.Count == 0) return;

        ValidateAuthorization(context, authorizeAttributes);
    }

    #endregion


    #region Private Methods

    private string GetScope()
    {
        return httpContextAccessor.HttpContext.User.Claims
            .Where(t => t.Type.Equals("scope"))
            .Select(t => t.Value.ToString())
            .First();
    }

    private void ValidateAuthorization(AuthorizationFilterContext context,
        ICollection<JwtAuthorizeAttribute> authorizeAttributes)
    {
        var scope = GetScope();

        // Verify the Authorization
        const bool isPermissionFound = false;
        if (authorizeAttributes
            .Where(jwtAuthorizeAttribute => scope.Equals(jwtAuthorizeAttribute.Scope.ToString(),
                StringComparison.InvariantCultureIgnoreCase))
            .Any(jwtAuthorizeAttribute => jwtAuthorizeAttribute.Permissions.Count == 0))
        {
            return;
        }

        if (!isPermissionFound) context.Result = new UnauthorizedResult();
    }

    #endregion
}