using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork( ApplicationDbContext context, IRepository<Product> productRepository, IRepository<Category> categoryRepository)
    {
        _context = context;
        ProductRepository = productRepository;
        CategoryRepository = categoryRepository;
    }
    public IRepository<Product> ProductRepository { get; }
    public IRepository<Category> CategoryRepository { get; }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken) > 0;
        
    }
}