using AzMyeStoreNov2020.Core.Contracts;
using AzMyeStoreNov2020.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzMyeStoreNov2020.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        // to make this repository work we have to inject our DataContext class.We also need a way of mapping an underlying product to 
        // the underlying product table itself.We can do that using specical internam Data Context commands
        // we need some internal variables
        internal DataContext context; // this implements DB context
        internal DbSet<T> dbSet; // dbSet is the underlying table which we want to access

        public SQLRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public IQueryable<T> Collection()
        {
            return dbSet;  // returning the underlying table related to T
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            var t = Find(Id); // using the internal Find() method
            if (context.Entry(t).State == EntityState.Detached)  // we need to check the state of the entry
                dbSet.Attach(t);

            dbSet.Remove(t);
        }

        public T Find(string Id)
        {
            return dbSet.Find(Id); // dbSet has its own Find() method , so the Id can be passed in
        }

        public void Insert(T t)
        {
            dbSet.Add(t);
        }

        public void Update(T t)
        {
            //Update is a slightly more complex, with update we first need to touch our model and set its state to modified
            // so that entity framework knows how to update the underlying database
            // because entity framework essentially caches data and it doesnt actually immediately write it to the database
            // hence why we have the save changes method seperately
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
            //this tells entity framework that when we call the save changes method to look for the object t and persist to the Database
        }

        // Now after this you have to tell the WebUI to use SQL repository instead of in memory repository
        // since we are using dependency injection , it is simple
        // go to WebUI project --> App Start --> UnityConfig and make the switch to SQL db
    }
}
