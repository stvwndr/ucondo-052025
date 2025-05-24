using AutoMapper;
using UCondoApp.Application.Queries.Responses;
using UCondoApp.CrossCutting.Dtos;

namespace UCondoApp.Application.Mapping;

public class UCondoApplicationMappingProfile : Profile
{
    public UCondoApplicationMappingProfile()
    {
        CreateMap<AccountsChartDto, GetAllAccountsChartResponseQuery>();
    }
}
