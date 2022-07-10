using maker_checker_v1.data;
using maker_checker_v1.models.entities;

namespace maker_checker_v1.Services
{
    public class UnitOfWork
    {
        private readonly RequestContext _context;
        private readonly GenericRepository<Request>? _requests;
        private readonly GenericRepository<Role>? _roles;
        private readonly GenericRepository<User>? _users;
        private readonly GenericRepository<Rule>? _rules;
        private readonly GenericRepository<ServiceType>? _serviceTypes;
        private readonly GenericRepository<Validation>? _validations;
        private readonly GenericRepository<Operation>? _operations;
        private readonly GenericRepository<ValidationProgress>? _validationProgress;

        public UnitOfWork(RequestContext context)
        {
            _context = context;
        }
        public GenericRepository<Request> Requests => _requests ?? new GenericRepository<Request>(_context);
        public GenericRepository<User> Users => _users ?? new GenericRepository<User>(_context);
        public GenericRepository<Role> Roles => _roles ?? new GenericRepository<Role>(_context);
        public GenericRepository<ServiceType> ServiceTypes => _serviceTypes ?? new GenericRepository<ServiceType>(_context);
        public GenericRepository<Validation> Validations => _validations ?? new GenericRepository<Validation>(_context);
        public GenericRepository<Rule> Rules => _rules ?? new GenericRepository<Rule>(_context);
        public GenericRepository<Operation> Operations => _operations ?? new GenericRepository<Operation>(_context);
        public GenericRepository<ValidationProgress> ValidationProgresses => _validationProgress ?? new GenericRepository<ValidationProgress>(_context);

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}