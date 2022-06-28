using maker_checker_v1.data;

namespace maker_checker_v1.Services
{
    public class UnitOfWork<TEntity> where TEntity : class
    {
        private readonly RequestContext _context;

        public UnitOfWork(RequestContext context)
        {
            _context = context ?? throw new NullReferenceException(nameof(context));
        }
        public async Task<Boolean> Exists<T>(int Id)
        {
            return true;
            // return _context.Set<T>();
        }
        public async Task<Boolean> Save()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}