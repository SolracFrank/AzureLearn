using Domain.Entities;

namespace Infrastructure.Repositories.Interfaces;

public interface IUnitOfWork
{
    public IRepository<Product> ProductRepository { get; }
    public IRepository<Category> CategoryRepository { get; }
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}