namespace UCondoApp.Infra.Data.UoW.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task Complete();
}
