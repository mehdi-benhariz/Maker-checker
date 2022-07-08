using AutoMapper;
using maker_checker_v1.models.entities;

namespace maker_checker_v1.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserToReturn>();
        }

    }
}