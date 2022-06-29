using maker_checker_v1.data;
using maker_checker_v1.models.entities;
using maker_checker_v1.Services;
using Microsoft.EntityFrameworkCore;

namespace maker_checker_v1
{
    public class UserRepository : IRepository
    {
        private readonly RequestContext _context;

        public UserRepository(RequestContext context)
        {
            _context = context;
        }
        public async void Add(User user)
        {
            await _context.Set<User>().AddAsync(user);
        }
        public async void AddRange(IEnumerable<User> users)
        {
            await _context.Set<User>().AddRangeAsync(users);
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(U => U.Id == id) != null;
        }
        public async Task<bool> Exists(string username)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(U => U.Username == username) != null;
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Set<User>().Include(u => u.Role).FirstOrDefaultAsync(U => U.Username == username);
        }
    }
}