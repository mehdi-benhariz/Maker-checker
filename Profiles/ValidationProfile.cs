using AutoMapper;
using maker_checker_v1.Controllers;
using maker_checker_v1.models.entities;

namespace maker_checker_v1.Profiles
{
    public class ValidationProfile : Profile
    {
        public ValidationProfile()
        {
            CreateMap<ValidationForCreationDTO, Validation>();
        }

    }
}