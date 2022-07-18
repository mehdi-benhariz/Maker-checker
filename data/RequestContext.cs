using System.Reflection;
using maker_checker_v1.configurations;
using maker_checker_v1.models.entities;
using Microsoft.EntityFrameworkCore;

namespace maker_checker_v1.data
{
    public class RequestContext : DbContext
    {
        // 
        // public DbSet<Request> Requests { get; set; }
        public RequestContext(DbContextOptions<RequestContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }



    }
}