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
    public class CheckedOnApp: JobAppTrackerRepository<CheckedOnApp>, IJobAppTracker<CheckedOnApp>
    {
        private JobAppDbContext _context;
        public CheckedOnApp(JobAppDbContext context) : base(context)
        {
            _context = context;
        }
        
        
    }
}
