
using UCondoApp.CrossCutting.Dtos;

namespace UCondoApp.Infra.Data.Interfaces.ReadRepository;

public interface IAccountsChartReadRepository
{
    Task<bool> AnyById(Guid id);
    Task<AccountsChartDto?> GetById(Guid id);
    Task<IList<AccountsChartDto>> GetAll();
    Task<IList<AccountsChartDto>> GetAllByPartialName(string partialName);
    Task<AccountsChartDto?> GetByCode(string code);
    Task<AccountsChartDto> GetMaxCode();
    Task<IList<AccountsChartDto>> GetMaxCode(string parentCode, string rootCode);
}
