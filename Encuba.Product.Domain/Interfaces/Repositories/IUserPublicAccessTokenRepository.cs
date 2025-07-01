using Encuba.Product.Domain.Entities;
using Encuba.Product.Domain.Seed;

namespace Encuba.Product.Domain.Interfaces.Repositories;

public interface IUserPublicAccessTokenRepository : IRepository<UserPublicAccessToken>
{
    UserPublicAccessToken Add(UserPublicAccessToken userPublicAccessToken);
    void Delete(List<UserPublicAccessToken> userPublicAccessTokens);
    
    Task<UserPublicAccessToken> GetByUserIdAsync(Guid userId);
}