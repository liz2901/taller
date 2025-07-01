using Encuba.Product.Domain.Seed;

namespace Encuba.Product.Domain.Entities;

public class PublicAccessToken : BaseEntity
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string Scope { get; set; }
    public DateTime ExpiresIn { get; set; }
    public DateTime CreatedAt { get; }

    protected PublicAccessToken()
    {
        Id = Guid.NewGuid();
    }

    public PublicAccessToken(string accessToken, string refreshToken, string scope, DateTime expiresIn)
        : this()
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        Scope = scope;
        ExpiresIn = expiresIn;
        CreatedAt = DateTime.UtcNow;
    }

    #region Public methods

    /// <summary>
    ///     Create new client public access token based on expiration properties
    /// </summary>
    /// <returns></returns>
    public static PublicAccessToken CreateNewClientPublicAccessToken()
    {
        //Expires is based on politics defined in PublicAccessTokenExpiresIn
        var expiresIn = DateTime.UtcNow.AddMinutes(PublicAccessTokenExpiresIn.Minutes);

        // Access and refresh token should be an UUIDv4
        return new PublicAccessToken(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(),
            Scopes.PublicAccessTokenScopes.Client, expiresIn);
    }

    /// <summary>
    ///     Create new worker public access token based on expiration properties
    /// </summary>
    /// <returns></returns>
    public static PublicAccessToken CreateNewWorkerPublicAccessToken()
    {
        //Expires is based on politics defined in PublicAccessTokenExpiresIn
        var expiresIn = DateTime.UtcNow.AddMinutes(PublicAccessTokenExpiresIn.Minutes);

        // Access and refresh token should be an UUIDv4
        return new PublicAccessToken(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(),
            Scopes.PublicAccessTokenScopes.Worker, expiresIn);
    }
    
    /// <summary>
    ///     Create new worker public access token based on expiration properties
    /// </summary>
    /// <returns></returns>
    public static PublicAccessToken CreateNewAdministratorPublicAccessToken()
    {
        //Expires is based on politics defined in PublicAccessTokenExpiresIn
        var expiresIn = DateTime.UtcNow.AddMinutes(PublicAccessTokenExpiresIn.Minutes);

        // Access and refresh token should be an UUIDv4
        return new PublicAccessToken(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(),
            Scopes.PublicAccessTokenScopes.Administrator, expiresIn);
    }

    #endregion
}