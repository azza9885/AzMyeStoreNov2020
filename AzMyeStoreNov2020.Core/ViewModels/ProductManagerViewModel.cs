using AzMyeStoreNov2020.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzMyeStoreNov2020.Core.ViewModels
{
    public class ProductManagerViewModel
    {
        // this method is being added so that the Product category list created can be sent over to the Product create or Product Edit view 
        public Product Product { get; set; } // an object of Product type
        public IEnumerable<ProductCategory> ProductCategories { get; set; }  // an IEnumerable list of ProductCategory type which we can iterate through

        // now we need to update the ProductManagerController so that it can use the ProductManagerViewModel, so that we can pass both the product and ProductCategory list
        // through it
    }
}
