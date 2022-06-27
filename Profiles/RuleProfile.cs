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
                    var rule = new Rule();
                    rule.Role = new Role(source.Role);
                    rule.Nbr = source.Nbr;
                    return rule;
                }
            );
            CreateMap<Rule, RuleForCreationDTO>().ConvertUsing(
                (source, context) =>
                {
                    var rule = new RuleForCreationDTO();
                    rule.Role = source.Role.Name;
                    rule.Nbr = source.Nbr;
                    return rule;
                }
            );

        }

    }
}