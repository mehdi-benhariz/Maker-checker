using AutoMapper;
using maker_checker_v1.models.DTO;
using maker_checker_v1.models.entities;

namespace maker_checker_v1
{
    public class DateTimeResolver : IValueResolver<Request, RequestToClient, string>
    {
        public string Resolve(Request source, RequestToClient destination, string destMember, ResolutionContext context) => source.CreationDate.ToString("dd/MM/yyyy HH:mm");
    }
    public class RoleResolver : IValueResolver<User, UserToReturn, string>
    {
        public string Resolve(User source, UserToReturn destination, string destMember, ResolutionContext context) => source.Role.Name;
    }
    //!not finished yet
    public class ServiceTypeResolver : IValueResolver<ServiceType, ServiceTypeToAdmin, List<RuleToAdmin>>
    {

        public List<RuleToAdmin> Resolve(ServiceType source, ServiceTypeToAdmin destination, List<RuleToAdmin> destMember, ResolutionContext context)
        {
            foreach (var item in source.Validation!.Rules)
            {
                destMember.Add(new RuleToAdmin
                {
                    Id = item.Id,
                    RoleId = item.Role.Id,
                    RoleName = item.Role.Name,
                    Nbr = item.Nbr,
                    MaxNbr = item.Role.Users.Count()
                });

            }
            return destMember;

        }
    }
}
