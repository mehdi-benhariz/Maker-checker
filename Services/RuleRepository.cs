using maker_checker_v1.data;
using maker_checker_v1.models.entities;

namespace maker_checker_v1.Services
{
    public class RuleRepository
    {
        private readonly RequestContext _requestContext;

        public RuleRepository(RequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        public void Add(Rule rule)
        {
            _requestContext.Set<Rule>().Add(rule);
        }
        public async Task<bool> Save()
        {
            return await _requestContext.SaveChangesAsync() >= 0;
        }


    }
}