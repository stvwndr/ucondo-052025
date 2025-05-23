using System.Transactions;
using UCondoApp.Infra.Data.UoW.Interfaces;

namespace UCondoApp.Infra.Data.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly TransactionScope _transactionScope;

    public UnitOfWork()
    {
        _transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
    }

    public Task Complete()
    {
        _transactionScope.Complete();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _transactionScope.Dispose();
        GC.SuppressFinalize(this);
    }
}
