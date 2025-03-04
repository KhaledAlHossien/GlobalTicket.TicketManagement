using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalTicket.TicketManagement.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GlobalTicket.TicketManagement.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly GloboTicketDbContext _dbContext;

        public BaseRepository(GloboTicketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        //Task<T> IAsyncRepository<T>.UpdateAsync(T entity)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<T> IAsyncRepository<T>.DeleteAsync(T entity)
        //{
        //    throw new NotImplementedException();
        //}
    }

}
