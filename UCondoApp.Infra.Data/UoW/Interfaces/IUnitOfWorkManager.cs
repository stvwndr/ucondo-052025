namespace UCondoApp.Infra.Data.UoW.Interfaces;

public interface IUnitOfWorkManager
{
    IUnitOfWork BeginTran();
}
