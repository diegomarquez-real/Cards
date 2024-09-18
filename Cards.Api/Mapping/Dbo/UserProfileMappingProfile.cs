using AutoMapper;

namespace Cards.Api.Mapping.Dbo
{
    public class UserProfileMappingProfile : Profile
    {
        public UserProfileMappingProfile()
        {
            CreateMap<Data.Models.Dbo.UserProfile, Models.Dbo.UserProfileModel>();
            CreateMap<Models.Dbo.Create.CreateUserProfileModel, Data.Models.Dbo.UserProfile>();
            CreateMap<Models.Dbo.Update.UpdateUserProfileModel, Data.Models.Dbo.UserProfile>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}