using Microsoft.EntityFrameworkCore;
using UCondoApp.Domain.Entitites;
using UCondoApp.Infra.Data.Contexts;
using UCondoApp.Infra.Data.Interfaces.Repositories;
using UCondoApp.Infra.Data.UoW.Interfaces;

namespace UCondoApp.Infra.Data.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly AccountsChartDbContext _context;
    private readonly DbSet<T> _dbSet;

    public IContextUnitOfWork UnitOfWork => _context;

    protected BaseRepository(AccountsChartDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task InsertEntity(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public Task UpdateEntity(T entity)
    {
        _dbSet.Update(entity);

        return Task.CompletedTask;
    }

    public async Task DeleteEntity(Guid id)
    {
        T entity = (await _dbSet.FindAsync(id))!;

        _dbSet.Remove(entity);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
