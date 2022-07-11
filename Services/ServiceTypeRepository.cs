using maker_checker_v1.data;
using maker_checker_v1.models.entities;
using Microsoft.EntityFrameworkCore;

namespace maker_checker_v1.Services
{
    public class ServiceTypeRepository
    {
        private readonly RequestContext _context;

        public ServiceTypeRepository(RequestContext requestContext)
        {
            _context = requestContext;

        }

        public async Task<IEnumerable<ServiceType>> getServiceTypes()
        {

            var serviceTypes = await _context.Set<ServiceType>()
            .Include(s => s.Validation)
            .ToListAsync();
            //todo ask ahmed about this
            // var maxNbr = _context.Set<User>().Count(u => u.RoleId == 1);

            return serviceTypes;
        }
        public async Task<ServiceType?> getServiceType(int serviceTypeId)
        {
            var serviceType = await _context.Set<ServiceType>().FindAsync(serviceTypeId);
            return serviceType;
        }
        public async void Add(ServiceType serviceType)
        {
            await _context.Set<ServiceType>().AddAsync(serviceType);
        }
        public async Task<Boolean> Exists(int serviceTypeId)
        {
            return await _context.Set<ServiceType>().FirstOrDefaultAsync(s => s.Id == serviceTypeId) != null;
        }
        public async Task<Boolean> Exists(string serviceTypeName)
        {
            return await _context.Set<ServiceType>().FirstOrDefaultAsync(s => s.Name == serviceTypeName) != null;
        }

        public void Remove(ServiceType serviceType)
        {
            _context.Set<ServiceType>().Remove(serviceType);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}