using AutoMapper;
using maker_checker_v1.Controllers;
using maker_checker_v1.models.entities;

namespace maker_checker_v1.Profiles
{
    public class ValidationProfile : Profile
    {
        public ValidationProfile()
        {
            CreateMap<Validation, ValidationForCreationDTO>();
            CreateMap<ValidationForCreationDTO, Validation>().ConvertUsing(
                (source, context) =>
                {
                    var validation = new Validation();
                    validation.Rules = source.Rules.Select(r => new Rule()
                    {
                        Role = new Role(r.Role),
                        Nbr = r.Nbr,
                        ValidationId = validation.Id
                    }).ToList();
                    return validation;
                }
            );
        }

    }
}