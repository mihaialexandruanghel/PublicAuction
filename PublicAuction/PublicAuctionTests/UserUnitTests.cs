// <copyright file="UserUnitTests.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the UserUnitTests test class.</summary>
namespace PublicAuctionTests
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using PublicAuction.BusinessLayer;
    using PublicAuction.Data_Mapper;
    using PublicAuction.Domain_Model;
    using Telerik.JustMock.EntityFramework;

    /// <summary>User test class.</summary>
    [TestFixture]
    public class UserUnitTests
    {
        /// <summary>The library database mock.</summary>
        private AuctionDb auctionDbMock;

        /// <summary>The User service.</summary>
        private UserService userService;

        /// <summary>The review service.</summary>
        private ReviewService reviewService;

        /// <summary>The User. </summary>
        private User user;

        /// <summary>Sets up.</summary>
        [SetUp]
        public void SetUp()
        {
            this.auctionDbMock = EntityFrameworkMock.Create<AuctionDb>();
            this.reviewService = new ReviewService(new ReviewRepository(this.auctionDbMock));
            this.userService = new UserService(new UserRepository(this.auctionDbMock), new ReviewRepository(this.auctionDbMock));
            this.user = new User();
            this.user.FirstName = "Mihai";
            this.user.LastName = "Dan";
            this.user.Gender = "M";
            this.user.Email = "mihai.anghel@gmail.com";
            this.user.Address = "brasov";
            this.user.PhoneNumber = "1234567891";
            this.user.Id = 1;
            var fromUser = new User { Id = 1, FirstName = "Dan", LastName = "Brown" };
            var review = new Review { FromUser = fromUser, ToUser = this.user, Score = 5.0 };
            var result = this.reviewService.AddReview(review);
        }

        /// <summary>Tests the add user.</summary>
        [Test]
        public void TestAddUser()
        {
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 1);
        }

        /// <summary>Tests the add null user.</summary>
        [Test]
        public void TestAddNullUser()
        {
            var result = this.userService.AddUser(null);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add null first name user.</summary>
        [Test]
        public void TestAddNullFirstNameUser()
        {
            this.user.FirstName = null;
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add empty first name user.</summary>
        [Test]
        public void TestAddEmptyFirstNameUser()
        {
            this.user.FirstName = string.Empty;
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add smaller first name user.</summary>
        [Test]
        public void TestAddSmallerFirstNameUser()
        {
            this.user.FirstName = "a";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add longer first name user.</summary>
        [Test]
        public void TestAddLongerFirstNameUser()
        {
            this.user.FirstName = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add lower first name user.</summary>
        [Test]
        public void TestAddLowerFirstNameUser()
        {
            this.user.FirstName = "mihai";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add digit first name user.</summary>
        [Test]
        public void TestAddDigitFirstNameUser()
        {
            this.user.FirstName = "Mihai3";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add symbol first name user.</summary>
        [Test]
        public void TestAddSymbolFirstNameUser()
        {
            this.user.FirstName = "Mihai@#$";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add white space first name user.</summary>
        [Test]
        public void TestAddWhiteSpaceFirstNameUser()
        {
            this.user.FirstName = "Mihai alex";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 1);
        }

        /// <summary>Tests the add dash first name user.</summary>
        [Test]
        public void TestAddDashFirstNameUser()
        {
            this.user.FirstName = "Mihai-alex";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 1);
        }

        /// <summary>Tests the add null last name user.</summary>
        [Test]
        public void TestAddNullLastNameUser()
        {
            this.user.LastName = null;
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add empty last name user.</summary>
        [Test]
        public void TestAddEmptyLastNameUser()
        {
            this.user.LastName = string.Empty;
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add smaller last name user.</summary>
        [Test]
        public void TestAddSmallerLastNameUser()
        {
            this.user.LastName = "s";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add longer last name user.</summary>
        [Test]
        public void TestAddLongerLastNameUser()
        {
            this.user.LastName = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add digit last name user.</summary>
        [Test]
        public void TestAddDigitLastNameUser()
        {
            this.user.LastName = "Mihai4";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add symbol last name user.</summary>
        [Test]
        public void TestAddSymbolLastNameUser()
        {
            this.user.LastName = "Mihai$%^&*";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add lowercase last name user.</summary>
        [Test]
        public void TestAddLowercaseLastNameUser()
        {
            this.user.LastName = "brown";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add white space last name user.</summary>
        [Test]
        public void TestAddWhiteSpaceLastNameUser()
        {
            this.user.LastName = "Andrei Ion";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 1);
        }

        /// <summary>Tests the add dash last name user.</summary>
        [Test]
        public void TestAddDashLastNameUser()
        {
            this.user.LastName = "Andrei-Ion";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 1);
        }

        /// <summary>Tests the add null email user.</summary>
        [Test]
        public void TestAddNullEmailUser()
        {
            this.user.Email = null;
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add empty email user.</summary>
        [Test]
        public void TestAddEmptyEmailUser()
        {
            this.user.Email = string.Empty;
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add smaller email user.</summary>
        [Test]
        public void TestAddSmallerEmailUser()
        {
            this.user.Email = "a@";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add longer email user.</summary>
        [Test]
        public void TestAddLongerEmailUser()
        {
            this.user.Email = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add lower email user.</summary>
        [Test]
        public void TestAddUpperEmailUser()
        {
            this.user.Email = "Mihai.anghel@gmail.com";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add digit email user.</summary>
        [Test]
        public void TestAddDigitEmailUser()
        {
            this.user.Email = "mihai3@gmail.com";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 1);
        }

        /// <summary>Tests the add symbol email user.</summary>
        [Test]
        public void TestAddSymbolEmailUser()
        {
            this.user.Email = "mihai@gmail.com";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 1);
        }

        /// <summary>Tests the add white space email user.</summary>
        [Test]
        public void TestAddWhiteSpaceEmailUser()
        {
            this.user.Email = "mihai alex@gmail.com";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 1);
        }

        /// <summary>Tests the add dash email user.</summary>
        [Test]
        public void TestAddDashEmailUser()
        {
            this.user.Email = "mihai-alex@gmail.com";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 1);
        }

        /// <summary>Tests the add null phone number user.</summary>
        [Test]
        public void TestAddNullPhoneNumberUser()
        {
            this.user.PhoneNumber = null;
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add empty phone number user.</summary>
        [Test]
        public void TestAddEmptyPhoneNumberUser()
        {
            this.user.PhoneNumber = string.Empty;
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add smaller phone number user.</summary>
        [Test]
        public void TestAddSmallerPhoneNumberUser()
        {
            this.user.PhoneNumber = "1";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add longer phone number user.</summary>
        [Test]
        public void TestAddLongerPhoneNumberUser()
        {
            this.user.PhoneNumber = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add digit phone number user.</summary>
        [Test]
        public void TestAddLettersPhoneNumberUser()
        {
            this.user.PhoneNumber = "12243a";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add symbol phone number user.</summary>
        [Test]
        public void TestAddSymbolPhoneNumberUser()
        {
            this.user.PhoneNumber = "1231$%^&*";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add white space phone number user.</summary>
        [Test]
        public void TestAddWhiteSpacePhoneNumberUser()
        {
            this.user.PhoneNumber = "123 3123 12312";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 1);
        }

        /// <summary>Tests the add dash phone number user.</summary>
        [Test]
        public void TestAddDashPhoneNumberUser()
        {
            this.user.PhoneNumber = "123-123";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 1);
        }

        /// <summary>Tests the add null address user.</summary>
        [Test]
        public void TestAddNullAddressUser()
        {
            this.user.Address = null;
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add empty address user.</summary>
        [Test]
        public void TestAddEmptyAddressUser()
        {
            this.user.Address = string.Empty;
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add smaller address user.</summary>
        [Test]
        public void TestAddSmallerAddressUser()
        {
            this.user.Address = "s";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add longer address user.</summary>
        [Test]
        public void TestAddLongerAddressUser()
        {
            this.user.Address = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add digit address user.</summary>
        [Test]
        public void TestAddDigitAddressUser()
        {
            this.user.Address = "brasov4";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 1);
        }

        /// <summary>Tests the add symbol address user.</summary>
        [Test]
        public void TestAddSymbolAddressUser()
        {
            this.user.Address = "brasov$%^&*";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add lowercase address user.</summary>
        [Test]
        public void TestAddUppercaseAddressUser()
        {
            this.user.Address = "Brasov";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add white space address user.</summary>
        [Test]
        public void TestAddWhiteSpaceAddressUser()
        {
            this.user.Address = "brasov brasov";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 1);
        }

        /// <summary>Tests the add dash address user.</summary>
        [Test]
        public void TestAddDashAddressUser()
        {
            this.user.Address = "brasov-brasov";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 1);
        }

        /// <summary>Tests the add null gender User.</summary>
        [Test]
        public void TestAddNullGenderUser()
        {
            this.user.Gender = null;
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add empty gender User.</summary>
        [Test]
        public void TestAddEmptyGenderUser()
        {
            this.user.Gender = string.Empty;
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add m gender User.</summary>
        [Test]
        public void TestAddMGenderUser()
        {
            this.user.Gender = "M";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 1);
        }

        /// <summary>Tests the add f gender User.</summary>
        [Test]
        public void TestAddFGenderUser()
        {
            this.user.Gender = "F";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 1);
        }

        /// <summary>Tests the add bad gender User.</summary>
        [Test]
        public void TestAddBadGenderUser()
        {
            this.user.Gender = "G";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add number gender User.</summary>
        [Test]
        public void TestAddNumberGenderUser()
        {
            this.user.Gender = "2";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests the add bad gender User.</summary>
        [Test]
        public void TestAddBigGenderUser()
        {
            this.user.Gender = "Gasd";
            var result = this.userService.AddUser(this.user);
            Assert.True(this.auctionDbMock.Users.Count() == 0);
        }

        /// <summary>Tests to get user.</summary>
        [Test]
        public void TestGetUser()
        {
            var user = this.user;
            var result = this.userService.AddUser(user);
            user = this.userService.GetUser(user.Id);
            Assert.NotNull(user);
        }

        /// <summary>Tests to get negative id value for user.</summary>
        [Test]
        public void TestGetUserWithNegativeIdValue()
        {
            var user = this.userService.GetUser(-1);
            Assert.Null(user);
        }

        /// <summary>Tests to get zero id value user.</summary>
        [Test]
        public void TestGetUserZeroValueId()
        {
            var user = this.userService.GetUser(0);
            Assert.Null(user);
        }

        /// <summary>Test calculate score with null user.</summary>
        [Test]
        public void TestCalculateScoreNullUser()
        {
            try
            {
                this.userService.CalculateScore(null);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>Test calculate score.</summary>
        [Test]
        public void TestCalculateScore()
        {
            try
            {
                this.userService.CalculateScore(this.user);
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}