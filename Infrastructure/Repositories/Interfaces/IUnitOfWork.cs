namespace Infrastructure.Repositories.Interfaces;

public interface IUnitOfWork
{
    IRepository<Student> StudentsRepository { get; } 
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}