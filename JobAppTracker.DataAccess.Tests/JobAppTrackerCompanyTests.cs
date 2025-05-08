using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobApplicationTracker.DataAccess.Models;

namespace JobAppTracker.DataAccess.Tests;

[TestClass]
public class JobAppTrackerCompanyTests : JobAppTrackerRepositoryTestBase<Company>
{
    protected override Company AddAnEntity()
    {
        
        return new Company
        {
            
            Name = "Test Company",
            Website = "https://www.testcompany.com",
            
            

        };

    }

    protected override void ModifyEntityForUpdate(Company entity)
    {
        entity.Name = "Updated Company";
        entity.Website = "https://www.updatedcompany.com";
    }
}
