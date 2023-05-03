
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext mDb;
        internal DbSet<T> mDbSet;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="db"></param>
        public Repository(ApplicationDbContext db)
        {
            mDb = db;

            // Retrieve the current dbset
            this.mDbSet = db.Set<T>();

            mDb.Products.Include(u => u.Category).Include(i => i.Category);

        }


        public void Add(T entity)
        {
            mDbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = mDbSet;
            query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                string[] properties = new string[] { };

                foreach (string includeProp in
                    includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Returns a list of all entities in the dbset
        ///     The include properties indicate extra entities (foriegn properties) that shall be retrieved alongside the records
        ///         Include properites should have exactly the same name as the it's name in the entity model
        ///             i.e. Category, Covertype
        /// </summary>
        /// <param name="includeProperties">The foreign properties that should be reterieved</param>
        /// <returns></returns>
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = mDbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                string[] properties = new string[] { };

                foreach (string includeProp in
                    includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query  = query.Include(includeProp);
                }
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            mDbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            mDbSet.RemoveRange(entities);
        }
    }
}

//////////////////////\\\\\\\\\\\\\\\\\\\\\\\\
/// <<<<<<<<<< Author: SamaClif-Q  >>>>>>>>>>>
/// <<<<<<<<< ww.kenjanka@gmail.com >>>>>>>>>>
//////////////////////\\\\\\\\\\\\\\\\\\\\\\\\