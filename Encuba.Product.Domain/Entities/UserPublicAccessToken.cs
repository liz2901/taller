using Encuba.Product.Domain.Seed;

namespace Encuba.Product.Domain.Entities;

public class UserPublicAccessToken : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid PublicAccessTokenId { get; set; }
    public PublicAccessToken PublicAccessToken { get; set; }

    public string ClientIp { get; set; }
    public string UserAgent { get; set; }

    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; set; }

    protected UserPublicAccessToken()
    {
        Id = Guid.NewGuid();
    }

    public UserPublicAccessToken(Guid userId, Guid publicAccessTokenId, string clientIp, string userAgent)
        : this()
    {
        UserId = userId;
        PublicAccessTokenId = publicAccessTokenId;
        ClientIp = clientIp;
        UserAgent = userAgent;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
}