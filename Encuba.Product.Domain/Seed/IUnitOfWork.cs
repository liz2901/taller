namespace Encuba.Product.Domain.Seed;

public interface IUnitOfWork : IDisposable
{
    Task<bool> SaveEntityAsync(CancellationToken cancellationToken = default);
}