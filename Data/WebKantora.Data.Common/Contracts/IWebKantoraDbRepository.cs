using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebKantora.Data.Common.Contracts
{
    public interface IWebKantoraDbRepository<T>
    {
        Task Add(T entity);

        IQueryable<T> All();

        Task<T> GetById(Guid id);

        void Update(Guid id, T Entity);

        Task Delete(Guid id);

        //void Add(T entity);

        //IQueryable<T> All();

        //T GetById(Guid id);

        //void Delete(T entity);

        //void Update(T entity);

        //IQueryable<T> Search(Expression<Func<T, bool>> predicate);

        //Task Add(T entity);
    }
}
