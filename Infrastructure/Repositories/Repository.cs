using System.Linq.Expressions;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id,CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task AddAsync(T entity,CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public void Update(T entity,CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);
    }

    public void Remove(T entity,CancellationToken cancellationToken)
    {
        _dbSet.Remove(entity);
    }
}