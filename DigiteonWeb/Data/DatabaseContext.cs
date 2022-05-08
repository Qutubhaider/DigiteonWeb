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
        public DbSet<Models.TrainingDetail> TrainingDetail { get; set; }
        public DbSet<Models.TrainingResults> TrainingResults { get; set; }
        public DbSet<Models.EnrollDetails> EnrollDetails { get; set; }
        public DbSet<Models.EnrollResults> EnrollResults { get; set; }
        protected override void OnModelCreating(ModelBuilder foModelbuilder)
        {
            foModelbuilder.Entity<Models.LoginResult>().HasNoKey();
            foModelbuilder.Entity<Models.TrainingDetail>().HasNoKey();
            foModelbuilder.Entity<Models.TrainingResults>().HasNoKey();
            foModelbuilder.Entity<Models.EnrollDetails>().HasNoKey();
            foModelbuilder.Entity<Models.EnrollResults>().HasNoKey();
            base.OnModelCreating(foModelbuilder);
        }
    }
}
