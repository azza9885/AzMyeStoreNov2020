using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzMyeStoreNov2020.Core.Models
{
    public class ProductCategory : BaseEntity
    {
        //public string Id { get; set; }  // commenting this out since this is implemented in the base class
        public string Category { get; set; }

        // commenting this constructor out since the Id initialization is implemented in the base class
        //public ProductCategory()
        //{
        //    this.Id = Guid.NewGuid().ToString();
        //}
    }
}
