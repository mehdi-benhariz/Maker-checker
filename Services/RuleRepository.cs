using maker_checker_v1.data;
using maker_checker_v1.models.entities;
using Microsoft.EntityFrameworkCore;

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

        internal void AddRange(Rule[] rules)
        {
            _requestContext.Set<Rule>().AddRange(rules);
        }

        public void Set(Rule rule)
        {
            _requestContext.Set<Rule>().Update(rule);

        }

        public async Task<IEnumerable<Rule>> GetRules(int id)
        {
            return await _requestContext.Set<Rule>().Include(r => r.Role).Where(r => r.ValidationId == id).ToListAsync();
        }
    }
}