// <copyright file="ReviewRepository.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the ReviewRepository class.</summary>

namespace PublicAuction.Data_Mapper
{
    using System.Linq;
    using PublicAuction.Domain_Model;

    /// <summary>Class used for communicating directly to database, performing operations on reviews.</summary>
    public class ReviewRepository
    {
        /// <summary>The auction database.</summary>
        private readonly AuctionDb auctionDb;

        /// <summary>Initializes a new instance of the <see cref="ReviewRepository"/> class.</summary>
        /// <param name="auctionDb">The auction database.</param>
        public ReviewRepository(AuctionDb auctionDb)
        {
            this.auctionDb = auctionDb;
        }

        /// <summary>Adds the review.</summary>
        /// <param name="review">The category.</param>
        /// <returns>True if category was added to database.</returns>
        public bool AddReview(Review review)
        {
            this.auctionDb.Reviews.Add(review);
            return this.auctionDb.SaveChanges() != 0;
        }

        /// <summary>Gets the reviews.</summary>
        /// <param name="user">The user reviewed.</param>
        /// <returns>Reviews for a specific user from database.</returns>
        public IQueryable<Review> GetReviews(User user)
        {
            return this.auctionDb.Reviews.Where(u => u.ToUser == user);
        }
    }
}
