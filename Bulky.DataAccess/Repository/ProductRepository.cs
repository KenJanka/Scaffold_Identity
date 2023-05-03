
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext mDb;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            mDb = db;
        }

        public void Update(Product product)
        {
            // Manually update selected properties
            var objFromDb = mDb.Products.AsNoTracking().FirstOrDefault(p => p.Id  == product.Id);

            if (objFromDb != null)
            {
                objFromDb.Title = product.Title;
                objFromDb.Description = product.Description;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.Price = product.Price;
                objFromDb.Price50= product.Price50;
                objFromDb.Price100 = product.Price100;
                objFromDb.ListPrice = product.ListPrice;
                objFromDb.Author = product.Author;
                objFromDb.ISBN = product.ISBN;

                // update product image if not null
                if(product.ImageUrl != null)
                {
                    objFromDb.ImageUrl = product.ImageUrl;
                }
            }
            mDb.Products.Update(product);
        }
    }
}

//////////////////////\\\\\\\\\\\\\\\\\\\\\\\\
/// <<<<<<<<<< Author: SamaClif-Q  >>>>>>>>>>>
/// <<<<<<<<< ww.kenjanka@gmail.com >>>>>>>>>>
//////////////////////\\\\\\\\\\\\\\\\\\\\\\\\The instance of entity type 'Product' cannot be tracked because another instance with the same key value for {'Id'} is already being tracked. When attaching existing entities, ensure that only one entity instance