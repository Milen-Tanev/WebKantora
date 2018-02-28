using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

using WebKantora.Data.Common.Contracts;
using WebKantora.Data.Models;

namespace WebKantora.Data.Common
{
    public class CustomErrorDbRepository : ICustomErrorDbRepository
    {
        public CustomErrorDbRepository(WebKantoraDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<CustomError>();
        }

        public WebKantoraDbContext Context { get; }

        public DbSet<CustomError> DbSet { get; }

        public async Task Add(CustomError entity)
        {
            await this.DbSet.AddAsync(entity);
        }

        public IQueryable<CustomError> All()
        {
            return this.DbSet.AsNoTracking();
        }

        public async Task Delete(Guid id)
        {
            var entity = await this.GetById(id);
            this.DbSet.Remove(entity);
        }

        public async Task<CustomError> GetById(Guid id)
        {
            return await this.DbSet
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Update(Guid id, CustomError entity)
        {
            this.DbSet.Update(entity);
        }
    }
}
