using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MrFixIt.Models
{
    //IdentityDbContext is a class which extends on the DbContext class to work with user identificaiton
    public class MrFixItContext : IdentityDbContext<ApplicationUser>
    {
        public MrFixItContext()
        {
        }
        //virtual is to link both jobs and workers models.
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }
       

        //connection string too database also another connection in appsettings.JSON file
        // last step for the connection string is too add services in the startup for entity framwoerk *meaning our database.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MrFixIt;integrated security=True");
        }

        public MrFixItContext(DbContextOptions<MrFixItContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // OnModelCreating is necessary when we want to create new tables in the database.
            base.OnModelCreating(builder);
        }
    }
}