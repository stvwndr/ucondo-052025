using UCondoApp.Infra.Data.UoW.Interfaces;

namespace UCondoApp.Infra.Data.UoW;

public class UnitOfWorkManager : IUnitOfWorkManager
{
    public IUnitOfWork BeginTran()
    {
        return new UnitOfWork();
    }
}
