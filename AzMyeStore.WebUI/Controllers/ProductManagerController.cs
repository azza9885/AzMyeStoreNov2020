using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AzMyeStoreNov2020.Core.Contracts;
using AzMyeStoreNov2020.Core.Models;
using AzMyeStoreNov2020.Core.ViewModels;
using AzMyeStoreNov2020.DataAccess.InMemory;

namespace AzMyeStore.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        //InMemoryRepository<Product> context;
        //InMemoryRepository<ProductCategory> productCategories; // creating of context of ProductCategoryRepository so that it can be added to Create and Edit Action results
        //                                                       // to load the product categories from the database

        //public ProductManagerController()  // this constructor initializes the Product repository context
        //{
        //    // even though the decleration has changed to interface the rest of the code can work fine because we are instantiating the concrete 
        //    // implementation of the repository down here
        //    context = new InMemoryRepository<Product>();   
        //    productCategories = new InMemoryRepository<ProductCategory>();
        //    // if a method is added in the repository(Example : Product Repository) after the interface is extracted from the repository , it cannot be 
        //    // interacted with the context object
        //}

        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        // this constructor initializes the Product repository context
        // below we are injecting actual classes through the constructor
        // everytime we implement an instance of ProductManagerController it will need to inject a class of 
        // IRepository<Product> and IRepository<ProductCategory>
        // we need to get infront of the controller starter process for the compiler to know so that we can implement and inject these classes 
        // which can be done using a DI container
        // DI container will resolve the interfaces to the concrete classes
        // in particular we need a DI container that is MVC aware
        // We need a DI container which will inject concrete classes in place of the interfaces
        // out of the many option we will use microsoft version of DI container , which is Unity
        // Essentially we will create a container in which we will register concrete classes against the interfaces, the container will then pick up 
        // to resolve these dependencies as required,it will also take care of intercepting the MVC controller creation process in order to resolve 
        // these dependencies
        // Add Unity for the WebUI project instead of adding it to the entire solution.Add it to the project using Manage NuGet Packages
        // Add Unity.MVC and once this is added, check updates tab in NuGet Package Manager and update these two packages : Unity.Container,Unity.Abstractions

        // check WebUI -->App_Start --> UnityMVCActivator , what this file does is wiring up the Unity Injector i.e this is the bit where it inserts itself into
        // MVC pipeline, tell MVC to use the Dependency Resolver whenever the request comes into it
        // the actual mapping between interface and the concrete implementations is done in the other file , UnityConfig
        // UnityActivator calls UnityContainer Method which contains RegisterTypes which gets called from the activator and tells it to register all our types.
        // Use the example there and add the register types i.e this following piece of code :
        //container.RegisterType<IRepository<Product>, InMemoryRepository<Product>>();
        //container.RegisterType<IRepository<ProductCategory>, InMemoryRepository<ProductCategory>>();
        // once this is added whenever you have to make a change you can do it here in the Unity code. 

        
        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)  
        {
            context = productContext;
            productCategories = productCategoryContext;
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
            //Product product = new Product();
            //return View(product);

            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }

  
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)  // second method is to have the product details posted in 
        {
            if (!ModelState.IsValid)  // to check if there are any validation errors on the page , if there are any errors return the page with the errors
            {
                return View(product);
            }

            else
            {
                if(file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }
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
                //return View(product);
                // we are getting an empty product or the product we loaded from the database
                // and instead of using that product view , we are using the viewModel view to 
                // list the product and all the productCategories from the database.
                // since we are using a different view model, we need to update the view Pages
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product,string Id,HttpPostedFileBase file)
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

                if (file != null)
                {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
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