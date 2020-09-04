// <copyright file="CategoriesUnitTests.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the CategoriesUnitTests test class.</summary>
namespace PublicAuctionTests
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using PublicAuction.BusinessLayer;
    using PublicAuction.Data_Mapper;
    using PublicAuction.Domain_Model;
    using Telerik.JustMock.EntityFramework;

    /// <summary>Category test class.</summary>
    [TestFixture]
    public class CategoriesUnitTests
    {
        /// <summary>The categories service</summary>
        private CategoriesService _categoriesService;

        /// <summary>The library database mock</summary>
        private AuctionDb _auctionDbMock;

        /// <summary>Sets up.</summary>
        [SetUp]
        public void SetUp()
        {
            this._auctionDbMock = EntityFrameworkMock.Create<AuctionDb>();

            this._categoriesService = new CategoriesService(new CategoriesRepository(this._auctionDbMock));
        }

        /// <summary>Tests the category is part of sub category.</summary>
        [Test]
        public void TestCategoryIsPartOfSubCategory()
        {
            var c1 = new Category { CategoryName = "C1" };
            var c2 = new Category { CategoryName = "C2" };
            var c3 = new Category { CategoryName = "C3" };
            var c4 = new Category { CategoryName = "C4", ParentCategory = c1 };

            var result = this._categoriesService.CategoryIsPartOfCategories(c4, new List<Category> { c2, c3, c1 });

            Assert.True(result);
        }

        /// <summary>Test is part of category.</summary>
        [Test]
        public void TestIsPartOfCategory()
        {
            var category = new Category { CategoryName = "Cars" };
            var category2 = new Category { CategoryName = "Ro Cars", ParentCategory = category };
            var product = new Product();
            product.Name = "Dacia Logan";
            product.Price = new Price { ThePrice = 200.0, Currency = "USD" };
            product.Category = new List<Category> { category2 };
            var result = this._categoriesService.IsPartOfCategory(product, category2);

            Assert.True(result);
        }

        /// <summary>Tests the parent category is part of sub category.</summary>
        [Test]
        public void TestParentCategoryIsPartOfSubCategory()
        {
            var c1 = new Category { CategoryName = "C1" };
            var c2 = new Category { CategoryName = "C2", ParentCategory = c1 };
            var c3 = new Category { CategoryName = "C3", ParentCategory = c1 };
            var c4 = new Category { CategoryName = "C4", ParentCategory = c1 };

            var result = this._categoriesService.CategoryIsPartOfCategories(c1, new List<Category> { c2, c3, c4 });

            Assert.True(result);
        }

        /// <summary>Tests the category is not part of sub category.</summary>
        [Test]
        public void TestCategoryIsNotPartOfSubCategory()
        {
            var c1 = new Category { CategoryName = "C1" };
            var c2 = new Category { CategoryName = "C2" };
            var c3 = new Category { CategoryName = "C3" };

            var result = this._categoriesService.CategoryIsPartOfCategories(c1, new List<Category> { c2, c3 });

            Assert.False(result);
        }

        /// <summary>Adds the null category.</summary>
        [Test]
        public void AddNullCategory()
        {
            var result = this._categoriesService.AddCategory(null);
            Assert.False(result);
            Assert.True(this._auctionDbMock.Categories.Count() == 0);
        }

        /// <summary>Adds the name of the category with null.</summary>
        [Test]
        public void AddCategoryWithNullName()
        {
            var c = new Category { CategoryName = null };
            var result = this._categoriesService.AddCategory(c);
            Assert.False(result);
            Assert.True(this._auctionDbMock.Categories.Count() == 0);
        }

        /// <summary>Adds the empty name of the category with.</summary>
        [Test]
        public void AddCategoryWithEmptyName()
        {
            var c = new Category { CategoryName = string.Empty };
            var result = this._categoriesService.AddCategory(c);
            Assert.False(result);
            Assert.True(this._auctionDbMock.Categories.Count() == 0);
        }

        /// <summary>Adds the category name length less than three.</summary>
        [Test]
        public void AddCategoryNameLengthLessThanThree()
        {
            var c = new Category { CategoryName = "Da" };
            var result = this._categoriesService.AddCategory(c);
            Assert.False(result);
            Assert.True(this._auctionDbMock.Categories.Count() == 0);
        }

        /// <summary>Adds the category name length more than limit eighty.</summary>
        [Test]
        public void AddCategoryNameLengthMoreThanLimitEighty()
        {
            var c = new Category
                    {
                CategoryName = "DraDraDraDraDraDraDraDraDraDraDraDraDraDraDraDr" + "DraDraDraDraDraDraDraDra"
                                                                                 + "DraDraDraDraDraDra"
                                                                                 + "DraDraDraDraDra"
                                                                                 + "DraDraDraDraDraDraDraaDraDr"
                                                                                 + "DraDraDraDraDraDraDraDraDra"
                                                                                 + "aDraDraDraDraDraDraDraDraDraDraDra"
                    };
            var result = this._categoriesService.AddCategory(c);
            Assert.False(result);
            Assert.True(this._auctionDbMock.Categories.Count() == 0);
        }

        /// <summary>Adds the category name with digit.</summary>
        [Test]
        public void AddCategoryNameWithDigit()
        {
            var c = new Category { CategoryName = "2425" };
            var result = this._categoriesService.AddCategory(c);
            Assert.False(result);
            Assert.True(this._auctionDbMock.Categories.Count() == 0);
        }

        /// <summary>Adds the category name with symbol.</summary>
        [Test]
        public void AddCategoryNameWithSymbol()
        {
            var c = new Category { CategoryName = "@&kfgdg" };
            var result = this._categoriesService.AddCategory(c);
            Assert.False(result);
            Assert.True(this._auctionDbMock.Categories.Count() == 0);
        }

        /// <summary>Adds the category name with white space.</summary>
        [Test]
        public void AddCategoryNameWithWhiteSpace()
        {
            var c = new Category { CategoryName = "Drama and thriller" };
            var result = this._categoriesService.AddCategory(c);
            Assert.False(this._auctionDbMock.Categories.Count() == 0);
        }

        /// <summary>Adds the category.</summary>
        [Test]
        public void AddCategory()
        {
            var c = new Category { CategoryName = "Science" };
            var product = new Product();
            product.Name = "Dacia Logan";
            product.Price = new Price { ThePrice = 200.0, Currency = "USD" };
            c.Products = new List<Product> { product };
            var result = this._categoriesService.AddCategory(c);
            Assert.True(this._auctionDbMock.Categories.Count() == 1);
        }

        /// <summary>Gets the category.</summary>
        [Test]
        public void GetCategory()
        {
            var c = new Category { CategoryName = "Science" };
            var result = this._categoriesService.AddCategory(c);

            c = this._categoriesService.GetCategory(c.CategoryName);
            Assert.NotNull(c);
        }

        /// <summary>Gets the null category.</summary>
        [Test]
        public void GetNullCategory()
        {
            var c = this._categoriesService.GetCategory(null);
            Assert.Null(c);
        }

        /// <summary>Gets the empty category.</summary>
        [Test]
        public void GetEmptyCategory()
        {
            var c = this._categoriesService.GetCategory(string.Empty);
            Assert.Null(c);
        }

        /// <summary>Gets the unknown category.</summary>
        [Test]
        public void GetUnknownCategory()
        {
            var c = new Category { CategoryName = "Science" };
            var result = this._categoriesService.AddCategory(c);

            c = this._categoriesService.GetCategory("Philosophy");
            Assert.Null(c);
        }
    }
}