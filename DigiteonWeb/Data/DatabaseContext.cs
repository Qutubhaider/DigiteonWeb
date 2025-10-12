using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolarCastleWeb.Data
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
        public DbSet<Models.Career> Career { get; set; }
        public DbSet<Models.CareerListResult> CareerListResult { get; set; }
        public DbSet<Models.CareerApplication> CareerApplication { get; set; }
        public DbSet<Models.ApplicantListResult> ApplicantListResult { get; set; }
        protected override void OnModelCreating(ModelBuilder foModelbuilder)
        {
            foModelbuilder.Entity<Models.LoginResult>().HasNoKey();
            foModelbuilder.Entity<Models.TrainingDetail>().HasNoKey();
            foModelbuilder.Entity<Models.TrainingResults>().HasNoKey();
            foModelbuilder.Entity<Models.EnrollDetails>().HasNoKey();
            foModelbuilder.Entity<Models.EnrollResults>().HasNoKey();
            foModelbuilder.Entity<Models.Career>().HasNoKey();
            foModelbuilder.Entity<Models.CareerListResult>().HasNoKey();
            foModelbuilder.Entity<Models.CareerApplication>().HasNoKey();
            foModelbuilder.Entity<Models.ApplicantListResult>().HasNoKey();
            base.OnModelCreating(foModelbuilder);
        }
    }
}
