using AutoMapper;
using maker_checker_v1.Controllers;
using maker_checker_v1.models.entities;

namespace maker_checker_v1.Profiles
{
    public class RuleProfile : Profile
    {
        public RuleProfile()
        {

            CreateMap<RuleForCreationDTO, Rule>().ConvertUsing(
                (source, context) =>
                {
                    var rule = new Rule()
                    {
                        RoleId = source.RoleId,
                        Nbr = source.Nbr,
                    };
                    return rule;
                }
            );
            //!not sure about this one
            CreateMap<Rule, RuleToAdmin>()
                .ForMember(
                dest => dest.RoleName,
                opt => opt.MapFrom(src => src.Role.Name)
                )
                .ForMember(
                    dest => dest.RoleId,
                    opt => opt.MapFrom(src => src.Role.Id)
                )
                .ForMember(
                    dest => dest.MaxNbr,
                    opt => opt.MapFrom(src => src.Role.Users.Count)
                );


        }

    }
}