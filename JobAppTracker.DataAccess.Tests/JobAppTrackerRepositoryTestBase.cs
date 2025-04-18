using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobApplicationTracker.DataAccess.Data;
using JobApplicationTracker.DataAccess.Models;
using JobApplicationTracker.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace JobAppTracker.DataAccess.Tests
{
    
    public abstract class JobAppTrackerRepositoryTestBase<T> where T : class
    {
        protected JobAppDbContext _context;
        protected JobAppTrackerRepository<T> _repository;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<JobAppDbContext>()
                .UseInMemoryDatabase(databaseName: "JobAppTrackerTestDb")
                .Options;
            _context = new JobAppDbContext(options);
            _repository = new JobAppTrackerRepository<T>(_context);
        }

        [TestCleanup]

        public void Cleanup()
        {
            _context.Dispose();
        }

        protected abstract T AddAnEntity();
        protected abstract void ModifyEntityForUpdate(T entity);

        [TestMethod]
        public async Task Add_Entity()
        {


            var entity = AddAnEntity();
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            var all = await _repository.GetAllAsync();
            Assert.AreEqual(1, all.Count());
                
        }

        [TestMethod]

        public async Task Get_Entity()
        {
            var entity = AddAnEntity();
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            var getEntity = await _repository.GetByIdAsync((entity as dynamic).Id);
            Assert.IsNotNull(getEntity);
        }

        [TestMethod]

        public async Task Update_Entity()
        {
            var entity = AddAnEntity();
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            ModifyEntityForUpdate(entity);

            await _repository.UpdateAsync(entity);
            await _context.SaveChangesAsync();
            var updatedEntity = await _repository.GetByIdAsync((entity as dynamic).Id);
            Assert.IsNotNull(updatedEntity);
        }

        [TestMethod]

        public async Task Delete_Entity()
        {
            var entity = AddAnEntity();
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();
            
            await _repository.DeleteAsync((entity as dynamic).Id);
            await _context.SaveChangesAsync();
            var deletedEntity = await _repository.GetByIdAsync((entity as dynamic).Id);
            Assert.IsNull(deletedEntity);
        }
    }
}
