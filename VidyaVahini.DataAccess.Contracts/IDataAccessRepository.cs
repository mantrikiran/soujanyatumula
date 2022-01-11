using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace VidyaVahini.DataAccess.Contracts
{
    public interface IDataAccessRepository<T> where T : class
    {
        /// <summary>
        /// Queries a database entity
        /// </summary>
        /// <returns>Entity</returns>
        IQueryable<T> Query();

        /// <summary>
        /// Gets all the records of an entity
        /// </summary>
        /// <returns>Database records</returns>
        ICollection<T> GetAll();

        /// <summary>
        /// Gets the record by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Entity</returns>
        T GetById(int id);

        /// <summary>
        /// Gets the record by unique identifier
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Entity</returns>
        T GetByUniqueId(string id);

        /// <summary>
        /// Returns the first record that match the expression
        /// </summary>
        /// <param name="match">Expression</param>
        /// <returns>Entity</returns>
        T Find(Expression<Func<T, bool>> match);

        /// <summary>
        /// Returns all the records that match the expression
        /// </summary>
        /// <param name="match">Expression</param>
        /// <returns>Entity</returns>
        ICollection<T> FindAll(Expression<Func<T, bool>> match);

        /// <summary>
        /// Adds a new record
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Entity</returns>
        T Add(T entity);

        /// <summary>
        /// Add bulk records
        /// </summary>
        /// <param name="entity">Entities</param>
        /// <returns>Entities</returns>
        IEnumerable<T> Add(IEnumerable<T> entity);

        /// <summary>
        /// Updates a record
        /// </summary>
        /// <param name="updated">Entity</param>
        /// <returns>Entity</returns>
        T Update(T updated);

        /// <summary>
        /// Deletes a record
        /// </summary>
        /// <param name="t">Entity</param>
        void Delete(T t);

        /// <summary>
        /// Deletes bulk records
        /// </summary>
        /// <param name="t">Entities</param>
        void Delete(ICollection<T> t);

        /// <summary>
        /// Gets the total number of records
        /// </summary>
        /// <returns>Count</returns>
        int Count();

        /// <summary>
        /// Filters the records based on the expression
        /// </summary>
        /// <param name="filter">Expression</param>
        /// <param name="orderBy">Order By</param>
        /// <param name="includeProperties">Include Properties</param>
        /// <param name="page">Page Number</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>Entities</returns>
        IEnumerable<T> Filter(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int? page = null,
            int? pageSize = null);

        /// <summary>
        /// Queries the database based on the expression
        /// </summary>
        /// <param name="predicate">Expression</param>
        /// <returns>Entities</returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Checks if the expression's evaluation has results
        /// </summary>
        /// <param name="predicate">Expression</param>
        /// <returns>success/failure</returns>
        bool Exist(Expression<Func<T, bool>> predicate);
    }
}
