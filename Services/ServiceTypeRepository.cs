using maker_checker_v1.data;
using maker_checker_v1.models.entities;

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
            var serviceTypes = _context.Set<ServiceType>();
            return serviceTypes;
        }
        public async Task<ServiceType?> getServiceType(int serviceTypeId)
        {
            var serviceType = await _context.Set<ServiceType>().FindAsync(serviceTypeId);
            return serviceType;
        }
        public void Add(ServiceType serviceType)
        {
            _context.Set<ServiceType>().Add(serviceType);
            // _context.ServiceTypes.Add(serviceType);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}