using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobApplicationTracker.DataAccess.Models;

namespace JobAppTracker.DataAccess.Tests;

[TestClass]
public class JobAppTrackerInterviewPrepTests : JobAppTrackerRepositoryTestBase<InterviewPrep>
{
   protected override InterviewPrep AddAnEntity()
    {

        var application = new Application
        {
            
            JobTitle = "InterviewPrep Test",
            JobDescription = "InterviewPrep Test Description",
            Status = "Applied",
            ApplicationDate = DateTime.Now,
            CompanyId = 0
        };
        _context.Applications.Add(application);
        _context.SaveChanges();

        var interview = new Interviews
        {
            
            InterviewDate = DateTime.Now.AddDays(1),
            Location = "Test Location",
            ApplicationId = application.Id
        };

        

        var interviewPrep = new InterviewPrep
        {
            
            PrepNotes = "Test Prep Notes",
            CompanyNotes = "Test Company Notes",
            InterviewsId = interview.Id,
   
        };
        
        return interviewPrep;
    }
    protected override void ModifyEntityForUpdate(InterviewPrep entity)
    {
        entity.PrepNotes = "Updated PrepNotes";
        entity.CompanyNotes = "Updated ComapnyNotes";
    }
}
