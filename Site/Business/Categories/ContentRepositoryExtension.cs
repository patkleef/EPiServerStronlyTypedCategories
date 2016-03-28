using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;

namespace Site.Business.Categories
{
    public static class ContentRepositoryExtension
    {
        private static IEnumerable<Category> _categories;

        private static IEnumerable<Category> _allCategories
        {
            get
            {
                if (_categories == null)
                {
                    var categoryRepository = ServiceLocator.Current.GetInstance<CategoryRepository>();
                    var rootCategory = categoryRepository.GetRoot();
                    _categories = rootCategory.GetList().Cast<Category>();
                }
                return _categories;
            }
        }

        public static IEnumerable<T> GetChildren<T>(this IContentRepository contentRepository,
            ContentReference contentReference, Type[] categoryTypes) where T : PageData
        {
            var categoryIds = categoryTypes.Select(GetCategoryIdFromType);

            return contentRepository.GetChildren<T>(contentReference)
                .Where(c => c.Category.MemberOfAny(categoryIds));
        }

        private static int GetCategoryIdFromType(Type type)
        {
            var attributes = type.GetCustomAttributes(false);

            var categoryTypeAttribute = attributes.OfType<CategoryTypeAttribute>().FirstOrDefault(a => a is CategoryTypeAttribute);
            if (categoryTypeAttribute != null)
            {
                return _allCategories.FirstOrDefault(c => c.GUID == new Guid(categoryTypeAttribute.Guid)).ID;
            }
            return -1;
        }
    }
}