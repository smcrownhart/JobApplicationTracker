using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobApplicationTracker.DataAccess.Models;

namespace JobAppTracker.DataAccess.Tests;

[TestClass]
public class JobAppTrackerCheckedOnAppTests : JobAppTrackerRepositoryTestBase<CheckedOnApp>
{
    protected override CheckedOnApp AddAnEntity()
    {
        return new CheckedOnApp
        {
            CheckedOnDate = DateTime.Now,
        };
    }

    protected override void ModifyEntityForUpdate(CheckedOnApp entity)
    {
        entity.CheckedOnDate = DateTime.Now.AddDays(1);
    }
}
