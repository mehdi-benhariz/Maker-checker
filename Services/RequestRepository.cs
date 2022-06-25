using maker_checker_v1.data;
using maker_checker_v1.models.entities;
using Microsoft.EntityFrameworkCore;

namespace maker_checker_v1.Services
{
    public class RequestRepository : IRepository
    {
        private readonly RequestContext _context;
        public RequestRepository(RequestContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Request>> getRequests()
        {
            var requests = await _context.Set<Request>().ToListAsync();
            return requests;
        }
        public async Task<IEnumerable<Request>> GetRequests(string? name, string? searchQuery)
        {
            var collection = _context.Set<Request>() as IQueryable<Request>;
            if (!string.IsNullOrEmpty(searchQuery))
                collection = collection.Where(s => s.Name.Contains(searchQuery));
            if (!string.IsNullOrEmpty(name))
                collection = collection.Where(s => s.Name.Contains(name));

            return await collection.ToListAsync();
        }
        public async Task<Request?> getRequest(int requestId)
        {
            var request = await _context.Set<Request>().Include(r => r.ServiceType).IgnoreAutoIncludes().FirstOrDefaultAsync(r => r.Id == requestId);
            return request;
        }
        public async void Add(Request request)
        {
            await _context.Set<Request>().AddAsync(request);
            // await _context.Set<ValidationProgress>().AddAsync(new ValidationProgress(request.Id)
            // {
            //     Rules = request?.ServiceType?.Validation?.Rules ?? new List<Rule>()
            // });
        }
        public async Task<Boolean> Exits(int requestId)
        {
            return await _context.Set<Request>().FirstOrDefaultAsync(s => s.Id == requestId) != null;
        }
        public void Remove(Request request)
        {
            _context.Set<Request>().Remove(request);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

    }
}