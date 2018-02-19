using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebKantora.Data.Common.Contracts
{
    public interface IWebKantoraDbRepository<T>
    {
        void Add(T entity);

        IQueryable<T> All();

        T GetById(Guid id);
        
        void Delete(T entity);

        void Update(T entity);

        IQueryable<T> Search(Expression<Func<T, bool>> predicate);
    }
}
