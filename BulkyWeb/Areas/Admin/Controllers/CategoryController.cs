using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork mUnitOfWork;

        #region Default Constructor

        public CategoryController(IUnitOfWork unitOfwork)
        {
            // Initialize db
            mUnitOfWork = unitOfwork;
        }

        #endregion


        public IActionResult Index()
        {
            // Retrieve all categories
            List<Category> categoryList = mUnitOfWork.Category.GetAll().ToList();

            return View(categoryList);
        }

        #region Create Action

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            // Add custom validation
            //if(category.Name == category.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("extra", "The display order can not exactly match the name");
            //}

            //if (category.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "'test' is an invalid value.");
            //}

            // Check if model state is valid
            if (ModelState.IsValid)
            {
                // Save new category to db
                mUnitOfWork.Category.Add(category);
                mUnitOfWork.Save();
                TempData["success"] = "Category created successfully";

                // Redirect to index page
                return RedirectToAction("Index");
                //return RedirectToAction("Index", "Category"); // Ignore controller name since we are navigating to same controller
            }

            return View();
        }

        #endregion

        #region Edit Action

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            // Retrieve category from db
            Category? categoryFromDb = mUnitOfWork.Category.Get(c => c.Id == id);

            //Category? categoryFromDb1 = mDb.Categories.Find(id);
            //Category? categoryFromDb2 = mDb.Categories.Where(c=>c.Id == id).FirstOrDefault();

            if (categoryFromDb == null)
                return NotFound();

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            // Check if model state is valid
            if (ModelState.IsValid)
            {
                // Save new category to db
                mUnitOfWork.Category.Update(category);
                mUnitOfWork.Save();
                TempData["success"] = "Category edited successfully";

                // Redirect to index page
                return RedirectToAction("Index");
                //return RedirectToAction("Index", "Category"); // Ignore controller name since we are navigating to same controller
            }

            return View();
        }


        #endregion

        #region Delete Action

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            // Retrieve category from db
            Category? categoryFromDb = mUnitOfWork.Category.Get(c => c.Id == id);

            //Category? categoryFromDb1 = mDb.Categories.Find(id);
            //Category? categoryFromDb2 = mDb.Categories.Where(c=>c.Id == id).FirstOrDefault();

            if (categoryFromDb == null)
                return NotFound();

            return View(categoryFromDb);
        }

        [HttpPost]
        [DisplayName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category category = mUnitOfWork.Category.Get(c => c.Id == id);

            if (category == null)
                return NotFound();

            // Delete the category
            mUnitOfWork.Category.Remove(category);
            mUnitOfWork.Save();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index", "Category");
        }


        #endregion
    }
}
