// <copyright file="PriceService.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the PriceService class.</summary>
namespace PublicAuction.BusinessLayer
{
    using System.Configuration;
    using Castle.Core.Internal;
    using PublicAuction.Data_Mapper;
    using PublicAuction.Domain_Model;

    /// <summary>Price service class using price repository.</summary>
    public class PriceService
    {
        /// <summary>The price repository.</summary>
        private readonly PriceRepository _priceRepository;

        /// <summary>Initializes a new instance of the <see cref="PriceService"/> class.</summary>
        /// <param name="priceRepository">The review repository.</param>
        public PriceService(PriceRepository priceRepository)
        {
            this._priceRepository = priceRepository;
        }

        /// <summary>Adds the price.</summary>
        /// <param name="price">The price.</param>
        /// <returns>Returns a boolean value if price added successfully.</returns>
        public bool AddPrice(Price price)
        {
            if (price == null)
            {
                LoggerUtil.LogInfo("Cannot create a null price!");
                return false;
            }

            var lim = double.Parse(ConfigurationManager.AppSettings["MIN_AUCTION_PRICE"]);
            if (price.ThePrice < lim)
            {
                LoggerUtil.LogInfo("The price can not be lower the minimum action value!");
                return false;
            }

            if (price.Currency.IsNullOrEmpty())
            {
                LoggerUtil.LogInfo("The Currency can not be null or empty!");
                return false;
            }

            return this._priceRepository.AddPrice(price);
        }
    }
}
