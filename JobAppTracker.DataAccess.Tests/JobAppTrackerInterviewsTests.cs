using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobApplicationTracker.DataAccess.Models;

namespace JobAppTracker.DataAccess.Tests;

[TestClass]
public class JobAppTrackerInterviewsTests : JobAppTrackerRepositoryTestBase<Interviews>
{
    protected override Interviews AddAnEntity()
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

        var interview = new Interviews
        {
            
            InterviewDate = DateTime.Now.AddDays(1),
            Location = "Test Location",
            ApplicationId = application.Id
        };

       
        return interview;
    }

    protected override void ModifyEntityForUpdate(Interviews entity)
    {
        entity.InterviewDate = DateTime.Now.AddDays(2);
        entity.Location = "Updated Location";
    }


}
