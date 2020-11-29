using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzMyeStoreNov2020.Core.Models
{
    public abstract class BaseEntity
    {
        //abstract class - this implies that we can never create an instance of Base Entity on its own and it can only be inherited
        // i.e we can only create a class that implements it
        //creating this base class since when creating the generic repository(InMemoryRepository) we are trying to find items using an Id.
        //and that generic method does not expect an Id unless it is directed by a baseclass to always expect an Id property
        public string Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedAt = DateTime.Now;
        }
    }
}
