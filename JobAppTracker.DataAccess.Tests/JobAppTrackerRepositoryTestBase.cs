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
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
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

            var all = await _repository.GetAllAsync();
            Assert.AreEqual(1, all.Count());
        }

        [TestMethod]
        public async Task Add_Entity_Throws_ArgumentNullException()
        {
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                _repository.AddAsync(null));

            Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));
        }

        [TestMethod]
        public async Task Get_All_Entities()
        {
            var entity = AddAnEntity();
            await _repository.AddAsync(entity);

            var all = await _repository.GetAllAsync();
            Assert.IsNotNull(all);
            Assert.AreEqual(1, all.Count());
        }

        [TestMethod]
        public async Task Get_Entity_By_Id()
        {
            var entity = AddAnEntity();
            await _repository.AddAsync(entity);

            var getEntity = await _repository.GetByIdAsync((entity as dynamic).Id);
            Assert.IsNotNull(getEntity);
        }

        [TestMethod]
        public async Task Get_Entity_By_Id_Throws_Exception()
        {
            await Assert.ThrowsExceptionAsync<Exception>(() =>
                _repository.GetByIdAsync(999)); // non-existent ID
        }

        [TestMethod]
        public async Task Update_Entity()
        {
            var entity = AddAnEntity();
            await _repository.AddAsync(entity);

            ModifyEntityForUpdate(entity);
            await _repository.UpdateAsync(entity);

            var updated = await _repository.GetByIdAsync((entity as dynamic).Id);
            Assert.IsNotNull(updated);
        }

        [TestMethod]
        public async Task Update_Entity_Throws_ArgumentNullException()
        {
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() =>
                _repository.UpdateAsync(null));

            Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));
        }

        [TestMethod]
        public async Task Delete_Entity()
        {
            var entity = AddAnEntity();
            await _repository.AddAsync(entity);

            await _repository.DeleteAsync((entity as dynamic).Id);
            var all = await _repository.GetAllAsync();
            Assert.AreEqual(0, all.Count());
        }

        [TestMethod]
        public async Task Delete_Entity_Throws_Exception()
        {
            await Assert.ThrowsExceptionAsync<Exception>(() =>
                _repository.DeleteAsync(999)); // non-existent ID
        }

    }
}
