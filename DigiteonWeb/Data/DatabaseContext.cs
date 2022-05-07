using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiteonWeb.Data
{
    public class DatabaseContext: IdentityDbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> Options) : base(Options)
        {

        }
        public DbSet<Models.LoginResult> LoginResult { get; set; }
        protected override void OnModelCreating(ModelBuilder foModelbuilder)
        {
            foModelbuilder.Entity<Models.LoginResult>().HasNoKey();
            base.OnModelCreating(foModelbuilder);
        }
    }
}
