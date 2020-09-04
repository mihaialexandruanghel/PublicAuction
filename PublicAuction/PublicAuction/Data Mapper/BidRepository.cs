// <copyright file="BidRepository.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the BidRepository class.</summary>
namespace PublicAuction.Data_Mapper
{
    using PublicAuction.Domain_Model;

    /// <summary>Class used for communicating directly to database, performing operations on bids.</summary>
    public class BidRepository
    {
        /// <summary>The auction database.</summary>
        private readonly AuctionDb _auctionDb;

        /// <summary>Initializes a new instance of the <see cref="BidRepository"/> class.</summary>
        /// <param name="auctionDb">The auction database.</param>
        public BidRepository(AuctionDb auctionDb)
        {
            this._auctionDb = auctionDb;
        }

        /// <summary>Adds the bid.</summary>
        /// <param name="bid">The auction bid.</param>
        /// <returns>True if price added successfully.</returns>
        public bool AddBid(Bid bid)
        {
            this._auctionDb.Bids.Add(bid);
            return this._auctionDb.SaveChanges() != 0;
        }
    }
}
