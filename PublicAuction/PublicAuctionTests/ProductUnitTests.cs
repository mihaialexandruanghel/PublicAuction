// <copyright file="ProductUnitTests.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the ProductUnitTests test class.</summary>
namespace PublicAuctionTests
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using NUnit.Framework;
    using PublicAuction.BusinessLayer;
    using PublicAuction.Data_Mapper;
    using PublicAuction.Domain_Model;
    using Telerik.JustMock.EntityFramework;

    /// <summary>Product test class.</summary>
    [TestFixture]
    public class ProductUnitTests
    {
        /// <summary>The product service.</summary>
        private ProductService productService;

        /// <summary>The category.</summary>
        private Category category;

        /// <summary>The auction database mock..</summary>
        private AuctionDb auctionDbMock;

        /// <summary>Sets up.</summary>
        [SetUp]
        public void SetUp()
        {
            this.category = new Category { CategoryName = "Electronics" };
            this.auctionDbMock = EntityFrameworkMock.Create<AuctionDb>();
            EntityFrameworkMock.PrepareMock(this.auctionDbMock);
            this.productService = new ProductService(
                new ProductRepository(this.auctionDbMock),
                new CategoriesService(new CategoriesRepository(this.auctionDbMock)));
        }

        /// <summary>Tests the add null product.</summary>
        [Test]
        public void TestAddNullProduct()
        {
            Product product = null;
            var result = this.productService.AddProduct(product);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Products.Count() == 0);
        }

        /// <summary>Tests the add product with null categories.</summary>
        [Test]
        public void TestAddProductWithNullCategories()
        {
            var product = new Product();
            product.Name = "Dacia Logan";
            product.Price = new Price { ThePrice = 200.0, Currency = "USD" };
            product.Category = null;
            var result = this.productService.AddProduct(product);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Products.Count() == 0);
        }

        /// <summary>Tests the add product with no category.</summary>
        [Test]
        public void TestAddProductWithNoCategory()
        {
            var product = new Product();
            product.Name = "Dacia Logan";
            product.Price = new Price { ThePrice = 200.0, Currency = "USD" };
            product.Category = new List<Category>();
            var result = this.productService.AddProduct(product);
            Assert.False(result);
        }

        /// <summary>Tests the add product with one category.</summary>
        [Test]
        public void TestAddProductWithOneCategory()
        {
            var product = new Product();
            product.Name = "Dacia Logan";
            product.Price = new Price { ThePrice = 200.0, Currency = "USD" };
            product.Category = new List<Category> { this.category };
            this.productService.AddProduct(product);
            Assert.True(this.auctionDbMock.Products.Count() == 1);
        }

        /// <summary>Tests the add product with null price.</summary>
        [Test]
        public void TestAddProductNullPrice()
        {
            var product = new Product();
            product.Name = "Dacia Logan";
            product.Price = null;
            product.Category = new List<Category> { this.category };
            var result = this.productService.AddProduct(product);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Products.Count() == 0);
        }

        /// <summary>Tests the empty name of the add product with.</summary>
        [Test]
        public void TestAddProductWithEmptyName()
        {
            var product = new Product();
            product.Name = string.Empty;
            product.Price = new Price { ThePrice = 200.0, Currency = "USD" };
            product.Category = new List<Category> { this.category };
            var result = this.productService.AddProduct(product);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Products.Count() == 0);
        }

        /// <summary>Tests the name of the add product with no.</summary>
        [Test]
        public void TestAddProductWithNoName()
        {
            var product = new Product();
            product.Name = null;
            product.Price = new Price { ThePrice = 200.0, Currency = "USD" };
            product.Category = new List<Category> { this.category };
            var result = this.productService.AddProduct(product);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Products.Count() == 0);
        }

        /// <summary>Tests the add product with name length less than three.</summary>
        [Test]
        public void TestAddProductWithNameLengthLessThanThree()
        {
            var product = new Product();
            product.Name = "Da";
            product.Price = new Price { ThePrice = 200.0, Currency = "USD" };
            product.Category = new List<Category> { this.category };
            var result = this.productService.AddProduct(product);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Products.Count() == 0);
        }

        /// <summary>Tests the add product with name length bigger than 100.</summary>
        [Test]
        public void TestAddProductWithNameLengthBiggerThanEighty()
        {
            var product = new Product();
            product.Name = "Aaaaaaaaaaaaaaaaaaaaaaaaaa" + "aaaaaaaaaaaaaaaaa" + "aaaaaaaaaaaaaaaaaaaaaaaaa"
                                     + "aaaaaaaaaaaaaaaaaaaaaa" + "anaaremereanaaremere" + "anaaremereanaaremere";
            product.Price = new Price { ThePrice = 200.0, Currency = "USD" };
            product.Category = new List<Category> { this.category };
            var result = this.productService.AddProduct(product);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Products.Count() == 0);
        }

        /// <summary>Tests the add product name with digits.</summary>
        [Test]
        public void TestAddProductNameWithDigits()
        {
            var product = new Product();
            product.Name = "23";
            product.Price = new Price { ThePrice = 200.0, Currency = "USD" };
            product.Category = new List<Category> { this.category };
            var result = this.productService.AddProduct(product);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Products.Count() == 0);
        }

        /// <summary>Tests the add product name with symbols.</summary>
        [Test]
        public void TestAddProductNameWithSymbols()
        {
            var product = new Product();
            product.Name = "^$#@";
            product.Price = new Price { ThePrice = 200.0, Currency = "USD" };
            product.Category = new List<Category> { this.category };
            var result = this.productService.AddProduct(product);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Products.Count() == 0);
        }

        /// <summary>Tests the add product with name contains whitespaces.</summary>
        [Test]
        public void TestAddProductWithNameContainsWhitespaces()
        {
            var product = new Product();
            product.Name = "Dacia logan";
            product.Price = new Price { ThePrice = 200.0, Currency = "USD" };
            product.Category = new List<Category> { this.category };
            var result = this.productService.AddProduct(product);
            Assert.False(this.auctionDbMock.Products.Count() == 0);
        }

        /// <summary>Tests the add product with related categories.</summary>
        [Test]
        public void TestAddProductWithRelatedCategories()
        {
            var product = new Product();
            product.Name = "Dacia logan";
            product.Price = new Price { ThePrice = 200.0, Currency = "USD" };
            product.Category = new List<Category>
                                        {
                                            this.category,
                                            new Category { CategoryName = "Romance", ParentCategory = this.category },
                                        };
            var result = this.productService.AddProduct(product);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Products.Count() == 0);
        }

        /// <summary>Tests the add product with related indirect categories.</summary>
        [Test]
        public void TestAddProductWithRelatedIndirectCategories()
        {
            var product = new Product();
            product.Name = "Dacia logan";
            product.Price = new Price { ThePrice = 200.0, Currency = "USD" };
            product.Category = new List<Category>
                                        {
                                            this.category,
                                            new Category
                                            {
                                                CategoryName = "Romance",
                                                ParentCategory =
                                                    new Category { ParentCategory = this.category, CategoryName = "Horror" },
                                            },
                                        };
            var result = this.productService.AddProduct(product);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Products.Count() == 0);
        }

        /// <summary>Tests the get product.</summary>
        [Test]
        public void TestGetProduct()
        {
            var product = new Product();
            product.Id = 1;
            product.Name = "Dacia logan";
            product.Price = new Price { ThePrice = 200.0, Currency = "USD" };
            product.Category = new List<Category> { this.category };
            this.productService.AddProduct(product);
            product = this.productService.GetProduct(product.Id);
            Assert.NotNull(product);
        }

        /// <summary>Tests the get product with zero value id.</summary>
        [Test]
        public void TestGetProductWithNegativeId()
        {
            var product = this.productService.GetProduct(0);
            Assert.Null(product);
        }

        /// <summary>Tests the get product with negative value.</summary>
        [Test]
        public void TestGetProductWithNegativeValue()
        {
            var product = this.productService.GetProduct(-1);
            Assert.Null(product);
        }
    }
}