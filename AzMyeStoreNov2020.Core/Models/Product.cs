using System;
using System.Collections.Generic;
using System.ComponentModel;  // Added to get the StringLength decorator
using System.ComponentModel.DataAnnotations; // Added to get the DisplayName decorator
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzMyeStoreNov2020.Core.Models
{
    public class Product
    {
        public string Id { get; set; }

        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        public string Description  { get; set; }

        [Range(0, 1000)]
        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Image { get; set; }

        public Product()
        {

            this.Id = Guid.NewGuid().ToString();
        }
    }
}
