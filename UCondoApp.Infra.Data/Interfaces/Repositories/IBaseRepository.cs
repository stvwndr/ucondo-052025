using UCondoApp.Infra.Data.Uow.Interfaces;

namespace UCondoApp.Infra.Data.Interfaces.Repositories;

public interface IBaseRepository<T> : IDisposable where T : class
{
    IContextUnitOfWork UnitOfWork { get; }
    Task InsertEntity(T entity);
    Task UpdateEntity(T entity);
    Task DeleteEntity(Guid id);
}
