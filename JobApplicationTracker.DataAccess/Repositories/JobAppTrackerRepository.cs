using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobApplicationTracker.DataAccess.Data;
using JobApplicationTracker.DataAccess.Interfaces;
using JobApplicationTracker.DataAccess.Models;
using Microsoft.EntityFrameworkCore;


namespace JobApplicationTracker.DataAccess.Repositories
{
    public class JobAppTrackerRepository<T> : IJobAppTracker<T> where T : class
    {
        private JobAppDbContext _context;

        public JobAppTrackerRepository(JobAppDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
            
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding {typeof(T).Name}", ex);
                
            }
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
            {
                throw new Exception($"{typeof(T).Name} with id {id} not found");
            }

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                var entities = await _context.Set<T>().ToListAsync();

                if (entities == null)
                {
                    throw new InvalidOperationException($"No {typeof(T).Name}s found");
                }
                    return entities;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all {typeof(T).Name}s", ex);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity == null)
                    throw new Exception($"{typeof(T).Name} with id {id} not found");

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving {typeof(T).Name} with id {id}", ex);
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
                return entity;
               
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating {typeof(T).Name}", ex);
            }
        }
    }
}


