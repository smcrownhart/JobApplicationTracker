using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobApplicationTracker.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTracker.DataAccess.Data
{
    public class JobAppDbContext : DbContext
    {
        public JobAppDbContext(DbContextOptions<JobAppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyContact> CompanyContacts { get; set; }

        public DbSet<CheckedOnApp> CheckedOnApp { get; set; }

        public DbSet<Interviews> Interviews { get; set; }

        public DbSet<InterviewPrep> InterviewPreps { get; set; }
    }
}
