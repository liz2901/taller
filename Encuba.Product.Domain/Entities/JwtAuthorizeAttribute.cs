using Encuba.Product.Domain.Seed;

namespace Encuba.Product.Domain.Entities;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class JwtAuthorizeAttribute : Attribute
{
    public JwtAuthorizeAttribute(JwtScope scope, string permissions)
    {
        Scope = scope;
        Permissions = permissions.Split(' ').ToList();
    }

    public JwtAuthorizeAttribute(JwtScope scope)
    {
        Scope = scope;
        Permissions = new List<string> { };
    }

    public JwtScope Scope { get; }
    public List<string> Permissions { get; }
}