using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobApplicationTracker.DataAccess.Data;
using JobApplicationTracker.DataAccess.Interfaces;
using JobApplicationTracker.DataAccess.Models;


namespace JobApplicationTracker.DataAccess.Repositories
{
    public class Application: JobAppTrackerRepository<Application>, IJobAppTracker<Application>
    {
        private JobAppDbContext _context;
        public Application(JobAppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
