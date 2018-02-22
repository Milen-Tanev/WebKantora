using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models.Contracts;

namespace WebKantora.Data.Common
{
    public class WebKantoraDbRepository<T>: IWebKantoraDbRepository<T>
        where T: class, IEntity, IDeletable
    {
        public WebKantoraDbRepository(WebKantoraDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        public WebKantoraDbContext Context { get; }

        public DbSet<T> DbSet { get; }

        public async Task Add(T entity)
        {
            await this.DbSet.AddAsync(entity);
        }

        public IQueryable<T> All()
        {
            return this.DbSet.AsNoTracking();
        }

        public async Task<T> GetById(Guid id)
        {
            return await this.DbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            entity.IsDeleted = true;
        }

        public void Update(Guid id, T entity)
        {
            this.DbSet.Update(entity);
        }

        //public void Add(T entity)
        //{
        //    this.DbSet.Add(entity);
        //}

        //public IQueryable<T> All()
        //{
        //    return this.DbSet.Where(e => !e.IsDeleted).AsNoTracking();
        //}

        //public T GetById(Guid id)
        //{
        //    var entity = this.DbSet.Find(id);

        //    if (entity.IsDeleted)
        //    {
        //        return null;
        //    }

        //    return entity;
        //}

        //public void Delete(T entity)
        //{
        //    entity.IsDeleted = true;
        //}

        //public void Update(T entity)
        //{
        //    var e = this.DbSet.Find(entity);

        //    if (!e.IsDeleted)
        //    {
        //        var entry = this.Context.Entry(entity);
        //        entry.State = EntityState.Modified;
        //    }
        //}

        //public IQueryable<T> Search(Expression<Func<T, bool>> predicate)
        //{
        //    return this.DbSet.Where(predicate)
        //        .Where(x => !x.IsDeleted);
        //}
    }
}
