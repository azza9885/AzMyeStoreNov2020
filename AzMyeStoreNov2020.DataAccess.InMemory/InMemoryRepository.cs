using AzMyeStoreNov2020.Core.Contracts;
using AzMyeStoreNov2020.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace AzMyeStoreNov2020.DataAccess.InMemory
{
    // Dependency Inversion principle states you should code against Interfaces and not concrete classes, Core C# Object oriented programming pattern
    // interface is a list of all methods and properties that a class exposes but it doesnt implement, instead we create another concrete class 
    // that implements them
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity  // T indicates we are creating a generic class , T is just an identifier it can be any letter or word 
                                                                                      // to indicate that it is going to be a generic class
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;  // a  generic list place holder which can be used to created a list of products or product categories and can be extended further
        string className; // used to specify which the cache object , to specify which cache object it is going to save for example Product or Product Category

        //constructor to initialize our repository
        public InMemoryRepository()
        {
            // using the concept of reflection
            className = typeof(T).Name;  // used to get the actual name of the class
            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T tToUpdate = items.Find(p => p.Id == t.Id);

            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        public T Find(string Id)
        {
            T tToFind = items.Find(p => p.Id == Id);

            if (tToFind != null)
            {
                return tToFind;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            T tToDelete = items.Find(p => p.Id == Id);
            if (tToDelete != null)
            {
                items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }
    }
}
