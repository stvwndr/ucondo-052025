namespace UCondoApp.Infra.Data.Uow.Interfaces;

public interface IContextUnitOfWork : IDisposable
{
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}
