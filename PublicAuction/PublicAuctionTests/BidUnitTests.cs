// <copyright file="BidUnitTests.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the BidUnitTests test class.</summary>
namespace PublicAuctionTests
{
    using System.Linq;
    using NUnit.Framework;
    using PublicAuction.BusinessLayer;
    using PublicAuction.Data_Mapper;
    using PublicAuction.Domain_Model;
    using Telerik.JustMock.EntityFramework;

    /// <summary>Bid test class.</summary>
    [TestFixture]
    public class BidUnitTests
    {
        /// <summary>The library database mock.</summary>
        private AuctionDb auctionDbMock;

        /// <summary>The bid service.</summary>
        private BidService bidService;

        /// <summary>The Price. </summary>
        private Price price;

        /// <summary>The User. </summary>
        private User user;

        /// <summary>The Bid. </summary>
        private Bid bid;

        /// <summary>Sets up.</summary>
        [SetUp]
        public void SetUp()
        {
            this.auctionDbMock = EntityFrameworkMock.Create<AuctionDb>();
            EntityFrameworkMock.PrepareMock(this.auctionDbMock);
            this.bidService = new BidService(new BidRepository(this.auctionDbMock));

            this.price = new Price { ThePrice = 150, Currency = "USD" };

            this.user = new User { Id = 1, FirstName = "Dan", LastName = "Brown" };

            this.bid = new Bid();
            this.bid.User = this.user;
            this.bid.Price = this.price;
        }

        /// <summary>Tests the add bid.</summary>
        [Test]
        public void TestAddBid()
        {
            var result = this.bidService.AddBid(this.bid);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Prices.Count() == 0);
        }

        /// <summary>Tests the add null bid.</summary>
        [Test]
        public void TestAddNullBid()
        {
            var result = this.bidService.AddBid(null);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Prices.Count() == 0);
        }

        /// <summary>Tests the add null bid user.</summary>
        [Test]
        public void TestAddNullBidUser()
        {
            this.bid.User = null;
            var result = this.bidService.AddBid(bid);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Prices.Count() == 0);
        }

        /// <summary>Tests the add null bid user.</summary>
        [Test]
        public void TestAddNullBidPrice()
        {
            this.bid.Price = null;
            var result = this.bidService.AddBid(bid);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Prices.Count() == 0);
        }
    }
}