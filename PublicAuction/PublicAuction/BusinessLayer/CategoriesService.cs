// <copyright file="CategoriesService.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the CategoriesService class.</summary>
namespace PublicAuction.BusinessLayer
{
    using System.Collections.Generic;
    using System.Linq;

    using Castle.Core.Internal;

    using PublicAuction.Data_Mapper;
    using PublicAuction.Domain_Model;

    /// <summary>Categories Service class using category repository.</summary>
    public class CategoriesService
    {
        /// <summary>The category repository.</summary>
        private CategoriesRepository _categoryRepository;

        /// <summary>Initializes a new instance of the <see cref="CategoriesService"/> class.</summary>
        /// <param name="categoryRepository">The category repository.</param>
        public CategoriesService(CategoriesRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>Adds the category.</summary>
        /// <param name="category">The category.</param>
        /// <returns>A boolean value if category added successfully.</returns>
        public bool AddCategory(Category category)
        {
            if (category == null)
            {
                LoggerUtil.LogInfo("Category can not be null!");
                return false;
            }

            if (category.CategoryName.IsNullOrEmpty())
            {
                LoggerUtil.LogInfo("CategoryName can not be null!");
                return false;
            }

            if ((category.CategoryName.Length < 3) || (category.CategoryName.Length > 80))
            {
                LoggerUtil.LogInfo("CategoryName can not be under 3 characters!");
                return false;
            }

            if (category.CategoryName.Any(c => !(char.IsLetter(c) || char.IsWhiteSpace(c))))
            {
                LoggerUtil.LogInfo("CategoryName can not be under 3 characters!");
                return false;
            }

            return this._categoryRepository.AddCategory(category);
        }

        /// <summary>Determines whether [is part of category] [the specified product].</summary>
        /// <param name="product">The product is part of category.</param>
        /// <param name="category">The category.</param>
        /// <returns>
        /// <c>true</c> if [is part of category] [the specified product]; otherwise, <c>false</c>.</returns>
        public bool IsPartOfCategory(Product product, Category category)
        {
            return CategoryIsPartOfCategories(category, product.Category.ToList());
        }

        /// <summary>Categories the is part of categories.</summary>
        /// <param name="category">The category.</param>
        /// <param name="categories">The categories.</param>
        /// <returns>Boolean value if a category is part of a list of categories.</returns>
        public bool CategoryIsPartOfCategories(Category category, List<Category> categories)
        {
            while (category != null)
            {
                if (CheckCategories(category, categories))
                {
                    return true;
                }

                category = category.ParentCategory;
            }

            return false;
        }

        /// <summary>Gets the category.</summary>
        /// <param name="name">The category name.</param>
        /// <returns>Category from database.</returns>
        public Category GetCategory(string name)
        {
            if (name.IsNullOrEmpty())
            {
                return null;
            }

            return this._categoryRepository.GetCategory(name);
        }

        /// <summary>Checks the categories.</summary>
        /// <param name="category">The category.</param>
        /// <param name="categories">The categories.</param>
        /// <returns>True is category found in a list of categories.</returns>
        private bool CheckCategories(Category category, List<Category> categories)
        {
            foreach (var productCategory in categories)
            {
                var currentCategory = productCategory;

                while (currentCategory != null)
                {
                    if (category.CategoryName.Equals(currentCategory.CategoryName))
                    {
                        return true;
                    }

                    currentCategory = currentCategory.ParentCategory;
                }
            }

            return false;
        }
    }
}