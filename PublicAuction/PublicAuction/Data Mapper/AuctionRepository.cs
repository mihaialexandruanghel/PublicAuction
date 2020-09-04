// <copyright file="AuctionRepository.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the AuctionRepository class.</summary>
namespace PublicAuction.Data_Mapper
{
    using System.Linq;
    using PublicAuction.Domain_Model;

    /// <summary>Class used for communicating directly to database, performing operations on auctions.</summary>
    public class AuctionRepository
    {
        /// <summary>The auction database.</summary>
        private readonly AuctionDb _auctionDb;

        /// <summary>Initializes a new instance of the <see cref="AuctionRepository"/> class.</summary>
        /// <param name="auctionDb">The auction database.</param>
        public AuctionRepository(AuctionDb auctionDb)
        {
            this._auctionDb = auctionDb;
        }

        /// <summary>Gets the auction.</summary>
        /// <param name="id">The id to get the auction.</param>
        /// <returns>Auction returned from database.</returns>
        public Auction GetAuction(int id)
        {
            return this._auctionDb.Auctions.FirstOrDefault(
                a => a.Id.Equals(id));
        }

        /// <summary>Adds the auction.</summary>
        /// <param name="auction">The auction.</param>
        /// <returns>True if the auction was added successfully.</returns>
        public bool AddAuction(Auction auction)
        {
            this._auctionDb.Auctions.Add(auction);
            return this._auctionDb.SaveChanges() != 0;
        }
    }
}