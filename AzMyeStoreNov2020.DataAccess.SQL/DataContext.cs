using AzMyeStoreNov2020.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity; // Added this after adding DbContext 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzMyeStoreNov2020.DataAccess.SQL
{
    // this class is going to inherit from a data entity framework class called DbContext
    // DbContext includes all the base methods and actions we need for our data context to work
    public class DataContext : DbContext
    {
        // passing "DefaultConnection" in the constructor causes the underlying DbContext method
        // to look into the webconfig file for an area called "DefaultConnection"
        // Copy connection string data from webconfig file in WebUI folder to App.Config file 
        // in DataAccess.SQL folder
        public DataContext() : base("DefaultConnection") 
        {

        }

        // We need to tell our DataContext which model will be stored in tables, because sometimes
        // we dont want all our models in the databases , for example we dont want view models
        // in our databases, we can do this by explicitly specifying it using Dbset command

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        // next is the process to connect to the DB and create the physical tables
        // for this we will use Entity framework migration classes
        // we will have to execute some commands in packagemanager console
        // view --> other windows --> packagemanager console
        // we have to tell the entity framework about how to connect to the database
        // we can have to the project in the default project dropdown the project
        // which contains our data context classes in this case DataAccess.SQL project
        // to make sure it knows where to find the web.config file make sure Web.UI project
        // is set up as the start up project

        // list of commands to set up Database
        // Command : Enable-Migrations
        // this creates a special folder DataAccess.SQL project a folder called
        // migrations which contains a configuration.cs file
        // now we need to create Migrations class , which tells the DB how to create our tables

        // Command : Add-Migration Initial
        // creates a migration class with timestamp in the name 202012010556199_Initial.cs
        //  Command : Update-Database

    }
}
