using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Infrastructure
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly ApplicationDbContext _context;
        private static readonly SemaphoreSlim _lock = new(1, 1);

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> ExistsByJobIdAsync(int jobId)
        {
            if (typeof(T) == typeof(Job))
            {
                return await _context.Jobs.AnyAsync(j => j.JobId == jobId);
            }
            throw new InvalidOperationException("ExistsByJobIdAsync can only be used with the Job entity.");
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _lock.WaitAsync();
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task UpdateAsync(T entity)
        {
            await _lock.WaitAsync();
            try
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            await _lock.WaitAsync();
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity != null)
                {
                    _context.Set<T>().Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            finally
            {
                _lock.Release();
            }
        }
    }
}