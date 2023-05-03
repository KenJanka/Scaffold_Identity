using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork 
    {
        /// <summary>
        /// Instance of category repository
        /// </summary>
        public ICategoryRepository Category{ get; }
        public IProductRepository Product { get; }

        /// <summary>
        /// Save db changes
        /// </summary>
        void Save();
    }
}

//////////////////////\\\\\\\\\\\\\\\\\\\\\\\\
/// <<<<<<<<<< Author: SamaClif-Q  >>>>>>>>>>>
/// <<<<<<<<< ww.kenjanka@gmail.com >>>>>>>>>>
//////////////////////\\\\\\\\\\\\\\\\\\\\\\\\
