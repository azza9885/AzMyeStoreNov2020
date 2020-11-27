using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AzMyeStoreNov2020.Core.Models;
using AzMyeStoreNov2020.DataAccess.InMemory;

namespace AzMyeStore.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        public ProductManagerController()  // this constructor initializes the Product repository context
        {
            context = new ProductRepository();
        }


        // GET: ProductManager
        public ActionResult Index()  //Make the index page return a list of products
                                     //when generating a view select List and Product as Model
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

       public ActionResult Create()  // first method is display the page to fill in the product details
        {
            Product product = new Product();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product)  // second method is to have the product details posted in 
        {
            if (!ModelState.IsValid)  // to check if there are any validation errors on the page , if there are any errors return the page with the errors
            {
                return View(product);
            }

            else
            {
                context.Insert(product);
                context.Commit();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id); //find the product from the DataAccess in memory to edit


            if(product == null)
            {
                return HttpNotFound();
            }
 
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product,string Id)
        {
            Product productToEdit = context.Find(Id); //find the product from the DataAccess in memory to edit


            if (productToEdit == null)
            {
                return HttpNotFound();
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id); //find the product from the DataAccess in memory to edit


            if (productToDelete == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id); //find the product from the DataAccess in memory to edit


            if (productToDelete == null)
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