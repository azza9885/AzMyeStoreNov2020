using AzMyeStoreNov2020.Core.Contracts;
using AzMyeStoreNov2020.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AzMyeStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        // added this piece of code from IRepository<Product> context till the constructor and replaced ProductManagerController with Home Controller
        // adding this piece since it is neccessary to add  the product list of the products already created on the home page
        // we are just hooking up our repositories ready to be injected using the Unity Injection system 
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
        }

        public ActionResult Index() // get a list of products and send it back to the main view
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
            // we wont use scaffolding to generate the view, we use the existing Home view and make the updates to the existing view
        }

        // creating a view for view products
        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            if(product == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}