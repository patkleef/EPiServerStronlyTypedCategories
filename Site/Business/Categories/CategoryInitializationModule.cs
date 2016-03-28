using System;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace Site.Business.Categories
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class CategoryInitializationModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var categoryLookup = new CategoryLookup();

            categoryLookup.Scan();
        }

        public void Uninitialize(InitializationEngine context)
        {

        }
    }
}