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
            Company = new Company
            {
                Name = "TestCo",
                Website = "https://example.com"
            }
        };
        _context.Applications.Add(application);
        _context.SaveChanges();

        

        

        var interviewPrep = new InterviewPrep
        {
            
            PrepNotes = "Test Prep Notes",
            CompanyNotes = "Test Company Notes",
            ApplicationId = application.Id,

        };
        
        return interviewPrep;
    }
    protected override void ModifyEntityForUpdate(InterviewPrep entity)
    {
        entity.PrepNotes = "Updated PrepNotes";
        entity.CompanyNotes = "Updated ComapnyNotes";
    }
}
