using UCondoApp.Application.Commands;
using UCondoApp.Application.Queries.Responses;

namespace UCondoApp.Application.Queries.Requests;

public class GetAllParentAccountsChartRequestQuery : BaseCommand<IList<GetAllAccountsChartResponseQuery>>
{
}
