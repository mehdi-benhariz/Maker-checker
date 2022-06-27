using maker_checker_v1.data;
using maker_checker_v1.models.entities;
using Microsoft.EntityFrameworkCore;

namespace maker_checker_v1.Services
{
    public class ValidationRepository : IRepository
    {

        private readonly RequestContext _context;
        public ValidationRepository(RequestContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Validation>> getValidations()
        {
            var validations = await _context.Set<Validation>().ToListAsync();
            return validations;
        }
        public async Task<Validation?> getValidation(int Id)
        {
            var validation = await _context.Set<Validation>().FirstOrDefaultAsync(v => (v.Id == Id || v.ServiceTypeId == Id));
            return validation;
        }
        public async void Add(Validation validation)
        {
            await _context.Set<Validation>().AddAsync(validation);
        }
        public async Task<Boolean> Exists(int validationId)
        {
            return await _context.Set<Validation>().FirstOrDefaultAsync(s => s.Id == validationId) != null;
        }
        public void Remove(Validation validation)
        {
            _context.Set<Validation>().Remove(validation);
        }

        internal Validation? getValidationByServiceType(int serviceTypeId)
        {
            var validation = _context.Set<Validation>().FirstOrDefault(v => v.ServiceTypeId == serviceTypeId);
            return validation;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}