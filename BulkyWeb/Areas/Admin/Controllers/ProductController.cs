using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork mUnitOfWork;
        private readonly IWebHostEnvironment mWebHostEnvironment; // Use the webhost to access the wwwroot folder


        #region Default Constructor

        public ProductController(IUnitOfWork unitOfwork, IWebHostEnvironment mWebHostEnvironment)
        {
            // Initialize db
            mUnitOfWork = unitOfwork;
            this.mWebHostEnvironment = mWebHostEnvironment;

        }

        #endregion


        public IActionResult Index()
        {
            // Retrieve all categories
            List<Product> ProductList = mUnitOfWork.Product.GetAll(includeProperties: "Category").ToList();


            return View(ProductList);
        }

        #region Upsert Action

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            // Initializes a viewmodel instance
            ProductVM productVM = new ProductVM()
            {
                CategoryList = mUnitOfWork.Category.GetAll().Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                }),
                Product = new Product(),
            };

            // Means this is a create request. Therefore return viewmodel to view
            //      Otherwise retrieve the product from the database
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                // Update
                productVM.Product = mUnitOfWork.Product.Get(p => p.Id == id);

                if (productVM.Product != null)
                    return View(productVM);

                else
                    return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {

            // Check if model state is valid
            if (ModelState.IsValid)
            {
                #region Manage Image Upload

                // Get root path
                string wwwRootPath = mWebHostEnvironment.WebRootPath;

                // Upload file if it's not null
                if (file != null)
                {

                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); // Generate random filename
                    string productPath = Path.Combine(wwwRootPath, @"Images\Products");

                    // If there's already and image for this product, delete and upload a new image
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        // Delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // save image
                    using (var fileStream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    // Update product url
                    productVM.Product.ImageUrl = @"\Images\Products\" + filename;

                }

                #endregion

                // If product already exist, Update
                //      Otherwise Create
                if (productVM.Product.Id == 0 || productVM.Product.Id == null)
                {
                    // Save new Product to db
                    mUnitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    mUnitOfWork.Product.Update(productVM.Product);
                }

                // Save changes
                mUnitOfWork.Save();
                TempData["success"] = "Product created successfully";

                // Redirect to index page
                return RedirectToAction("Index");
                //return RedirectToAction("Index", "Product"); // Ignore controller name since we are navigating to same controller
            }



            productVM.CategoryList = mUnitOfWork.Category.GetAll().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });


            return View(productVM);
        }

        #endregion

        #region Edit Action

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //        return NotFound();

        //    // Retrieve Product from db
        //    Product? productFromDb = mUnitOfWork.Product.Get(c => c.Id == id);

        //    //Product? ProductFromDb1 = mDb.Categories.Find(id);
        //    //Product? ProductFromDb2 = mDb.Categories.Where(c=>c.Id == id).FirstOrDefault();

        //    if (productFromDb == null)
        //        return NotFound();

        //    ProductVM productVM = new ProductVM()
        //    {
        //        CategoryList = mUnitOfWork.Category.GetAll().Select(s => new SelectListItem
        //        {
        //            Text = s.Name,
        //            Value = s.Id.ToString(),
        //        }),
        //        Product = productFromDb,
        //    };

        //    return View(productVM);
        //}

        //[HttpPost]
        //public IActionResult Edit(ProductVM productVM)
        //{
        //    // Check if model state is valid
        //    if (ModelState.IsValid)
        //    {
        //        // Save new Product to db
        //        mUnitOfWork.Product.Update(productVM.Product);
        //        mUnitOfWork.Save();
        //        TempData["success"] = "Product edited successfully";

        //        // Redirect to index page
        //        return RedirectToAction("Index");
        //        //return RedirectToAction("Index", "Product"); // Ignore controller name since we are navigating to same controller
        //    }

        //    // Update category list
        //    productVM.CategoryList = mUnitOfWork.Category.GetAll().Select(c => new SelectListItem()
        //    {
        //        Text = c.Name,
        //        Value = c.Id.ToString()
        //    });

        //    return View(productVM);
        //}


        #endregion

        #region Delete Action

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //        return NotFound();

        //    // Retrieve Product from db
        //    Product? productFromDb = mUnitOfWork.Product.Get(c => c.Id == id);

        //    //Product? ProductFromDb1 = mDb.Categories.Find(id);
        //    //Product? ProductFromDb2 = mDb.Categories.Where(c=>c.Id == id).FirstOrDefault();

        //    if (productFromDb == null)
        //        return NotFound();

        //    ProductVM productVM = new ProductVM()
        //    {
        //        CategoryList = mUnitOfWork.Category.GetAll().Select(s => new SelectListItem
        //        {
        //            Text = s.Name,
        //            Value = s.Id.ToString(),
        //        }),
        //        Product = productFromDb,
        //    };

        //    return View(productVM);
        //}

        //[HttpPost]
        //[DisplayName("Delete")]
        //public IActionResult Delete(ProductVM? prodVm)
        //{
        //    Product product = mUnitOfWork.Product.Get(c => c.Id == prodVm.Product.Id);

        //    if (product == null)
        //        return NotFound();

        //    // Delete the Product
        //    mUnitOfWork.Product.Remove(product);
        //    mUnitOfWork.Save();
        //    TempData["success"] = "Product deleted successfully";

        //    return RedirectToAction("Index");
        //}


        #endregion

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            // Retrieve all categories
            List<Product> productList = mUnitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return Json(new { data = productList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            // Retrieve product to be deleted
            if (id != null || id != 0)
            {
                Product productFromDb = mUnitOfWork.Product.Get(p => p.Id == id, includeProperties: "Category");

                if (productFromDb == null)
                    return Json(new { success = false, message = "Error deleting selected product" });

                // Delete project image if product exist
                // Delete the old image
                var oldImagePath = Path.Combine(mWebHostEnvironment.WebRootPath,
                    productFromDb.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                // Finally delete the product
                mUnitOfWork.Product.Remove(productFromDb);
                mUnitOfWork.Save();

                return Json(new { success = true, message = "Delete Successful" });

            }

            return Json(new { success = false, message = "Error deleting selected product" });

        }

        #endregion
    }
}
