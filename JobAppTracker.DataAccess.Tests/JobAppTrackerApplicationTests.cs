using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobApplicationTracker.DataAccess.Models;

namespace JobAppTracker.DataAccess.Tests;

[TestClass]
public class JobAppTrackerApplicationTests : JobAppTrackerRepositoryTestBase<Application>
{
    protected override Application AddAnEntity()
    {
        return new Application
        {
            JobTitle = "Test Job",
            JobDescription = "Test Job Description",
            ApplicationDate = DateTime.Today,
            Status = "Applied",
            Company = new Company { Name = "TestCo", Website = "https://example.com" },
            CompanyContacts = new List<CompanyContact>(),
            Interviews = new List<Interviews>(),
            InterviewPreps = new List<InterviewPrep>(),
            CheckedOnHistory = new List<CheckedOnApp>()
        };
    }

    protected override void ModifyEntityForUpdate(Application entity)
    {
        entity.ApplicationDate = DateTime.Now.AddDays(1);
        entity.Status = "Interviewed";
        entity.JobDescription = "Updated JobDescription";
        entity.JobTitle = "Updated JobTitle";
    }
    
}
