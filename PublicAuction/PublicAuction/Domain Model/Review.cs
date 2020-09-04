// <copyright file="Review.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the Auction entity class.</summary>
namespace PublicAuction.Domain_Model
{
    /// <summary>Review entity.</summary>
    public class Review
    {
        /// <summary>Gets or sets the from user.</summary>
        /// <value>The from user.</value>
        public User FromUser { get; set; }

        /// <summary>Gets or sets the to user.</summary>
        /// <value>The to user.</value>
        public User ToUser { get; set; }

        /// <summary>Gets or sets the score.</summary>
        /// <value>The score.</value>
        public double Score { get; set; }
    }
}
