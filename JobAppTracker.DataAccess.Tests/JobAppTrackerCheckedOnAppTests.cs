using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobApplicationTracker.DataAccess.Models;

namespace JobAppTracker.DataAccess.Tests;

[TestClass]
public class JobAppTrackerCheckedOnAppTests : JobAppTrackerRepositoryTestBase<CheckedOnApp>
{
    protected override CheckedOnApp AddAnEntity()
    {
        var application = new Application
        {
            JobTitle = "Test Job",
            JobDescription = "Test Job Description",
            ApplicationDate = DateTime.Today,
            Status = "Applied",
            Company = new Company
            {
                Name = "TestCo",
                Website = "https://example.com"
            }
        };

        _context.Applications.Add(application);
        _context.SaveChanges();

        return new CheckedOnApp
        {
            CheckedOnDate = DateTime.Now,
            ApplicationId = application.Id,
        };
    }

    protected override void ModifyEntityForUpdate(CheckedOnApp entity)
    {
        entity.CheckedOnDate = DateTime.Now.AddDays(1);
    }
}
