using AutoMapper;

namespace Cards.Api.Mapping
{
    public class QueryMappingProfile : Profile
    {
        public QueryMappingProfile()
        {
            CreateMap<Models.QueryModel, Data.Models.Query>();
        }
    }
}