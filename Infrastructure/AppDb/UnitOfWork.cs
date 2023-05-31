using System.Collections;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure.AppDb;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private Hashtable _repositories;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IGenericRepository<TEntitiy> Repository<TEntitiy>() where TEntitiy : BaseEntity
    {
        if (_repositories == null) _repositories = new Hashtable();

        var type = typeof(TEntitiy).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repoType = typeof(GenericRepository<>);
            var repoInstance = Activator.CreateInstance(
                repoType.MakeGenericType(typeof(TEntitiy)), _context);

            _repositories.Add(type, repoInstance);
        }

        return (IGenericRepository<TEntitiy>)_repositories[type];
    }
}
