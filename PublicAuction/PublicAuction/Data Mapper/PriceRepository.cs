// <copyright file="PriceRepository.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the PriceRepository class.</summary>
namespace PublicAuction.Data_Mapper
{
    using PublicAuction.Domain_Model;

    /// <summary>Class used for communicating directly to database, performing operations on prices.</summary>
    public class PriceRepository
    {
        /// <summary>The auction database.</summary>
        private readonly AuctionDb _auctionDb;

        /// <summary>Initializes a new instance of the <see cref="PriceRepository"/> class.</summary>
        /// <param name="auctionDb">The auction database.</param>
        public PriceRepository(AuctionDb auctionDb)
        {
            this._auctionDb = auctionDb;
        }

        /// <summary>Adds the price.</summary>
        /// <param name="price">The price.</param>
        /// <returns>True if price added successfully.</returns>
        public bool AddPrice(Price price)
        {
            this._auctionDb.Prices.Add(price);
            return this._auctionDb.SaveChanges() != 0;
        }
    }
}
