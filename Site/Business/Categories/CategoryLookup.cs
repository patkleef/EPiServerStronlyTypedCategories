using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer.DataAbstraction;
using EPiServer.Framework.TypeScanner;
using EPiServer.ServiceLocation;

namespace Site.Business.Categories
{
    public class CategoryLookup
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly ITypeScannerLookup _typeScannerLookup;

        private readonly IList<Category> _allCategories;
        private readonly Category _rootCategory;

        public CategoryLookup()
        {
            _categoryRepository = ServiceLocator.Current.GetInstance<CategoryRepository>();
            _typeScannerLookup = ServiceLocator.Current.GetInstance<ITypeScannerLookup>();

            _rootCategory = _categoryRepository.GetRoot();
            _allCategories = _rootCategory.GetList().Cast<Category>().ToList();
        }

        /// <summary>
        /// Scan types that inherit from Category.
        /// Save the types as categories in EPiServer.
        /// </summary>
        public void Scan()
        {
            var sourceTypes = _typeScannerLookup.AllTypes.Where(
                t => typeof(Category).IsAssignableFrom(t) && t != typeof(Category));
            
            foreach (var type in sourceTypes)
            {
                var attributes = type.GetCustomAttributes();
                var categoryTypeAttribute = attributes.FirstOrDefault(a => a is CategoryTypeAttribute) as CategoryTypeAttribute;

                if (categoryTypeAttribute != null)
                {
                    CreateOrUpdateCategory(categoryTypeAttribute);
                }
            }
        }

        private void CreateOrUpdateCategory(CategoryTypeAttribute categoryTypeAttribute)
        {
            Category category = _allCategories.FirstOrDefault(c => c.GUID == new Guid(categoryTypeAttribute.Guid));

            if (category == null)
            {
                category = new Category();
                category.GUID = new Guid(categoryTypeAttribute.Guid);
                _allCategories.Add(category);
            }
            else
            {
                category = category.CreateWritableClone();
            }
            category.Name = categoryTypeAttribute.Name;
            category.Description = categoryTypeAttribute.Description;
            category.Parent = GetParentCategory(categoryTypeAttribute.Parent);

            _categoryRepository.Save(category);
        }

        private Category GetParentCategory(Type parentType)
        {
            if (parentType == null)
            {
                return _rootCategory;
            }
            var attributes = parentType.GetCustomAttributes();
            var categoryTypeAttribute = attributes.FirstOrDefault(a => a is CategoryTypeAttribute) as CategoryTypeAttribute;
            if (categoryTypeAttribute != null)
            {
                return FindCategoryByGuid(new Guid(categoryTypeAttribute.Guid));
            }
            return _rootCategory;
        }

        private Category FindCategoryByGuid(Guid guid)
        {
            return _allCategories.FirstOrDefault(c => c.GUID == guid);
        }
    }
}