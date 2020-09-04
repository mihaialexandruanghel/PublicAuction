// <copyright file="AuctionService.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the AuctionService class.</summary>
namespace PublicAuction.BusinessLayer
{
    using System;
    using System.Configuration;
    using System.Linq;
    using Castle.Core.Internal;
    using log4net;
    using PublicAuction.Data_Mapper;
    using PublicAuction.Domain_Model;

    /// <summary>Auction service class using auction repository.</summary>
    public class AuctionService
    {
        /// <summary>The auction repository.</summary>
        private readonly AuctionRepository auctionRepository;

        /// <summary>The user service.</summary>
        private readonly UserService userService;

        /// <summary>The review service.</summary>
        private readonly ReviewService reviewService;

        /// <summary>Initializes a new instance of the <see cref="AuctionService"/> class.</summary>
        /// <param name="auctionRepository">The auction repository.</param>
        public AuctionService(AuctionRepository auctionRepository)
        {
            this.auctionRepository = auctionRepository;
        }

        /// <summary>Adds the auction.</summary>
        /// <param name="auction">The auction.</param>
        /// <returns>Returns a boolean value if auction added successfully.</returns>
        public bool AddAuction(Auction auction)
        {
            if (auction == null)
            {
                LoggerUtil.LogInfo("Cannot create a null auction!");
                return false;
            }

            if (auction.Name.IsNullOrEmpty())
            {
                LoggerUtil.LogInfo("Name can not be null or empty!");
                return false;
            }

            if ((auction.Name.Length < 3) || (auction.Name.Length > 100))
            {
                LoggerUtil.LogInfo("Name can not be under 3 characters!");
                return false;
            }

            if (!auction.Name.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                LoggerUtil.LogInfo("Cannot set a Name which has no characters!");
                return false;
            }

            if (auction.StartDate == default)
            {
                LoggerUtil.LogInfo("The Start Date of an auction can not be default!");
                return false;
            }

            DateTime timeOfTheDay = DateTime.Now;
            if (auction.StartDate < timeOfTheDay)
            {
                LoggerUtil.LogInfo("The Start Date of an auction can not be in the past!");
                return false;
            }

            if (auction.EndDate == default)
            {
                LoggerUtil.LogInfo("The End Date of an auction can not be default!");
                return false;
            }

            if (auction.StartDate > auction.EndDate)
            {
                LoggerUtil.LogInfo("The Start Date can not be after the end date!");
                return false;
            }

            var max_months_period = int.Parse(ConfigurationManager.AppSettings["MAX_MONTHS_PERIOD"]);
            DateTime months = default;
            months.AddMonths(max_months_period);
            if (auction.EndDate.Month - auction.StartDate.Month > months.Month)
            {
                LoggerUtil.LogInfo("The differene between Start Date and End Date can not be greater than " + max_months_period + " months");
                return false;
            }

            if (auction.StartingPrice == null)
            {
                LoggerUtil.LogInfo("The Price of an auction can not be null!");
                return false;
            }

            if (auction.OfferUser == null)
            {
                LoggerUtil.LogInfo("The OfferUser of an auction can not be null!");
                return false;
            }

            if (auction.Product == null)
            {
                LoggerUtil.LogInfo("The Product of an auction can not be null!");
                return false;
            }

            var max_started_auctions = int.Parse(ConfigurationManager.AppSettings["MAX_STARTED_AUCTIONS"]);
            if (auction.OfferUser.StartedAuctions > max_started_auctions)
            {
                LoggerUtil.LogInfo("You can not start more than " + max_started_auctions + " auctions");
                return false;
            }

            var max_startedspecified_category_auctions = int.Parse(ConfigurationManager.AppSettings["MAX_STARTED_SPECIFIED_CATEGORY_AUCTIONS"]);
            foreach (Category category in auction.Product.Category)
            {
                if (auction.OfferUser.StartedSpecifiedCategoryAuctions[category] > max_startedspecified_category_auctions)
                {
                    LoggerUtil.LogInfo("You can not start more than " + max_startedspecified_category_auctions + " auctions in one category");
                    return false;
                }
            }

            return this.auctionRepository.AddAuction(auction);
        }

        /// <summary>Gets the auction.</summary>
        /// <param name="id">The id to get the auction.</param>
        /// <returns>An Auction from database.</returns>
        public Auction GetAuction(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return this.auctionRepository.GetAuction(id);
        }

        /// <summary>Starts the auction process.</summary>
        /// <param name="auction">The auction.</param>
        public void StartAuction(Auction auction)
        {
            if (auction != null)
            {
                if (!this.CheckAuction(auction))
                {
                    LoggerUtil.LogError("A closed auction can not be reopened ");
                }
                else
                {
                    if (auction.StartingPrice == null)
                    {
                        LoggerUtil.LogError("A auction starting price can not be null ");
                    }
                    else
                    {
                        auction.OfferUser.StartedAuctions++;
                        LoggerUtil.LogInfo("Auction for " + auction.Product.Name + " has begun with the starting price of " + auction.StartingPrice + auction.StartingPrice.Currency);
                        //// this.AuctionProcess(auction);
                    }
                }
            }
            else
            {
                LoggerUtil.LogError("Can not start a null auction ");
            }
        }

        /// <summary>Continues the auction process.</summary>
        /// <param name="auction">The auction.</param>
        public void ContinueAuction(Auction auction)
        {
            if (auction != null)
            {
                if (!this.CheckAuction(auction))
                {
                    LoggerUtil.LogError("A closed auction can not be reopened ");
                }
                else
                {
                    if (auction.Bid == null)
                    {
                        LoggerUtil.LogInfo("Auction for " + auction.Product.Name + " continues with the price of " + auction.StartingPrice + auction.StartingPrice.Currency);
                    }
                    else
                    {
                        if (auction.Bid.Price == null)
                        {
                            LoggerUtil.LogError("A bid Price can not be null ");
                        }
                        else
                        {
                            LoggerUtil.LogInfo("Auction for " + auction.Product.Name + " continues with the price of " + auction.Bid.Price.ThePrice + auction.Bid.Price.Currency);
                        }
                    }

                    //// this.AuctionProcess(auction);
                }
            }
            else
            {
                LoggerUtil.LogError("Can not continue a null auction ");
            }
        }

        /// <summary>Check if the auction is not closed.</summary>
        /// <param name="auction">The auction.</param>
        /// <returns>Check auction.</returns>
        public bool CheckAuction(Auction auction)
        {
            if (auction != null
                && auction.OfferUser != null
                && auction.OfferUser.FirstName != null
                && auction.OfferUser.FirstName != string.Empty
                && auction.OfferUser.LastName != null
                && auction.OfferUser.LastName != string.Empty)
            {
                if (auction.EndDate > DateTime.Now)
                {
                    if (auction.IsClosed == false)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>Check if the bid is correct.</summary>
        /// <param name="auction">The auction.</param>
        /// <param name="price">The price.</param>
        /// <returns>Return if the bid is correct.</returns>
        public bool IsBidCorrect(Auction auction, Price price)
        {
            if (auction != null && price != null)
            {
                if (auction.Bid == null)
                {
                    if (price.ThePrice > (auction.StartingPrice.ThePrice * 10 / 100) + auction.StartingPrice.ThePrice)
                    {
                        LoggerUtil.LogError("Your bid can be at most 10% from the starting price");
                        return false;
                    }

                    if (price.ThePrice <= auction.StartingPrice.ThePrice)
                    {
                        LoggerUtil.LogError("Your bid can not be lower than starting price");
                        return false;
                    }
                }
                else
                {
                    if (price.ThePrice < auction.Bid.Price.ThePrice)
                    {
                        LoggerUtil.LogError("Your bid can not be lower than last bid");
                        return false;
                    }

                    double v = (auction.Bid.Price.ThePrice * 10) / 100;
                    if (price.ThePrice > v + auction.Bid.Price.ThePrice)
                    {
                        LoggerUtil.LogError("Your bid can be at most 10% from the current price");
                        return false;
                    }
                }

                if (auction.StartingPrice.Currency != price.Currency)
                {
                    LoggerUtil.LogError("Your price currency does not match the auction currency");
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary>The auction process.</summary>
        /// <param name="auction">The auction.</param>
        public void AuctionProcess(Auction auction, int opt)
        {
            bool exit = false;
            while (auction.EndDate > DateTime.Now && auction.IsClosed == false && exit == false)
            {
                LoggerUtil.LogInfo("Choose your option: ");
                LoggerUtil.LogInfo("1. Bid.");
                LoggerUtil.LogInfo("2. Check the last Bid.");
                LoggerUtil.LogInfo("3. Close auction.");
                LoggerUtil.LogInfo("4. Review the offer user with a score.");
                LoggerUtil.LogInfo("5. Exit auction.");
                //// int option = int.Parse(Console.ReadLine());
                switch (opt)
                {
                    case 1:
                        LoggerUtil.LogInfo("Please specify your userId: ");
                        int id = int.Parse(Console.ReadLine());
                        User user = this.userService.GetUser(id);
                        LoggerUtil.LogInfo("Welcome, " + user.FirstName + "! Please place your bid.");
                        Price price = new Price();
                        LoggerUtil.LogInfo("Price: ");
                        price.ThePrice = double.Parse(Console.ReadLine());
                        LoggerUtil.LogInfo("Currency: ");
                        price.Currency = Console.ReadLine();
                        if (this.IsBidCorrect(auction, price))
                        {
                            Bid bid = new Bid
                            {
                                User = user,
                                Price = price,
                            };
                            auction.Bid = bid;
                        }

                        break;
                    case 2:
                        LoggerUtil.LogInfo("The last Bid was: ");
                        if (auction.Bid == null)
                        {
                            LoggerUtil.LogInfo("Price: " + auction.StartingPrice.ThePrice + " Currency: " + auction.StartingPrice.Currency);
                        }
                        else
                        {
                            LoggerUtil.LogInfo("Price: " + auction.Bid.Price.ThePrice + " Currency: " + auction.Bid.Price.Currency);
                        }

                        exit = true;

                        break;
                    case 3:
                        LoggerUtil.LogInfo("Please specify your userId: ");
                        int idUser = int.Parse(Console.ReadLine());
                        if (auction.OfferUser.Id == idUser)
                        {
                            auction.IsClosed = true;
                            auction.OfferUser.StartedAuctions--;
                            LoggerUtil.LogInfo("Auction closed!");
                        }
                        else
                        {
                            LoggerUtil.LogError("Only the user who start the auction can close it!");
                        }

                        break;
                    case 4:
                        LoggerUtil.LogInfo("Please specify your userId: ");
                        int idFromUser = int.Parse(Console.ReadLine());
                        User fromUser = this.userService.GetUser(idFromUser);
                        if (this.reviewService.CheckIfExists(fromUser, auction.OfferUser))
                        {
                            LoggerUtil.LogError("You can not add another review for this user!");
                        }
                        else
                        {
                            Review review = new Review();
                            LoggerUtil.LogInfo("Please specify your score: ");
                            double score = double.Parse(Console.ReadLine());
                            review.FromUser = fromUser;
                            review.ToUser = auction.OfferUser;
                            review.Score = score;
                            if (this.reviewService.AddReview(review))
                            {
                                LoggerUtil.LogInfo("The review was successfully added!");
                            }
                            else
                            {
                                LoggerUtil.LogError("The review was not added!");
                            }
                        }

                        break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        LoggerUtil.LogInfo("Please choose a valid option: ");
                        break;
                }
            }

            if (auction.EndDate > DateTime.Now)
            {
                LoggerUtil.LogInfo("The auction is closing because the time expired!");
                if (auction.Bid == null)
                {
                    LoggerUtil.LogInfo("There was no bidder.");
                }
                else
                {
                    LoggerUtil.LogInfo("The product goes to: " + auction.Bid.User.FirstName + " " + auction.Bid.User.LastName);
                }

                auction.IsClosed = true;
                auction.OfferUser.StartedAuctions--;
            }
        }
    }
}
