using System.Data;

namespace UCondoApp.Infra.Data.ReadRepositories;

public abstract class BaseReadRepository
{
    protected readonly IDbConnection DbConnection;

    protected BaseReadRepository(IDbConnection dbConnection)
    {
        DbConnection = dbConnection;
    }
}
