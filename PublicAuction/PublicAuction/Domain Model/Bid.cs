// <copyright file="Bid.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the Bid entity class.</summary>
namespace PublicAuction.Domain_Model
{
    /// <summary>Bid entity.</summary>
    public class Bid
    {
        /// <summary>Gets or sets the Price.</summary>
        /// <value>The Price.</value>
        public User User { get; set; }

        /// <summary>Gets or sets the Price.</summary>
        /// <value>The Price.</value>
        public Price Price { get; set; }
    }
}
