using System.Linq.Expressions;
using maker_checker_v1.data;
using Microsoft.EntityFrameworkCore;

namespace maker_checker_v1
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        private readonly RequestContext _context;
        private readonly DbSet<TEntity> _db;

        public GenericRepository(RequestContext context)
        {
            _context = context ?? throw (new ArgumentNullException(nameof(context)));
            _db = _context.Set<TEntity>();
        }
        public async Task<IList<TEntity>> GetAll(Expression<Func<TEntity, bool>>? expression = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, List<string>? includes = null)
        {
            IQueryable<TEntity> query = _db;
            if (expression != null)
                query = query.Where(expression);
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            if (orderBy != null)
                query = orderBy(query);
            return await query.AsNoTracking().ToListAsync();
        }
        public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> expression, List<string>? includes = null)
        {
            IQueryable<TEntity> query = _db;
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }
        public async Task Insert(TEntity entity)
        {
            await _db.AddAsync(entity);
        }
        public async Task InsertRange(IEnumerable<TEntity> entities)
        {
            await _db.AddRangeAsync(entities);
        }


        public void Update(TEntity entity)
        {
            _db.Attach(entity);

        }
        public void Delete(TEntity entity)
        {
            _db.Remove(entity);
        }
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _db.RemoveRange(entities);
        }
        public Task<Boolean> Exists(Expression<Func<TEntity, bool>> expression)
        {
            return _db.AnyAsync(expression);
        }

    }
}