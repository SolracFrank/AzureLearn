namespace Infrastructure.Repositories.Interfaces;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}