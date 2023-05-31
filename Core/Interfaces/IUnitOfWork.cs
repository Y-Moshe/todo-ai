using Core.Entities;

namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntitiy> Repository<TEntitiy>() where TEntitiy : BaseEntity;
    Task<int> SaveChangesAsync();
}
