// <copyright file="User.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the User entity class.</summary>
namespace PublicAuction.Domain_Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>User entity.</summary>
    public class User
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the first name.</summary>
        /// <value>The first name.</value>
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string FirstName { get; set; }

        /// <summary>Gets or sets the last name.</summary>
        /// <value>The last name.</value>
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string LastName { get; set; }

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Email { get; set; }

        /// <summary>Gets or sets the phone number.</summary>
        /// <value>The phone number.</value>
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string PhoneNumber { get; set; }

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Address { get; set; }

        /// <summary>Gets or sets the gender.</summary>
        /// <value>The gender.</value>
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Gender { get; set; }

        /// <summary>Gets or sets the Started Auctions.</summary>
        /// <value>The Started Auctions.</value>
        public int StartedAuctions { get; set; }

        /// <summary>Gets or sets the Started Specified Category Auctions.</summary>
        /// <value>The Started Specified Category Auctions.</value>
        public Dictionary<Category, int> StartedSpecifiedCategoryAuctions { get; set; }

        /// <summary>Gets or sets the Auctions.</summary>
        /// <value>The Auctions.</value>
        public ICollection<Auction> Auctions { get; set; }

        /// <summary>Gets or sets the score.</summary>
        /// <value>The score.</value>
        public double Score { get; set; }
    }
}
