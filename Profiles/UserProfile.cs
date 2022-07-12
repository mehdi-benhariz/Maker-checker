using AutoMapper;
using maker_checker_v1.models.DTO.Return;
using maker_checker_v1.models.entities;

namespace maker_checker_v1.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //convert Role in User to string
            // CreateMap<User, UserToReturn>()
            //     .ForMember(dest => dest.Role, opt => opt.MapFrom<RoleResolver>());
            CreateMap<User, UserToReturn>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(u => u.Role.Name));
            CreateMap<User, StaffUser>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(u => u.Role.Name))
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(u => u.CreationDate.ToString("dd-MM-yyyy")));
        }

    }
}