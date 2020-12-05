using AzMyeStoreNov2020.Core.Contracts;
using AzMyeStoreNov2020.Core.Models;
using AzMyeStoreNov2020.DataAccess.InMemory;
using AzMyeStoreNov2020.DataAccess.SQL;
using System;

using Unity;

namespace AzMyeStore.WebUI
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            //container.RegisterType<IRepository<Product>, InMemoryRepository<Product>>();
            //container.RegisterType<IRepository<ProductCategory>, InMemoryRepository<ProductCategory>>();

            //above statements were prior to making the switch to SQL repository i.e while we were using InMemory repository

            container.RegisterType<IRepository<Product>, SQLRepository<Product>>();
            container.RegisterType<IRepository<ProductCategory>, SQLRepository<ProductCategory>>();

            // one thing to notice is even though SQLRepository we are injecting a DataContext , in the unity context we are not 
            // telling to specifically do that because the DI container actually figures out what needs to be injected and does it 
            // for us
        }
    }
}