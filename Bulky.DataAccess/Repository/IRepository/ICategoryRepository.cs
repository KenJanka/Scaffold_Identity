using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        /// <summary>
        /// Update the property of single category
        /// </summary>
        /// <param name="category"></param>
        void Update(Category category);
       
    }
}

//////////////////////\\\\\\\\\\\\\\\\\\\\\\\\
/// <<<<<<<<<< Author: SamaClif-Q  >>>>>>>>>>>
/// <<<<<<<<< ww.kenjanka@gmail.com >>>>>>>>>>
//////////////////////\\\\\\\\\\\\\\\\\\\\\\\\
