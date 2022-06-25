using maker_checker_v1.data;
using maker_checker_v1.models.entities;
using Microsoft.EntityFrameworkCore;

namespace maker_checker_v1.Services
{
    public class RoleRepository : IRepository
    {
        private readonly RequestContext _context;

        public RoleRepository(RequestContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> getRoles()
        {
            var roles = await _context.Set<Role>().ToListAsync();
            return roles;
        }
        public async Task<Role?> getRole(int roleId)
        {
            var role = await _context.Set<Role>().FindAsync(roleId);
            return role;
        }
        public async void Add(Role role)
        {
            await _context.Set<Role>().AddAsync(role);
        }
        public async Task<Boolean> Exits(string roleName)
        {
            return await _context.Set<Role>().FirstOrDefaultAsync(s => s.Name == roleName) != null;
        }
        public void Remove(Role role)
        {
            _context.Set<Role>().Remove(role);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }


    }
}