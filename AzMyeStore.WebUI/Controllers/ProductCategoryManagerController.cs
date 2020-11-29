using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AzMyeStoreNov2020.Core.Models;
using AzMyeStoreNov2020.DataAccess.InMemory;

namespace AzMyeStore.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {

        InMemoryRepository<ProductCategory> context;

        public ProductCategoryManagerController()  // this constructor initializes the Product repository context
        {
            context = new InMemoryRepository<ProductCategory>();
        }

        // GET: ProductCategoryManager
        public ActionResult Index()  //Make the index page return a list of product Categories
                                     //when generating a view select List and ProductCategory as Model
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()  // first method is display the page to fill in the product details
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)  // second method is to have the product category details posted in 
        {
            if (!ModelState.IsValid)  // to check if there are any validation errors on the page , if there are any errors return the page with the errors
            {
                return View(productCategory);
            }

            else
            {
                context.Insert(productCategory);
                context.Commit();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = context.Find(Id); //find the productCategory from the DataAccess in memory to edit


            if (productCategory == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(productCategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id); //find the product category from the DataAccess in memory to edit


            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }

                productCategoryToEdit.Category = productCategory.Category;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id); //find the productCategory from the DataAccess in memory to edit


            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(productCategoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id); //find the productCategory from the DataAccess in memory to edit


            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }

            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}