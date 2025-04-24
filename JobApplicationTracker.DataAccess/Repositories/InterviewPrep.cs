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
    public class InterviewPrep:JobAppTrackerRepository<InterviewPrep>, IJobAppTracker<InterviewPrep>
    {
        private JobAppDbContext _context;
        public InterviewPrep(JobAppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
