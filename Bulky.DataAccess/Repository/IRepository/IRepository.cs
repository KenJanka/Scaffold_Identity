using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T - Will be category or any other model

        /// <summary>
        /// Return a list of entities of <typeparamref name="T"/>
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll(string? includeProperties = null);
        /// <summary>
        /// Retrieve a single entity of <typeparamref name="T"/>  based on the filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        /// <summary>
        /// Add a single enitity of <typeparamref name="T"/> to the dbset
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);
        /// <summary>
        /// Remove an entity of <typeparamref name="T"/> from the dbset
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);
        /// <summary>
        /// Removes a range of entities of <typeparamref name="T"/> from the dbset
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<T> entities);
    }
}

//////////////////////\\\\\\\\\\\\\\\\\\\\\\\\
/// <<<<<<<<<< Author: SamaClif-Q  >>>>>>>>>>>
/// <<<<<<<<< ww.kenjanka@gmail.com >>>>>>>>>>
//////////////////////\\\\\\\\\\\\\\\\\\\\\\\\
