using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repo.shared
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext context;
        private DbSet<T> entities;
        public Repository(ApplicationContext _context)
        {
            context = _context;
            entities = context.Set<T>();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> expression, int pageNumber, int pagesize, int sortingDirection, string sortExpression, string include = "")
        {
            if (String.IsNullOrWhiteSpace(sortExpression))
                sortExpression = "CreationDate";

            var param = Expression.Parameter(typeof(T), "item");
            var sortExp = Expression.Lambda<Func<T, object>>
            (Expression.Convert(Expression.Property(param, sortExpression), typeof(object)), param);

            var query = entities.Where(expression).AsQueryable();

            if (sortingDirection == 1)
                query = query.OrderByDescending(sortExp);
            else
                query = query.OrderBy(sortExp);

            query = include.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
               .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            query = query.Skip(pagesize * pageNumber).Take(pagesize).AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, string include = "")
        {
           if(expression != null)
            {
                return await include.Split(",", StringSplitOptions.RemoveEmptyEntries).Aggregate(
                    entities.Where(e => e.IsDeleted != true).Where(expression),
                    (current, includedProps) => current.Include(includedProps)).ToListAsync();
            }
           else
            {
              return  await include.Split(",", StringSplitOptions.RemoveEmptyEntries).Aggregate(
                    entities.Where(e => e.IsDeleted != true),
                    (current, includedProps) => current.Include(includedProps)).ToListAsync();
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression = null, string include = "")
        {
            if (expression == null)
            {
                return await include.Split(",", StringSplitOptions.RemoveEmptyEntries).Aggregate(
                     entities.Where(e => e.IsDeleted != true),
                     (current, includedProps) => current.Include(includedProps)).FirstOrDefaultAsync();
            }
            else
            {
                return await include.Split(",", StringSplitOptions.RemoveEmptyEntries).Aggregate(
                   entities.Where(e => e.IsDeleted != true).Where(expression),
                   (current, includedProps) => current.Include(includedProps)).FirstOrDefaultAsync();
            }
        }

        public async Task<T> GetLatestAsync(Expression<Func<T, bool>> expression = null, string include = "")
        {
            if (expression == null)
            {
                return await include.Split(",", StringSplitOptions.RemoveEmptyEntries).Aggregate(
                     entities.Where(e => e.IsDeleted != true),
                     (current, includedProps) => current.Include(includedProps)).OrderByDescending(em=>em.Id).FirstOrDefaultAsync();
            }
            else
            {
                return await include.Split(",", StringSplitOptions.RemoveEmptyEntries).Aggregate(
                   entities.Where(e => e.IsDeleted != true).Where(expression),
                   (current, includedProps) => current.Include(includedProps)).OrderByDescending(em => em.Id).FirstOrDefaultAsync();
            }
        }
        public  Task<T> GetByIdAsync(int id)
        {
            return entities.Where(em => em.IsDeleted != true && em.Id == id).FirstOrDefaultAsync();
        }

      
        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            return (await entities.AddAsync(entity)).Entity; 
           
        }
        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            return entities.Add(entity).Entity;
            
        }

        public void BulkInsert(List<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            this.entities.AddRange(entities);
        }
        public void BulkDelete(Expression<Func<T, bool>> expression = null)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            this.entities.RemoveRange(entities.Where(expression));
        }



        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.IsDeleted = true;
            entities.Update(entity);
        }


        public void Update(T entity)
        {
            if(entity==null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
        }
       

    }
}
