using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Repo.shared
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> expression, int pageNumber, int pagesize, int sortingDirection, string sortExpression, string include = "");
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, string include = "");
        Task<T> GetAsync(Expression<Func<T, bool>> expression = null, string include = "");
        Task<T> GetLatestAsync(Expression<Func<T, bool>> expression = null, string include = "");
        Task<T> GetByIdAsync(int id);
        Task<T> InsertAsync(T entity);
        T Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void BulkInsert(List<T> entities);
        void BulkDelete(Expression<Func<T, bool>> expression = null);

    }
}