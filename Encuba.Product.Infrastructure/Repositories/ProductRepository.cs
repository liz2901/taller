using Encuba.Product.Domain.Interfaces.Repositories;
using Encuba.Product.Domain.Seed;
using Microsoft.EntityFrameworkCore;

namespace Encuba.Product.Infrastructure.Repositories;

public class ProductRepository(SecurityDbContext dbContext) : IProductRepository
{
    public IUnitOfWork UnitOfWork => dbContext;

    public Domain.Entities.Product Add(Domain.Entities.Product product)
    {
        return dbContext.Add(product).Entity;
    }

    public Domain.Entities.Product Update(Domain.Entities.Product product)
    {
        return dbContext.Update(product).Entity;
    }

    public void Delete(Domain.Entities.Product product)
    {
        dbContext.Remove(product);
    }

    public async Task<Domain.Entities.Product> GetById(Guid id)
    {
        return await dbContext.Products.FirstAsync(x => x.Id == id);
    }

    public async Task<List<Domain.Entities.Product>> GetAll()
    {
        return await dbContext.Products.ToListAsync();
    }
}