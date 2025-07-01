using Encuba.Product.Domain.Seed;

namespace Encuba.Product.Domain.Interfaces.Repositories;

public interface IProductRepository: IRepository<Entities.Product>
{
    Entities.Product Add(Entities.Product product);
    Entities.Product Update(Entities.Product product);
    void Delete(Entities.Product product);
    Task<Entities.Product> GetById(Guid id);
    Task<List<Entities.Product>> GetAll();
}