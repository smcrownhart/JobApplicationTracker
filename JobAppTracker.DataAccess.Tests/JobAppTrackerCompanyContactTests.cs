using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobApplicationTracker.DataAccess.Models;

namespace JobAppTracker.DataAccess.Tests;

[TestClass]
public class JobAppTrackerCompanyContactTests : JobAppTrackerRepositoryTestBase<CompanyContact>
{
    protected override CompanyContact AddAnEntity()
    {
        var application = new Application
        {
            Id = 1,
            JobTitle = "Company Contact Test",
            JobDescription = "Company Contact Test Description",
            ApplicationDate = DateTime.Now,
            Status = "Applied",
            Company = new Company
            {
                Name = "TestCo",
                Website = "https://example.com"
            }
        };

        _context.Applications.Add(application);
        _context.SaveChanges();

        

        

        return new CompanyContact
        {
            Id = 1,
            Name = "Company Contact Test",
            Email = "contact@Test.com",
            Phone = "123-456-7890",
            ApplicationId = application.Id,
            
        };
    }

    protected override void ModifyEntityForUpdate(CompanyContact entity)
    {
        entity.Name = "Updated Company Contact Test";
        entity.Email = "update@test.com";
        entity.Phone = "098-765-4321";
    }
}
