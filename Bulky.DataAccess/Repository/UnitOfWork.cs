
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext mDb;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="db"></param>
        public UnitOfWork(ApplicationDbContext db)
        {
            mDb = db;
            // Initialize category repo
            Category = new CategoryRepository(db);
            Product = new ProductRepository(db);
        }


        public void Save()
        {
           mDb.SaveChanges();
        }

        public ICategoryRepository Category{ get; private set; }
        public IProductRepository Product { get; private set; }
    }
}

//////////////////////\\\\\\\\\\\\\\\\\\\\\\\\
/// <<<<<<<<<< Author: SamaClif-Q  >>>>>>>>>>>
/// <<<<<<<<< ww.kenjanka@gmail.com >>>>>>>>>>
//////////////////////\\\\\\\\\\\\\\\\\\\\\\\\