// <copyright file="PriceUnitTests.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the PriceUnitTests test class.</summary>
namespace PublicAuctionTests
{
    using System.Linq;
    using NUnit.Framework;
    using PublicAuction.BusinessLayer;
    using PublicAuction.Data_Mapper;
    using PublicAuction.Domain_Model;
    using Telerik.JustMock.EntityFramework;

    /// <summary>Price test class.</summary>
    [TestFixture]
    public class PriceUnitTests
    {
        /// <summary>The library database mock.</summary>
        private AuctionDb auctionDbMock;

        /// <summary>The price service.</summary>
        private PriceService priceService;

        /// <summary>Sets up.</summary>
        [SetUp]
        public void SetUp()
        {
            this.auctionDbMock = EntityFrameworkMock.Create<AuctionDb>();
            EntityFrameworkMock.PrepareMock(this.auctionDbMock);
            this.priceService = new PriceService(new PriceRepository(this.auctionDbMock));
        }

        /// <summary>Tests the add price.</summary>
        [Test]
        public void TestAddPrice()
        {
            var result = this.priceService.AddPrice(new Price { ThePrice = 300, Currency = "USD" });
            Assert.False(result);
            Assert.True(this.auctionDbMock.Prices.Count() != 0);
        }

        /// <summary>Tests the add null price.</summary>
        [Test]
        public void TestAddNullPrice()
        {
            var result = this.priceService.AddPrice(null);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Prices.Count() == 0);
        }

        /// <summary>Tests the add null currency.</summary>
        [Test]
        public void TestAddNullCurrency()
        {
            var price = new Price { Currency = null, ThePrice = 200.0 };
            var result = this.priceService.AddPrice(price);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Prices.Count() == 0);
        }

        /// <summary>Tests the add empty currency.</summary>
        [Test]
        public void TestAddEmptyCurrency()
        {
            var price = new Price { Currency = string.Empty, ThePrice = 200.0 };
            var result = this.priceService.AddPrice(price);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Prices.Count() == 0);
        }

        /// <summary>Tests the add price lower than system min.</summary>
        [Test]
        public void TestAddPriceLowerThanMin()
        {
            var price = new Price { Currency = string.Empty, ThePrice = 0 };
            var result = this.priceService.AddPrice(price);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Prices.Count() == 0);
        }
    }
}