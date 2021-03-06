using AutoMapper;
using maker_checker_v1.models.entities;
using maker_checker_v1.models.DTO;
using maker_checker_v1.Controllers;

namespace maker_checker_v1.Profiles
{
    public class ServiceTypeProfile : Profile
    {
        public ServiceTypeProfile()
        {

            CreateMap<ServiceType, ServiceTypeToAdmin>()
            .ForMember(
                dest => dest.Rules,
                opt => opt.MapFrom(src => src.Validation!.Rules)
            );

        }
    }
}