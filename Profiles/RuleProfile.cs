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


        }

    }
}