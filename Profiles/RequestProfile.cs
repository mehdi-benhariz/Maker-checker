using AutoMapper;
using maker_checker_v1.models.DTO;
using maker_checker_v1.models.entities;

namespace maker_checker_v1.Profiles
{
    public class RequestProfile : Profile
    {
        public RequestProfile()
        {
            CreateMap<RequestForCreationDTO, Request>();
            CreateMap<Request, RequestToClient>().ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate.ToString("dd-MM-yyyy")));
        }
    }
}