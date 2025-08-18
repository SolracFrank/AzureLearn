using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(IRepository<Student> studentsRepository, ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken) > 0;
        
    }
}