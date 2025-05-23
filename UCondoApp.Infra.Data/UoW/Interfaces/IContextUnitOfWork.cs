namespace UCondoApp.Infra.Data.UoW.Interfaces;

public interface IContextUnitOfWork : IDisposable
{
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}
