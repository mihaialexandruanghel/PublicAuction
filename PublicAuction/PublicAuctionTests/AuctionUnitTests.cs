// <copyright file="AuctionUnitTests.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the AuctionUnitTests test class.</summary>
namespace PublicAuctionTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using PublicAuction.BusinessLayer;
    using PublicAuction.Data_Mapper;
    using PublicAuction.Domain_Model;
    using Telerik.JustMock.EntityFramework;

    /// <summary>Auction test class.</summary>
    [TestFixture]
    public class AuctionUnitTests
    {
        /// <summary>The library database mock.</summary>
        private AuctionDb auctionDbMock;

        /// <summary>The Auction service.</summary>
        private AuctionService auctionService;

        /// <summary>The Price. </summary>
        private Price price;

        /// <summary>The User. </summary>
        private User user;

        /// <summary>The Product. </summary>
        private Product product;

        /// <summary>The Category. </summary>
        private Category category;

        /// <summary>The Auction. </summary>
        private Auction auction;

        /// <summary>Sets up.</summary>
        [SetUp]
        public void SetUp()
        {
            this.auctionDbMock = EntityFrameworkMock.Create<AuctionDb>();

            this.auctionService = new AuctionService(new AuctionRepository(this.auctionDbMock));

            this.price = new Price { ThePrice = 150, Currency = "USD" };

            this.user = new User { Id = 1, FirstName = "Dan", LastName = "Brown" };

            this.category = new Category { CategoryName = "Electronics" };

            this.product = new Product();

            this.product.Name = "Masina";

            this.product.Price = new Price { ThePrice = 200.0, Currency = "USD" };

            List<Category> categories = new List<Category>();

            categories.Add(this.category);

            this.product.Category = categories;

            Dictionary<Category, int> categoryAuctions = new Dictionary<Category, int>();
            categoryAuctions.Add(this.product.Category[0], 1);
            this.user.StartedSpecifiedCategoryAuctions = categoryAuctions;

            DateTime sd = new DateTime(2020, 10, 23);
            DateTime ed = new DateTime(2020, 11, 23);
            this.auction = new Auction();
            this.auction.Id = 1;
            this.auction.Name = "auction one";
            this.auction.StartDate = sd;
            this.auction.EndDate = ed;
            this.auction.StartingPrice = this.price;
            this.auction.OfferUser = this.user;
            this.auction.Product = this.product;
            Bid bid = new Bid();
            User newUser = new User();
            newUser.FirstName = "Mihai";
            newUser.LastName = "Anghel";
            Price price = new Price();
            price.Currency = "USD";
            price.ThePrice = 210;
            bid.User = newUser;
            bid.Price = price;
            this.auction.Bid = bid;
        }

        /// <summary>Tests the add null auction.</summary>
        [Test]
        public void TestAddNullAuction()
        {
            Auction auction = null;
            var result = this.auctionService.AddAuction(auction);
            Assert.False(result);
            Assert.True(this.auctionDbMock.Auctions.Count() == 0);
        }

        /// <summary>Tests the add auction.</summary>
        [Test]
        public void TestAddAuction()
        {
            var auction = this.auction;
            this.auctionService.AddAuction(auction);
            Assert.True(this.auctionDbMock.Auctions.Count() == 1);
        }

        /// <summary>Tests the name of the add null.</summary>
        [Test]
        public void TestAddAuctionNullName()
        {
            var auction = this.auction;
            auction.Name = null;
            this.auctionService.AddAuction(auction);
            Assert.True(this.auctionDbMock.Auctions.Count() == 0);
        }

        /// <summary>Tests the empty name of the add auction.</summary>
        [Test]
        public void TestAddAuctionEmptyName()
        {
            var auction = this.auction;
            auction.Name = string.Empty;
            this.auctionService.AddAuction(auction);
            Assert.True(this.auctionDbMock.Auctions.Count() == 0);
        }

        /// <summary>Tests the name of the add auction small.</summary>
        [Test]
        public void TestAddAuctionSmallName()
        {
            var auction = this.auction;
            auction.Name = "a";
            this.auctionService.AddAuction(auction);
            Assert.True(this.auctionDbMock.Auctions.Count() == 0);
        }

        /// <summary>Tests the long name of the add auction.</summary>
        [Test]
        public void TestAddAuctionLongName()
        {
            var auction = this.auction;
            auction.Name = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            this.auctionService.AddAuction(auction);
            Assert.True(this.auctionDbMock.Auctions.Count() == 0);
        }

        /// <summary>Tests the add auction name symbol.</summary>
        [Test]
        public void TestAddAuctionNameSymbol()
        {
            var auction = this.auction;
            auction.Name = "#$%^&*";
            this.auctionService.AddAuction(auction);
            Assert.True(this.auctionDbMock.Auctions.Count() == 0);
        }

        /// <summary>Tests the add edition auction lower case.</summary>
        [Test]
        public void TestAddAuctionNameLowerCase()
        {
            var auction = this.auction;
            auction.Name = "auction one";
            this.auctionService.AddAuction(auction);
            Assert.True(this.auctionDbMock.Auctions.Count() == 1);
        }

        /// <summary>Tests the type of the add auction null price.</summary>
        [Test]
        public void TestAddAuctionNullPrice()
        {
            var auction = this.auction;
            auction.StartingPrice = null;
            this.auctionService.AddAuction(auction);
            Assert.True(this.auctionDbMock.Auctions.Count() == 0);
        }

        /// <summary>Tests the type of the add auction null user.</summary>
        [Test]
        public void TestAddAuctionNullUser()
        {
            var auction = this.auction;
            auction.OfferUser = null;
            this.auctionService.AddAuction(auction);
            Assert.True(this.auctionDbMock.Auctions.Count() == 0);
        }

        /// <summary>Tests the type of the add auction null product.</summary>
        [Test]
        public void TestAddAuctionNullProduct()
        {
            var auction = this.auction;
            auction.Product = null;
            this.auctionService.AddAuction(auction);
            Assert.True(this.auctionDbMock.Auctions.Count() == 0);
        }

        /// <summary>Tests the type of the add auction default start date.</summary>
        [Test]
        public void TestAddAuctionWithDefaultStartDate()
        {
            var auction = this.auction;
            auction.StartDate = default;
            this.auctionService.AddAuction(auction);
            Assert.True(this.auctionDbMock.Auctions.Count() == 0);
        }

        /// <summary>Tests the type of the add auction default end date.</summary>
        [Test]
        public void TestAddAuctionWithDefaultEndDate()
        {
            var auction = this.auction;
            auction.EndDate = default;
            this.auctionService.AddAuction(auction);
            Assert.True(this.auctionDbMock.Auctions.Count() == 0);
        }

        /// <summary>Tests add start date in the past.</summary>
        [Test]
        public void TestAddAuctionWithPastStartDate()
        {
            DateTime sd = new DateTime(2019, 10, 23);
            DateTime ed = new DateTime(2020, 11, 23);
            var auction = this.auction;
            auction.StartDate = sd;
            auction.EndDate = ed;
            this.auctionService.AddAuction(auction);
            Assert.True(this.auctionDbMock.Auctions.Count() == 0);
        }

        /// <summary>Tests add start date is before end date.</summary>
        [Test]
        public void TestAddAuctionWithStartDateIsAfterEndDate()
        {
            DateTime sd = new DateTime(2020, 10, 23);
            DateTime ed = new DateTime(2019, 11, 23);
            var auction = this.auction;
            auction.StartDate = sd;
            auction.EndDate = ed;
            this.auctionService.AddAuction(auction);
            Assert.True(this.auctionDbMock.Auctions.Count() == 0);
        }

        /// <summary>Tests period between start date and end date.</summary>
        [Test]
        public void TestAddAuctionWithMaxPeriodExceeded()
        {
            DateTime sd = new DateTime(2021, 01, 23);
            DateTime ed = new DateTime(2021, 11, 23);
            var auction = this.auction;
            auction.StartDate = sd;
            auction.EndDate = ed;
            this.auctionService.AddAuction(auction);
            Assert.True(this.auctionDbMock.Auctions.Count() == 0);
        }

        /// <summary>Tests if user.startedAuction is bigger than system max.</summary>
        [Test]
        public void TestAddAuctionWithUserWithStartedAuctionsMax()
        {
            var user = new User { Id = 1, FirstName = "Dan", LastName = "Brown", StartedAuctions = 5 };
            var auction = this.auction;
            auction.OfferUser = user;
            this.auctionService.AddAuction(auction);
            Assert.True(this.auctionDbMock.Auctions.Count() == 0);
        }

        /// <summary>Tests if user.startedSpecifiedCategoryAuctions is bigger than system max.</summary>
        [Test]
        public void TestAddAuctionWithUserWithStartedSpecifiedCategoryAuctionsMax()
        {
            Dictionary<Category, int> categoryAuctions = new Dictionary<Category, int>();
            categoryAuctions.Add(this.product.Category[0], 5);
            var user = new User { Id = 1, FirstName = "Dan", LastName = "Brown", StartedSpecifiedCategoryAuctions = categoryAuctions };
            var auction = this.auction;
            auction.OfferUser = user;
            this.auctionService.AddAuction(auction);
            Assert.True(this.auctionDbMock.Auctions.Count() == 0);
        }

        /// <summary>Gets the auction.</summary>
        [Test]
        public void GetAuction()
        {
            var auction = this.auction;
            this.auctionService.AddAuction(auction);
            var result = this.auctionService.GetAuction(auction.Id);
            Assert.NotNull(result);
        }

        /// <summary>Gets the 0 value id auction.</summary>
        [Test]
        public void GetNullAuction()
        {
            var auction = this.auctionService.GetAuction(0);
            Assert.Null(auction);
        }

        /// <summary>Gets the negative value id auction.</summary>
        [Test]
        public void GetNegativeIdAuction()
        {
            var auction = this.auctionService.GetAuction(-1);
            Assert.Null(auction);
        }

        /// <summary>Test start null auction.</summary>
        [Test]
        public void StartNullAuction()
        {
            try
            {
                Auction auction = null;
                this.auctionService.StartAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction.</summary>
        [Test]
        public void StartAuction()
        {
            try
            {
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction.</summary>
        [Test]
        public void ContinueAuction()
        {
            try
            {
                var auction = this.auction;
                auction.Bid = null;
                this.auctionService.ContinueAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test auction process.</summary>
        [Test]
        public void AuctionProcessExit()
        {
            try
            {
                var auction = this.auction;
                this.auctionService.AuctionProcess(auction, 5);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test auction process.</summary>
        [Test]
        public void AuctionProcessCheckBid()
        {
            try
            {
                var auction = this.auction;
                this.auctionService.AuctionProcess(auction, 2);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction null price.</summary>
        [Test]
        public void StartAuctionNullPrice()
        {
            try
            {
                auction.StartingPrice = null;
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction null price.</summary>
        [Test]
        public void ContinueAuctionNullPrice()
        {
            try
            {
                auction.StartingPrice = null;
                this.auctionService.ContinueAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction null name.</summary>
        [Test]
        public void StartAuctionNullName()
        {
            try
            {
                auction.Name = null;
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction null name.</summary>
        [Test]
        public void ContinueAuctionNullName()
        {
            try
            {
                auction.Name = null;
                this.auctionService.ContinueAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction empty price currency.</summary>
        [Test]
        public void StartAuctionEmptyCurrency()
        {
            try
            {
                this.auction.StartingPrice.Currency = string.Empty;
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction empty price currency.</summary>
        [Test]
        public void ContinueAuctionEmptyCurrency()
        {
            try
            {
                this.auction.StartingPrice.Currency = string.Empty;
                this.auctionService.ContinueAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction empty name.</summary>
        [Test]
        public void StartAuctionEmptyName()
        {
            try
            {
                auction.Name = string.Empty;
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction empty name.</summary>
        [Test]
        public void ContinueAuctionEmptyName()
        {
            try
            {
                auction.Name = string.Empty;
                this.auctionService.ContinueAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction small name.</summary>
        [Test]
        public void StartAuctionSmallName()
        {
            try
            {
                auction.Name = "a";
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction small name.</summary>
        [Test]
        public void ContinueAuctionSmallName()
        {
            try
            {
                auction.Name = "a";
                this.auctionService.ContinueAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction big name.</summary>
        [Test]
        public void StartAuctionBigName()
        {
            try
            {
                auction.Name = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction big name.</summary>
        [Test]
        public void ContinueAuctionBigName()
        {
            try
            {
                auction.Name = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
                this.auctionService.ContinueAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction lower case name.</summary>
        [Test]
        public void StartAuctionLowerCaseName()
        {
            try
            {
                auction.Name = "auction one";
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction lower case name.</summary>
        [Test]
        public void ContinueAuctionLowerCaseName()
        {
            try
            {
                auction.Name = "auction one";
                this.auctionService.ContinueAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction symbol name.</summary>
        [Test]
        public void StartAuctionSymbolName()
        {
            try
            {
                auction.Name = "#$%^&*";
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction symbol name.</summary>
        [Test]
        public void ContinueAuctionSymbolName()
        {
            try
            {
                auction.Name = "#$%^&*(";
                this.auctionService.ContinueAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction max period exceeded.</summary>
        [Test]
        public void StartAuctionMaxPeriodExceeded()
        {
            try
            {
                DateTime sd = new DateTime(2020, 10, 23);
                DateTime ed = new DateTime(2021, 11, 23);
                var auction = this.auction;
                auction.StartDate = sd;
                auction.EndDate = ed;
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction max period exceeded.</summary>
        [Test]
        public void ContinueAuctionMaxPeriodExceeded()
        {
            try
            {
                DateTime sd = new DateTime(2020, 10, 23);
                DateTime ed = new DateTime(2021, 11, 23);
                var auction = this.auction;
                auction.StartDate = sd;
                auction.EndDate = ed;
                this.auctionService.ContinueAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction past start date.</summary>
        [Test]
        public void StartAuctionPastStartDate()
        {
            try
            {
                DateTime sd = new DateTime(2019, 10, 23);
                var auction = this.auction;
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction past start date.</summary>
        [Test]
        public void ContinueAuctionPastStartDate()
        {
            try
            {
                DateTime sd = new DateTime(2019, 10, 23);
                var auction = this.auction;
                auction.StartDate = sd;
                this.auctionService.ContinueAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start with user with started auction max.</summary>
        [Test]
        public void StartAuctionWithUserWithStartedAuctionMax()
        {
            try
            {
                var auction = this.auction;
                auction.OfferUser.StartedAuctions = 10;
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue with user with started auction max.</summary>
        [Test]
        public void ContinueAuctionWithUserWithStartedAuctionMax()
        {
            try
            {
                var auction = this.auction;
                auction.OfferUser.StartedAuctions = 10;
                this.auctionService.ContinueAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start with user with started category auction max.</summary>
        [Test]
        public void StartAuctionWithUserWithStartedCategoryAuctionMax()
        {
            try
            {
                Dictionary<Category, int> categoryAuctions = new Dictionary<Category, int>();
                categoryAuctions.Add(this.product.Category[0], 5);
                var user = new User { Id = 1, FirstName = "Dan", LastName = "Brown", StartedSpecifiedCategoryAuctions = categoryAuctions };
                var auction = this.auction;
                auction.OfferUser = user;
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue with user with started category auction max.</summary>
        [Test]
        public void ContinueAuctionWithUserWithStartedCategoryAuctionMax()
        {
            try
            {
                Dictionary<Category, int> categoryAuctions = new Dictionary<Category, int>();
                categoryAuctions.Add(this.product.Category[0], 5);
                var user = new User { Id = 1, FirstName = "Dan", LastName = "Brown", StartedSpecifiedCategoryAuctions = categoryAuctions };
                var auction = this.auction;
                auction.OfferUser = user;
                this.auctionService.ContinueAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction with small starting price.</summary>
        [Test]
        public void StartAuctionSmallPrice()
        {
            try
            {
                this.price.ThePrice = 0;
                auction.StartingPrice = this.price;
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction with small starting price.</summary>
        [Test]
        public void ContinueAuctionSmallPrice()
        {
            try
            {
                this.price.ThePrice = 0;
                auction.StartingPrice = this.price;
                this.auctionService.ContinueAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction with wrong start date.</summary>
        [Test]
        public void StartAuctionWrongStartDate()
        {
            try
            {
                auction.StartDate = default;
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction with wrong start date.</summary>
        [Test]
        public void ContinueAuctionWrongStartDate()
        {
            try
            {
                auction.StartDate = default;
                this.auctionService.ContinueAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start closed auction false.</summary>
        [Test]
        public void StartAuctionClosedFalse()
        {
            try
            {
                auction.IsClosed = false;
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue closed auction false.</summary>
        [Test]
        public void ContinueAuctionClosedFalse()
        {
            try
            {
                auction.IsClosed = false;
                this.auctionService.ContinueAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start closed auction true.</summary>
        [Test]
        public void StartAuctionClosedTrue()
        {
            try
            {
                auction.IsClosed = true;
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue closed auction true.</summary>
        [Test]
        public void ContinueAuctionClosedTrue()
        {
            try
            {
                auction.IsClosed = true;
                this.auctionService.ContinueAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start with past now end date.</summary>
        [Test]
        public void StartAuctionWithPastEndDate()
        {
            try
            {
                var auction = this.auction;
                DateTime ed = new DateTime(2019, 11, 23);
                auction.EndDate = ed;
                this.auctionService.StartAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue with past now end date.</summary>
        [Test]
        public void ContinueAuctionWithPastEndDate()
        {
            try
            {
                var auction = this.auction;
                DateTime ed = new DateTime(2019, 11, 23);
                auction.EndDate = ed;
                this.auctionService.ContinueAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction with start date after end date.</summary>
        [Test]
        public void StartAuctionWrongStartDateAfterEndDate()
        {
            try
            {
                DateTime sd = new DateTime(2021, 11, 23);
                DateTime ed = new DateTime(2020, 11, 23);
                auction.EndDate = ed;
                auction.StartDate = sd;
                this.auctionService.StartAuction(this.auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue null auction.</summary>
        [Test]
        public void ContinueNullAuction()
        {
            try
            {
                Auction auction = null;
                this.auctionService.ContinueAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction with wrong bid null user.</summary>
        [Test]
        public void StartAuctionWithWrongBidNullUser()
        {
            try
            {
                var auction = this.auction;
                auction.Bid.User = null;
                this.auctionService.StartAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction with wrong bid null user.</summary>
        [Test]
        public void ContinueAuctionWithWrongBidNullUser()
        {
            try
            {
                var auction = this.auction;
                auction.Bid.User = null;
                this.auctionService.ContinueAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction with wrong bid null price.</summary>
        [Test]
        public void StartAuctionWithWrongBidNullPrice()
        {
            try
            {
                var auction = this.auction;
                auction.Bid.Price = null;
                this.auctionService.StartAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction with wrong bid null price.</summary>
        [Test]
        public void ContinueAuctionWithWrongBidNullPrice()
        {
            try
            {
                var auction = this.auction;
                auction.Bid.Price = null;
                this.auctionService.ContinueAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction with wrong bid wrong currency.</summary>
        [Test]
        public void StartAuctionWithWrongBidWrongCurrency()
        {
            try
            {
                var auction = this.auction;
                auction.Bid.Price.Currency = "asd";
                this.auctionService.StartAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction with wrong bid wrong currency.</summary>
        [Test]
        public void ContinueAuctionWithWrongBidWrongCurrency()
        {
            try
            {
                var auction = this.auction;
                auction.Bid.Price.Currency = "asd";
                this.auctionService.ContinueAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction with wrong bid wrong currency null.</summary>
        [Test]
        public void StartAuctionWithWrongBidWrongCurrencyNull()
        {
            try
            {
                var auction = this.auction;
                auction.Bid.Price.Currency = null;
                this.auctionService.StartAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction with wrong bid wrong currency null.</summary>
        [Test]
        public void ContinueAuctionWithWrongBidWrongCurrencyNull()
        {
            try
            {
                var auction = this.auction;
                auction.Bid.Price.Currency = null;
                this.auctionService.ContinueAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction with wrong bid wrong currency empty.</summary>
        [Test]
        public void StartAuctionWithWrongBidWrongCurrencyEmpty()
        {
            try
            {
                var auction = this.auction;
                auction.Bid.Price.Currency = string.Empty;
                this.auctionService.StartAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction with wrong bid wrong currency empty.</summary>
        [Test]
        public void ContinueAuctionWithWrongBidWrongCurrencyEmpty()
        {
            try
            {
                var auction = this.auction;
                auction.Bid.Price.Currency = string.Empty;
                this.auctionService.ContinueAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction with bid null.</summary>
        [Test]
        public void StartAuctionWithBidNull()
        {
            try
            {
                var auction = this.auction;
                auction.Bid = null;
                this.auctionService.StartAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction with wrong bid null.</summary>
        [Test]
        public void ContinueAuctionWithWrongBidNull()
        {
            try
            {
                var auction = this.auction;
                auction.Bid = null;
                this.auctionService.ContinueAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction with wrong bid exceeded price.</summary>
        [Test]
        public void StartAuctionWithWrongBidExceededPrice()
        {
            try
            {
                var auction = this.auction;
                auction.Bid.Price.ThePrice = 300;
                this.auctionService.StartAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test continue auction with wrong bid exceeded price.</summary>
        [Test]
        public void ContinueAuctionWithWrongBidExceededPrice()
        {
            try
            {
                var auction = this.auction;
                auction.Bid.Price.ThePrice = 300;
                this.auctionService.ContinueAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test start auction with wrong bid lower the starting price.</summary>
        [Test]
        public void StartAuctionWithWrongBidLowerThanStartingPice()
        {
            try
            {
                var auction = this.auction;
                auction.Bid.Price.ThePrice = 0;
                this.auctionService.StartAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test Continue auction with wrong bid lower the starting price.</summary>
        [Test]
        public void ContinueAuctionWithWrongBidLowerThanStartingPice()
        {
            try
            {
                var auction = this.auction;
                auction.Bid.Price.ThePrice = 0;
                this.auctionService.ContinueAuction(auction);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test Check auction with past end date.</summary>
        [Test]
        public void TestCheckAuctionPastEndDate()
        {
            var auction = this.auction;
            DateTime ed = new DateTime(2019, 11, 23);
            auction.EndDate = ed;
            var result = this.auctionService.CheckAuction(auction);
            Assert.False(result);
        }

        /// <summary>Test Check auction is closed.</summary>
        [Test]
        public void TestCheckAuctionIsClosed()
        {
            var auction = this.auction;
            auction.IsClosed = true;
            var result = this.auctionService.CheckAuction(auction);
            Assert.False(result);
        }

        /// <summary>Test Check auction null user.</summary>
        [Test]
        public void TestCheckAuctionNullUser()
        {
            var auction = this.auction;
            auction.OfferUser = null;
            var result = this.auctionService.CheckAuction(auction);
            Assert.False(result);
        }

        /// <summary>Test Check auction null first name user.</summary>
        [Test]
        public void TestCheckAuctionNullFirstNameUser()
        {
            var auction = this.auction;
            auction.OfferUser.FirstName = null;
            var result = this.auctionService.CheckAuction(auction);
            Assert.False(result);
        }

        /// <summary>Test Check auction empty first name user.</summary>
        [Test]
        public void TestCheckAuctionEmptyFirstNameUser()
        {
            var auction = this.auction;
            auction.OfferUser.FirstName = string.Empty;
            var result = this.auctionService.CheckAuction(auction);
            Assert.False(result);
        }

        /// <summary>Test Check auction null last name user.</summary>
        [Test]
        public void TestCheckAuctionNullLastNameUser()
        {
            var auction = this.auction;
            auction.OfferUser.LastName = null;
            var result = this.auctionService.CheckAuction(auction);
            Assert.False(result);
        }

        /// <summary>Test Check auction empty first name user.</summary>
        [Test]
        public void TestCheckAuctionEmptyLastNameUser()
        {
            var auction = this.auction;
            auction.OfferUser.LastName = string.Empty;
            var result = this.auctionService.CheckAuction(auction);
            Assert.False(result);
        }

        /// <summary>Test Check auction null email user.</summary>
        [Test]
        public void TestCheckAuctionNullEmailUser()
        {
            var auction = this.auction;
            auction.OfferUser.Email = null;
            var result = this.auctionService.CheckAuction(auction);
            Assert.True(result);
        }

        /// <summary>Test Check auction empty email user.</summary>
        [Test]
        public void TestCheckAuctionEmptyEmailUser()
        {
            var auction = this.auction;
            auction.OfferUser.Email = string.Empty;
            var result = this.auctionService.CheckAuction(auction);
            Assert.True(result);
        }

        /// <summary>Test Check auction null address user.</summary>
        [Test]
        public void TestCheckAuctionNullAddressUser()
        {
            var auction = this.auction;
            auction.OfferUser.Address = null;
            var result = this.auctionService.CheckAuction(auction);
            Assert.True(result);
        }

        /// <summary>Test Check auction empty address user.</summary>
        [Test]
        public void TestCheckAuctionEmptyAddressUser()
        {
            var auction = this.auction;
            auction.OfferUser.Address = string.Empty;
            var result = this.auctionService.CheckAuction(auction);
            Assert.True(result);
        }

        /// <summary>Test Check auction null phone number user.</summary>
        [Test]
        public void TestCheckAuctionNullPhoneNumberUser()
        {
            var auction = this.auction;
            auction.OfferUser.PhoneNumber = null;
            var result = this.auctionService.CheckAuction(auction);
            Assert.True(result);
        }

        /// <summary>Test Check auction empty phone number user.</summary>
        [Test]
        public void TestCheckAuctionEmptyPhoneNumberUser()
        {
            var auction = this.auction;
            auction.OfferUser.PhoneNumber = string.Empty;
            var result = this.auctionService.CheckAuction(auction);
            Assert.True(result);
        }

        /// <summary>Test Check auction null product name.</summary>
        [Test]
        public void TestCheckAuctionNullProductName()
        {
            var auction = this.auction;
            auction.Product.Name = null;
            var result = this.auctionService.CheckAuction(auction);
            Assert.True(result);
        }

        /// <summary>Test Check auction empty product name.</summary>
        [Test]
        public void TestCheckAuctionEmptyProductName()
        {
            var auction = this.auction;
            auction.Product.Name = string.Empty;
            var result = this.auctionService.CheckAuction(auction);
            Assert.True(result);
        }

        /// <summary>Test Check auction null product categories.</summary>
        [Test]
        public void TestCheckAuctionNullProductCategories()
        {
            var auction = this.auction;
            auction.Product.Category = null;
            var result = this.auctionService.CheckAuction(auction);
            Assert.True(result);
        }

        /// <summary>Test Check auction empty product categories.</summary>
        [Test]
        public void TestCheckAuctionEmptyProductCategtories()
        {
            var auction = this.auction;
            IList<Category> categories = new List<Category>();
            auction.Product.Category = categories;
            var result = this.auctionService.CheckAuction(auction);
            Assert.True(result);
        }

        /// <summary>Test Check auction with null.</summary>
        [Test]
        public void TestCheckAuctionNull()
        {
            var result = this.auctionService.CheckAuction(null);
            Assert.False(result);
        }

        /// <summary>Test Is bid correct null params.</summary>
        [Test]
        public void TestIsBidcorrectNullParams()
        {
            var result = this.auctionService.IsBidCorrect(null, null);
            Assert.False(result);
        }

        /// <summary>Test Is bid correct null user auction.</summary>
        [Test]
        public void TestIsBidcorrectNullAuctionUser()
        {
            this.auction.OfferUser = null;
            var result = this.auctionService.IsBidCorrect(this.auction, this.price);
            Assert.False(result);
        }

        /// <summary>Test Is bid correct null product auction.</summary>
        [Test]
        public void TestIsBidcorrectNullAuctionProduct()
        {
            this.auction.Product = null;
            var result = this.auctionService.IsBidCorrect(this.auction, this.price);
            Assert.False(result);
        }

        /// <summary>Test Is bid correct null starting price auction.</summary>
        [Test]
        public void TestIsBidcorrectNullAuctionStartingPrice()
        {
            this.auction.StartingPrice = null;
            var result = this.auctionService.IsBidCorrect(this.auction, this.price);
            Assert.False(result);
        }

        /// <summary>Test Is bid correct null name auction.</summary>
        [Test]
        public void TestIsBidcorrectNullAuctionName()
        {
            this.auction.Name = null;
            var result = this.auctionService.IsBidCorrect(this.auction, this.price);
            Assert.False(result);
        }

        /// <summary>Test Is bid correct empty name auction.</summary>
        [Test]
        public void TestIsBidcorrectEmptyAuctionName()
        {
            this.auction.Name = string.Empty;
            var result = this.auctionService.IsBidCorrect(this.auction, this.price);
            Assert.False(result);
        }

        /// <summary>Test Is bid correct null auction.</summary>
        [Test]
        public void TestIsBidcorrectNullAuction()
        {
            var result = this.auctionService.IsBidCorrect(null, this.price);
            Assert.False(result);
        }

        /// <summary>Test Is bid correct null price.</summary>
        [Test]
        public void TestIsBidcorrectNullPrice()
        {
            var result = this.auctionService.IsBidCorrect(this.auction, null);
            Assert.False(result);
        }

        /// <summary>Test Is bid correct small price.</summary>
        [Test]
        public void TestIsBidcorrectSmallPrice()
        {
            this.price.ThePrice = 0;
            var result = this.auctionService.IsBidCorrect(this.auction, this.price);
            Assert.False(result);
        }

        /// <summary>Test Is bid correct big price.</summary>
        [Test]
        public void TestIsBidcorrectBigPrice()
        {
            this.price.ThePrice = 300;
            var result = this.auctionService.IsBidCorrect(this.auction, this.price);
            Assert.False(result);
        }

        /// <summary>Test Is bid correct.</summary>
        [Test]
        public void TestIsBidcorrect()
        {
            this.price.ThePrice = 160;
            var auction = this.auction;
            var price = new Price { Currency = "USD", ThePrice = 155 };
            auction.Bid = new Bid { Price = price, User = this.user };
            var result = this.auctionService.IsBidCorrect(auction, this.price);
            Assert.True(result);
        }

        /// <summary>Test Is bid correct small price.</summary>
        [Test]
        public void TestIsBidcorrectSmallPriceBidNull()
        {
            this.price.ThePrice = 0;
            this.auction.Bid = null;
            var result = this.auctionService.IsBidCorrect(this.auction, this.price);
            Assert.False(result);
        }

        /// <summary>Test Is bid correct big price.</summary>
        [Test]
        public void TestIsBidcorrectBigPriceBidNull()
        {
            var price = new Price { ThePrice = 11000, Currency = "USD" };
            this.auction.Bid = null;
            this.auction.StartingPrice.ThePrice = 300;
            var result = this.auctionService.IsBidCorrect(this.auction, price);
            Assert.False(result);
        }

        /// <summary>Test Is bid correct wrong currency starting price.</summary>
        [Test]
        public void TestIsBidcorrectBigPriceWrongCurrency()
        {
            var price = new Price { ThePrice = 310, Currency = "USD" };
            this.auction.Bid = null;
            this.auction.StartingPrice.ThePrice = 300;
            this.auction.StartingPrice.Currency = "asd";
            var result = this.auctionService.IsBidCorrect(this.auction, price);
            Assert.False(result);
        }

        /// <summary>Test Is bid correct wrong currency.</summary>
        [Test]
        public void TestIsBidcorrectWrongCurrency()
        {
            this.price.Currency = "asd";
            var result = this.auctionService.IsBidCorrect(this.auction, this.price);
            Assert.False(result);
        }

        /// <summary>Test Is bid correct null currency.</summary>
        [Test]
        public void TestIsBidcorrectNullCurrency()
        {
            this.price.Currency = null;
            var result = this.auctionService.IsBidCorrect(this.auction, this.price);
            Assert.False(result);
        }

        /// <summary>Test Is bid correct empty currency.</summary>
        [Test]
        public void TestIsBidcorrectEmptyCurrency()
        {
            this.price.Currency = string.Empty;
            var result = this.auctionService.IsBidCorrect(this.auction, this.price);
            Assert.False(result);
        }
    }
}