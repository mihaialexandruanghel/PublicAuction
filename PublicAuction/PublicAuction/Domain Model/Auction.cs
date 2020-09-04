// <copyright file="Auction.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the Auction entity class.</summary>
namespace PublicAuction.Domain_Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>Auction entity.</summary>
    public class Auction
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The user name.</value>
        [Index(IsUnique = false)]
        [StringLength(450)]
        public string Name { get; set; }

        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        public DateTime StartDate { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        public DateTime EndDate { get; set; }

        /// <summary>Gets or sets the Is Closed.</summary>
        /// <value>The IsClosed.</value>
        public bool IsClosed { get; set; }

        /// <summary>Gets or sets the price.</summary>
        /// <value>The price.</value>
        public Price StartingPrice { get; set; }

        /// <summary>Gets or sets the offer user.</summary>
        /// <value>The offer user.</value>
        public User OfferUser { get; set; }

        /// <summary>Gets or sets the auctioneers.</summary>
        /// <value>The auctioneers.</value>
        public ICollection<User> Auctioneers { get; set; }

        /// <summary>Gets or sets the product.</summary>
        /// <value>The product.</value>
        public Product Product { get; set; }

        /// <summary>Gets or sets the Bid.</summary>
        /// <value>The Auction Bid.</value>
        public Bid Bid { get; set; }
    }
}
