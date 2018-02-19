using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models.Contracts;

namespace WebKantora.Data.Common
{
    public class WebKantoraDbRepository<T>: IWebKantoraDbRepository<T>
        where T: class, IDeletable
    {
        public WebKantoraDbRepository(WebKantoraDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        public WebKantoraDbContext Context { get; }

        public DbSet<T> DbSet { get; }

        public void Add(T entity)
        {
            this.DbSet.Add(entity);
        }

        public IQueryable<T> All()
        {
            return this.DbSet.Where(e => !e.IsDeleted);
        }

        public T GetById(Guid id)
        {
            var entity = this.DbSet.Find(id);

            if (entity.IsDeleted)
            {
                return null;
            }

            return entity;
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
        }

        public void Update(T entity)
        {
            var e = this.DbSet.Find(entity);

            if (!e.IsDeleted)
            {
                var entry = this.Context.Entry(entity);
                entry.State = EntityState.Modified;
            }
        }

        public IQueryable<T> Search(Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.Where(predicate)
                .Where(x => !x.IsDeleted);
        }
    }
}
