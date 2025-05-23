using AutoMapper;
using UCondoApp.Application.Queries.Responses;
using UCondoApp.CrossCutting.Dtos;

namespace UCondoApp.Application.Mapping;

internal class UCondoApplicationMappingProfile : Profile
{
    internal UCondoApplicationMappingProfile()
    {
        CreateMap<AccountsChartDto, GetAllAccountsChartResponseQuery>();
    }
}
