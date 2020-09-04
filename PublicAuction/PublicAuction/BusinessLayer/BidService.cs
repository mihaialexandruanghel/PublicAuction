// <copyright file="BidService.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the BidService class.</summary>
namespace PublicAuction.BusinessLayer
{
    using System.Configuration;
    using Castle.Core.Internal;
    using PublicAuction.Data_Mapper;
    using PublicAuction.Domain_Model;

    /// <summary>Bid service class using price repository.</summary>
    public class BidService
    {
        /// <summary>The bid repository.</summary>
        private readonly BidRepository bidRepository;

        /// <summary>Initializes a new instance of the <see cref="BidService"/> class.</summary>
        /// <param name="bidRepository">The review repository.</param>
        public BidService(BidRepository bidRepository)
        {
            this.bidRepository = bidRepository;
        }

        /// <summary>Adds the price.</summary>
        /// <param name="bid">The auction bid.</param>
        /// <returns>Returns a boolean value if bid added successfully.</returns>
        public bool AddBid(Bid bid)
        {
            if (bid == null)
            {
                LoggerUtil.LogInfo("Cannot create a null bid!");
                return false;
            }

            if (bid.User == null)
            {
                LoggerUtil.LogInfo("Cannot create a null bid user!");
                return false;
            }

            if (bid.Price == null)
            {
                LoggerUtil.LogInfo("Cannot create a null bid price!");
                return false;
            }

            return this.bidRepository.AddBid(bid);
        }
    }
}
