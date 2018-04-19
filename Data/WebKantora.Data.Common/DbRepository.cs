using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebKantora.Data.Models.Contracts;

namespace WebKantora.Data.Common
{
    public abstract class DbRepository<T>
        where T : class, IDeletable, IEntity
    {
        public DbRepository(WebKantoraDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("The DbContext cannot be null.");
            }
            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        protected WebKantoraDbContext Context { get; }

        protected DbSet<T> DbSet { get; }

        public async Task Add(T entity)
        {
            await this.DbSet.AddAsync(entity);
        }

        public async Task Add(ICollection<T> collection)
        {
            await this.DbSet.AddRangeAsync(collection);
        }

        public IQueryable<T> All()
        {
            var entity = this.DbSet.Where(x => !x.IsDeleted);
            return this.Include(entity);
        }

        public async Task Delete(Guid id)
        {
            var entity = await this.GetById(id);
            entity.IsDeleted = true;
        }

        public async Task<T> GetById(Guid id)
        {
            var entity = this.DbSet;
            var result = this.Include(entity);
            return await result
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Update(Guid id, T entity)
        {
            this.DbSet.Update(entity);
        }

        protected abstract IQueryable<T> Include(IQueryable<T> entity);
    }
}
