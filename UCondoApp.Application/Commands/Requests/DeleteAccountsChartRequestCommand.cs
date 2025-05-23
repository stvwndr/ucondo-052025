using UCondoApp.Application.Commands.Responses;

namespace UCondoApp.Application.Commands.Requests;

public class DeleteAccountsChartRequestCommand : BaseCommand<DeleteAccountChartResponseCommand?>
{
    public Guid Id { get; set; }
}
