// <copyright file="ReviewUnitTests.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the ReviewUnitTests test class.</summary>
namespace PublicAuctionTests
{
    using System.Linq;
    using NUnit.Framework;
    using PublicAuction.BusinessLayer;
    using PublicAuction.Data_Mapper;
    using PublicAuction.Domain_Model;
    using Telerik.JustMock.EntityFramework;

    /// <summary>Employee test class.</summary>
    [TestFixture]
    public class ReviewUnitTests
    {
        /// <summary>The library database mock.</summary>
        private AuctionDb auctionDbMock;

        /// <summary>The review service.</summary>
        private ReviewService reviewService;

        /// <summary>The from user.</summary>
        private User fromUser;

        /// <summary>The to user.</summary>
        private User toUser;

        /// <summary>Sets up.</summary>
        [SetUp]
        public void SetUp()
        {
            this.auctionDbMock = EntityFrameworkMock.Create<AuctionDb>();
            EntityFrameworkMock.PrepareMock(this.auctionDbMock);
            this.reviewService = new ReviewService(new ReviewRepository(this.auctionDbMock));
            this.fromUser = new User { Id = 1, FirstName = "Dan", LastName = "Brown" };
            this.toUser = new User { Id = 2, FirstName = "Mihai", LastName = "Anghel" };
        }

        /// <summary>Tests the add null review.</summary>
        [Test]
        public void TestAddNullReview()
        {
            var result = this.reviewService.AddReview(null);
            Assert.True(this.auctionDbMock.Reviews.Count() == 0);
        }

        /// <summary>Tests the add null review.</summary>
        [Test]
        public void TestAddReview()
        {
            var review = new Review { FromUser = this.fromUser, ToUser = this.toUser, Score = 5.0 };
            var result = this.reviewService.AddReview(review);
            Assert.True(this.auctionDbMock.Reviews.Count() == 1);
        }

        /// <summary>Tests the add null fromUser review.</summary>
        [Test]
        public void TestAddNullFromUserReview()
        {
            var review = new Review { FromUser = null, ToUser = this.toUser, Score = 5.0 };
            var result = this.reviewService.AddReview(review);
            Assert.True(this.auctionDbMock.Reviews.Count() == 0);
        }

        /// <summary>Tests the add null toUser review.</summary>
        [Test]
        public void TestAddNullToUserReview()
        {
            var review = new Review { FromUser = this.fromUser, ToUser = null, Score = 5.0 };
            var result = this.reviewService.AddReview(review);
            Assert.True(this.auctionDbMock.Reviews.Count() == 0);
        }

        /// <summary>Tests the add negative score review.</summary>
        [Test]
        public void TestAddNegativeScoreReview()
        {
            var review = new Review { FromUser = this.fromUser, ToUser = this.toUser, Score = -5.0 };
            var result = this.reviewService.AddReview(review);
            Assert.True(this.auctionDbMock.Reviews.Count() == 0);
        }

        /// <summary>Tests the add over max score review.</summary>
        [Test]
        public void TestAddOverMaxScoreReview()
        {
            var review = new Review { FromUser = this.fromUser, ToUser = this.toUser, Score = 15.0 };
            var result = this.reviewService.AddReview(review);
            Assert.True(this.auctionDbMock.Reviews.Count() == 0);
        }

        /// <summary>Tests the get reviews null user.</summary>
        [Test]
        public void TestGetReviewsNullUser()
        {
            var reviews = this.reviewService.GetAllReviews(null);
            Assert.True(reviews.Count() == 0);
        }

        /// <summary>Tests the get user reviews.</summary>
        [Test]
        public void TestGetUserReviews()
        {
            var reviews = this.reviewService.GetAllReviews(this.toUser);
            Assert.NotNull(reviews);
        }

        /// <summary>Tests to check if review exists.</summary>
        [Test]
        public void TestCheckIfReviewExists()
        {
            var review = new Review { FromUser = this.fromUser, ToUser = this.toUser, Score = 5.0 };
            var result = this.reviewService.AddReview(review);
            var checkReview = this.reviewService.CheckIfExists(this.fromUser, this.toUser);
            Assert.True(checkReview);
        }

        /// <summary>Tests to check if review does not exists.</summary>
        [Test]
        public void TestCheckIfReviewDoesNotExists()
        {
            var checkReview = this.reviewService.CheckIfExists(this.fromUser, this.toUser);
            Assert.False(checkReview);
        }

        /// <summary>Tests to check if review exists with null from user.</summary>
        [Test]
        public void TestCheckIfExistsWithNullFromUser()
        {
            var review = new Review { FromUser = this.fromUser, ToUser = this.toUser, Score = 5.0 };
            var result = this.reviewService.AddReview(review);
            var checkReview = this.reviewService.CheckIfExists(null, this.toUser);
            Assert.False(checkReview);
        }

        /// <summary>Tests to check if review exists with null to user.</summary>
        [Test]
        public void TestCheckIfExistsWithNullToUser()
        {
            var review = new Review { FromUser = this.fromUser, ToUser = this.toUser, Score = 5.0 };
            var result = this.reviewService.AddReview(review);
            var checkReview = this.reviewService.CheckIfExists(this.fromUser, null);
            Assert.False(checkReview);
        }
    }
}