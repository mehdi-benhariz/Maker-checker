using maker_checker_v1.data;
using maker_checker_v1.models.DTO;
using maker_checker_v1.models.entities;
using Microsoft.EntityFrameworkCore;

namespace maker_checker_v1.Services
{
    public class RequestRepository
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
        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        internal async Task<(IEnumerable<RequetToReturn>, PagginationMetaData)> getRequestsHistory(int userId, int pageNumber, int pageSize = 5)
        {
            var collection = _context.Set<Request>() as IQueryable<Request>;
            collection = collection.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            int totalItems = await collection.CountAsync();
            var pagginationMetaData = new PagginationMetaData(pageNumber, pageSize, totalItems);
            // var collectionToReturn = await collection.Include(r => r.ServiceType).ToListAsync();

            var collectionToReturn = collection
                .Join(
                    _context.Set<ServiceType>(),
                    r => r.ServiceTypeId,
                    st => st.Id,
                    (req, st) => new RequetToReturn
                    {
                        Id = req.Id,
                        serviceType = st.Name,
                        Status = req.Status,
                        Amount = req.Amount
                    }
                ).ToList();

            return (collectionToReturn, pagginationMetaData);

        }
    }
}