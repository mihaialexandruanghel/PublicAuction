// <copyright file="Price.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the Price entity class.</summary>
namespace PublicAuction.Domain_Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>Price entity.</summary>
    public class Price
    {
        /// <summary>Gets or sets the price.</summary>
        /// <value>The price.</value>
        [Index(IsUnique = true)]
        public double ThePrice { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Currency { get; set; }
    }
}
