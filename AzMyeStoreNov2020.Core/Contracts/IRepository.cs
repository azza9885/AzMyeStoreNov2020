using AzMyeStoreNov2020.Core.Models;
using System.Linq;

namespace AzMyeStoreNov2020.Core.Contracts
{
    // this interface was extracted from the InMemoryRepository in AzMyeStoreNov2020.DataAccess.InMemory but was moved to the AzMyeStoreNov2020.Core project
    // so it is available in a common place where all the classes or methods can refer
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(string Id);
        T Find(string Id);
        void Insert(T t);
        void Update(T t);
    }
}