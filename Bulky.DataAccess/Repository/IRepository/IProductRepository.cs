using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Update the property of single product
        /// </summary>
        /// <param name="product"></param>
        void Update(Product product);
    }
}

//////////////////////\\\\\\\\\\\\\\\\\\\\\\\\
/// <<<<<<<<<< Author: SamaClif-Q  >>>>>>>>>>>
/// <<<<<<<<< ww.kenjanka@gmail.com >>>>>>>>>>
//////////////////////\\\\\\\\\\\\\\\\\\\\\\\\
