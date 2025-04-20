using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobApplicationTracker.DataAccess.Models;

namespace JobAppTracker.DataAccess.Tests;

[TestClass]
public class JobAppTrackerCompanyTests : JobAppTrackerRepositoryTestBase<Company>
{
    protected override Company AddAnEntity()
    {
        var application = new Application
        {
            Id = 1,
            JobTitle = "Company Test",
            JobDescription = "Company Test Description",
            Status = "Applied",
            ApplicationDate = DateTime.Now,
            CompanyId = 1
        };

        _context.Applications.Add(application);
        _context.SaveChanges();

        return new Company
        {
            Id = 1,
            Name = "Test Company",
            Website = "https://www.testcompany.com",
            ApplicationId = 1,
            

        };

    }

    protected override void ModifyEntityForUpdate(Company entity)
    {
        entity.Name = "Updated Company";
        entity.Website = "https://www.updatedcompany.com";
    }
}
