using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models.Contracts;

namespace WebKantora.Data.Common
{
    public class WebKantoraDbRepository<T> : IWebKantoraDbRepository<T>
        where T : class, IEntity, IDeletable
    {
        public WebKantoraDbRepository(WebKantoraDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException("The DbContext cannot be null.");
            this.DbSet = this.Context.Set<T>();

            if (this.DbSet == null)
            {
                throw new NullReferenceException("The DbSet cannot be null");
            }
        }

        public WebKantoraDbContext Context { get; }

        public DbSet<T> DbSet { get; }

        public IQueryable<T> GetById(Guid id)
        {
            return this.DbSet
                .Where(e => !e.IsDeleted)
                .Where(e => e.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return this.DbSet
                .Where(e => !e.IsDeleted);
        }

        public async Task Add(T entity)
        {
            await this.DbSet.AddAsync(entity);
        }

        public async Task Delete(T entity)
        {
            var dbEntity = await this.DbSet.FindAsync(entity);

            if (dbEntity != null)
            {
                dbEntity.IsDeleted = true;
            }
        }

        public async Task Delete(Guid id)
        {
            var dbEntity = await this.DbSet.FirstOrDefaultAsync(e => e.Id == id);

            if (dbEntity != null)
            {
                dbEntity.IsDeleted = true;
            }
        }

        public async Task Update(T entity)
        {
            var dbEntity = await this.DbSet.FindAsync(entity);

            if (dbEntity != null)
            {
                this.DbSet.Update(dbEntity);
            }
        }

        public async Task Update(Guid id)
        {
            var dbEntity = await this.DbSet.FirstOrDefaultAsync(e => e.Id == id);

            if (dbEntity != null)
            {
                this.DbSet.Update(dbEntity);
            }
        }
    }
}
