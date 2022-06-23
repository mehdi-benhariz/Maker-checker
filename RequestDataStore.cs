using maker_checker_v1.models.entities;

namespace maker_checker_v1
{
    public class RequestDataStore
    {
        public List<Request> Requests = new List<Request>();
        public List<Role> Roles = new List<Role>();
        public List<ServiceType> ServiceTypes = new List<ServiceType>();
        public RequestDataStore()
        {
            //seed data
            ServiceTypes.Add(new ServiceType("international")
            {
                validation = new Validation(ServiceTypes.Count)
                {
                    rules = new List<Rule>{
                        new Rule("A",2),
                        new Rule("B",2),
                        new Rule("C")
                    }
                }

            });

            ServiceTypes.Add(new ServiceType("intrabank")
            {
                validation = new Validation(ServiceTypes.Count)
                {
                    rules = new List<Rule>{
                        new Rule("A",1),
                        new Rule("B",2),
                        new Rule("C",2)
                    }
                }
            }
            );

            Roles.Add(new Role("A"));
            Roles.Add(new Role("B"));
            Roles.Add(new Role("C"));

        }
        public bool ServiceTypeExists(int serviceTypeId)
        {
            return ServiceTypes.Find(s => s.Id == serviceTypeId) != null;
        }
        public bool RoleExits(string roleName)
        {
            return Roles.Find(r => r.Name == roleName) != null;
        }
        public byte GetRoleMaxNbr(string roleName)
        {
            //todo later get max nbr from role table
            var MaxNbr = roleName switch
            {
                "A" => 2,
                "B" => 3,
                "C" => 4,
                _ => 0,
            };
            // var MaxNbr = Roles.Count(r => r.Name == roleName);
            return (byte)MaxNbr;
        }


    }
}