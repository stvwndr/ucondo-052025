using System.ComponentModel.DataAnnotations;
using UCondoApp.Application.Commands;
using UCondoApp.Application.Queries.Responses;

namespace UCondoApp.Application.Queries.Requests;

public class GetAllAccountsChartByPartialNameRequestQuery : BaseCommand<IList<GetAllAccountsChartResponseQuery>>
{
    [Required(ErrorMessage = "O atributo 'PartialName' deve ser informado.")]
    public string? PartialName { get; set; }
}
