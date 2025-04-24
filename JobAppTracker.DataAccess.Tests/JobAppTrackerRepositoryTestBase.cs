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
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
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
        public async Task Add_Entity_Throws_Exception()
        {
            var ex = await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                
                await _repository.AddAsync(null);
                
            });

            Assert.IsTrue(ex.Message.Contains("Error adding"));
        }

        [TestMethod]

        public async Task Get_All_Entities()
        {
            var entity = AddAnEntity();
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();
            var all = await _repository.GetAllAsync();
            Assert.IsNotNull(all);
        }

        [TestMethod]

        public async Task Get_All_Entities_Throws_Exception()
        {
            var noRepo = new JobAppTrackerRepository<object>(_context);
            var ex = await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {

                await noRepo.GetAllAsync();
            });
            Assert.IsTrue(ex.Message.Contains("Error retrieving all"));
        }


        [TestMethod]

        public async Task Get_Entity_By_Id()
        {
            var entity = AddAnEntity();
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();

            var getEntity = await _repository.GetByIdAsync((entity as dynamic).Id);
            Assert.IsNotNull(getEntity);
        }

        [TestMethod]

        public async Task Get_Entity_By_Id_Throws_Exception()
        {
            var ex = await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {

                await _repository.GetByIdAsync(0);
            });
            Assert.IsTrue(ex.Message.Contains("Error retrieving"));
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

        public async Task Update_Entity_Throws_Exception()
        {
            var ex = await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                
                await _repository.UpdateAsync(null);
            });

            Assert.IsTrue(ex.Message.Contains("Error updating"));
        }

        [TestMethod]

        public async Task Delete_Entity()
        {
            var entity = AddAnEntity();
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();
            
            await _repository.DeleteAsync((entity as dynamic).Id);
            await _context.SaveChangesAsync();
            
            var deletedEntity = await _context.Set<T>().FindAsync((entity as dynamic).Id);
            Assert.IsNull(deletedEntity);
        }


        [TestMethod]

        public async Task Delete_Null_Entity_Throws_Exception()
        {
            var ex = await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                await _repository.DeleteAsync(0);
            });

            Assert.IsTrue(ex.Message.Contains("not found"));
        }
        
    }
}
