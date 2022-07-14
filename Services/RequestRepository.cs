using maker_checker_v1.data;
using maker_checker_v1.models.DTO;
using maker_checker_v1.models.DTO.Return;
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
        public async Task<IEnumerable<Request>> GetRequests(string? desc, string? searchQuery)
        {
            var collection = _context.Set<Request>() as IQueryable<Request>;
            if (!string.IsNullOrEmpty(searchQuery))
                collection = collection.Where(s => s.Description != null & s.Description!.Contains(searchQuery));
            if (!string.IsNullOrEmpty(desc))
                collection = collection.Where(s => s!.Description!.Contains(desc));

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

        internal async Task<(IEnumerable<RequestToClient>, PagginationMetaData)> getRequestsHistory(int userId, int pageNumber, int pageSize = 5)
        {
            var collection = _context.Set<Request>() as IQueryable<Request>;
            collection = collection.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            collection = collection.OrderByDescending(r => r.CreationDate);
            int totalItems = await collection.CountAsync();
            var pagginationMetaData = new PagginationMetaData(pageNumber, pageSize, totalItems);

            var collectionToReturn = collection
                .Join(
                    _context.Set<ServiceType>(),
                    r => r.ServiceTypeId,
                    st => st.Id,
                    (req, st) => new RequestToClient
                    {
                        Id = req.Id,
                        serviceType = st.Name,
                        Status = req.Status,
                        Amount = req.Amount,
                        CreationDate = req.CreationDate.ToString("dd-MM-yyyy"),
                    }
                )
                .ToList();

            return (collectionToReturn, pagginationMetaData);

        }

        public async Task<(IEnumerable<RequestToAdmin>, PagginationMetaData)> getRequestsForAdmin(string search = "", int pageNumber = 1, int pageSize = 5)
        {
            var collection = _context.Set<Request>()
                   .Include(r => r.ServiceType)
                   .Include(r => r.User)
                .Include(r => r.ValidationProgress) as IQueryable<Request>;
            collection = collection.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            collection = collection.OrderByDescending(r => r.CreationDate);
            if (!string.IsNullOrEmpty(search))
                collection = collection.Where(r => (r.Description != null && r.Description.Contains(search)) || r.User.Username.Contains(search));

            int totalItems = await collection.CountAsync();
            var pagginationMetaData = new PagginationMetaData(pageNumber, pageSize, totalItems);

            List<RequestToAdmin> collectionToReturn = new List<RequestToAdmin>();
            foreach (Request item in collection)
            {
                byte progress = 0;
                if (item.ValidationProgress != null)
                    progress = (byte)item.ValidationProgress.Progress();

                collectionToReturn.Add(new RequestToAdmin
                {
                    Id = item.Id,
                    owner = item.User.Username,
                    serviceType = item.ServiceType.Name,
                    Status = item.Status,
                    Amount = item.Amount,
                    Progress = progress,
                    CreationDate = item.CreationDate.ToString("dd-MM-yyyy"),
                });
            }

            return (collectionToReturn, pagginationMetaData);
        }

        public IEnumerable<RequestToStaff> getRequestsForStaff(int RoleId, int UserId)
        {
            var collection = _context.Set<Request>()
                               .Include(r => r.ServiceType)
                               .ThenInclude(st => st.Validation)
                               .ThenInclude(v => v.Rules)
                               .Include(r => r.User)
                            .Include(r => r.ValidationProgress) as IQueryable<Request>;
            //*step 1 : collection where Rules include any RoleName
            collection = collection.Where(r => r.Status == "Pending" && r.ServiceType.Validation!.Rules.Any(rule => rule.RoleId == RoleId));
            //*step 2 : collection where operations with RoleName are less than Nbr from Rule

            // collection = collection.Where(r => r.ServiceType.Validation!.Rules.Any(rule => rule.Nbr <
            //       r.ValidationProgress!.Operations.Count(op => op.User!.RoleId == RoleId)));

            collection = collection.Where(r => r.ServiceType.Validation!.Rules
                .Any(rule => rule.Nbr > r.ValidationProgress!.Operations.Count(op => op.User!.RoleId == RoleId)));

            //*step 3 : collection where operations wasn't made by that userId 
            collection = collection.Where(r => !r.ValidationProgress!.Operations.Any(op => op.User!.Id == UserId));
            List<RequestToStaff> collectionToReturn = new List<RequestToStaff>();
            foreach (Request item in collection)
            {
                byte progress = 0;
                if (item.ValidationProgress != null)
                    progress = (byte)item.ValidationProgress.Progress();

                collectionToReturn.Add(new RequestToStaff
                {
                    Id = item.Id,
                    Owner = item.User.Username,
                    ServiceType = item.ServiceType.Name,
                    Amount = item.Amount,
                    Description = item.Description,
                    CreationDate = item.CreationDate.ToString("dd-MM-yyyy"),
                });
            }
            return collectionToReturn;
        }
    }
}