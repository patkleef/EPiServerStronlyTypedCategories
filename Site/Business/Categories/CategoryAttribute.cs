using System;
using EPiServer.DataAbstraction;

namespace Site.Business.Categories
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CategoryTypeAttribute : Attribute
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Type Parent { get; set; }
    }
}