using System;
using System.Collections.Generic;
using System.ComponentModel;  // Added to get the StringLength decorator
using System.ComponentModel.DataAnnotations; // Added to get the DisplayName decorator
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzMyeStoreNov2020.Core.Models
{
    public class Product : BaseEntity
    {
        //public string Id { get; set; }  // commenting this out since this is implemented in the base class

        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        public string Description  { get; set; }

        [Range(0, 1000)]
        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Image { get; set; }

        // commenting this constructor out since the Id initialization is implemented in the base class
        //public Product()
        //{

        //    this.Id = Guid.NewGuid().ToString();
        //}
    }
}
