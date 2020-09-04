// <copyright file="ReviewService.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the ReviewService class.</summary>
namespace PublicAuction.BusinessLayer
{
    using System.Linq;
    using PublicAuction.Data_Mapper;
    using PublicAuction.Domain_Model;

    /// <summary>Review service class using review repository.</summary>
    public class ReviewService
    {
        /// <summary>The review repository.</summary>
        private readonly ReviewRepository reviewRepository;

        /// <summary>Initializes a new instance of the <see cref="ReviewService"/> class.</summary>
        /// <param name="reviewRepository">The review repository.</param>
        public ReviewService(ReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        /// <summary>Adds the review.</summary>
        /// <param name="review">The review.</param>
        /// <returns>Returns a boolean value if review added successfully.</returns>
        public bool AddReview(Review review)
        {
            if (review == null)
            {
                LoggerUtil.LogInfo("Cannot create a null review!");
                return false;
            }

            if (review.FromUser == null)
            {
                LoggerUtil.LogInfo("FromUser can not be null!");
                return false;
            }

            if (review.ToUser == null)
            {
                LoggerUtil.LogInfo("ToUser can not be null!");
                return false;
            }

            if (review.Score < 0.0)
            {
                LoggerUtil.LogInfo("The Score can not be negative!");
                return false;
            }

            if (review.Score > 10.0)
            {
                LoggerUtil.LogInfo("The Score can not be greater than 10!");
                return false;
            }

            return this.reviewRepository.AddReview(review);
        }

        /// <summary>Gets the reviews.</summary>
        /// <param name="user">The user reviewed.</param>
        /// <returns>Reviews for a specific user from database.</returns>
        public IQueryable<Review> GetAllReviews(User user)
        {
            return this.reviewRepository.GetReviews(user);
        }

        /// <summary>Check if there is an existing review for the fromUser to toUser.</summary>
        /// <param name="fromUser">The user who reviews.</param>
        /// <param name="toUser">The user reviewed.</param>
        /// <returns>Reviews true if exists and false if not.</returns>
        public bool CheckIfExists(User fromUser, User toUser)
        {
            IQueryable<Review> reviews = this.reviewRepository.GetReviews(toUser);
            foreach (var review in reviews)
            {
                if (review.FromUser == fromUser)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
