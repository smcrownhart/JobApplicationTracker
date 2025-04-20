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
            JobTitle = "Test JobTitle",
            JobDescription = "Test JobDescription",
            ApplicationDate = DateTime.Now,
            Status = "Applied",
            CompanyId = 1
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
