using System.Text.Json.Serialization;
using UCondoApp.Application.Commands;
using UCondoApp.Application.Queries.Responses;

namespace UCondoApp.Application.Queries.Requests;

public class GetNextCodeRequestQuery : BaseCommand<GetNextCodeResponseQuery?>
{
    public string? ParentCode { get; set; }
}
