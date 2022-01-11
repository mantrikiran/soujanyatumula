using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VidyaVahini.DataAccess.Models;
using VidyaVahini.DataAccess.Contracts;

namespace VidyaVahini.DataAccess.Repository
{
    public class DataAccessRepository<T> : IDataAccessRepository<T> where T : class
    {
        private readonly vidyavahiniContext _context;
        private readonly DbSet<T> _dbSet;

        public DataAccessRepository(vidyavahiniContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }

        public ICollection<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T GetByUniqueId(string id)
        {
            return _dbSet.Find(id);
        }

        public T Find(Expression<Func<T, bool>> match)
        {
            return _dbSet.SingleOrDefault(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _dbSet.Where(match).ToList();
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public IEnumerable<T> Add(IEnumerable<T> entity)
        {
            _dbSet.AddRange(entity);
            return entity;
        }

        public T Update(T updated)
        {
            if (updated == null)
            {
                return null;
            }

            _dbSet.Attach(updated);
            _context.Entry(updated).State = EntityState.Modified;

            return updated;
        }

        public void Delete(T t)
        {
            _dbSet.Remove(t);
        }

        public void Delete(ICollection<T> t)
        {
            _dbSet.RemoveRange(t);
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int? page = null,
            int? pageSize = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (includeProperties != null)
            {
                foreach (
                    var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query.ToList();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public bool Exist(Expression<Func<T, bool>> predicate)
        {
            var exist = _dbSet.Where(predicate);
            return exist.Any() ? true : false;
        }
    }
}
