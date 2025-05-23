namespace UCondoApp.Application.Queries.Responses;

public record GetAllAccountsChartResponseQuery
{
    public Guid Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string FormattedName => $"{Code} - {Name}";
};
