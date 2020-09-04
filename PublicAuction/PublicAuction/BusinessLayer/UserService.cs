// <copyright file="UserService.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the UserService class.</summary>
namespace PublicAuction.BusinessLayer
{
    using System.Configuration;
    using System.Linq;
    using Castle.Core.Internal;
    using PublicAuction.Data_Mapper;
    using PublicAuction.Domain_Model;

    /// <summary>User service class using user repository.</summary>
    public class UserService
    {
        /// <summary>The user repository.</summary>
        private readonly UserRepository userRepository;

        /// <summary>The review repository.</summary>
        private readonly ReviewRepository reviewRepository;

        /// <summary>Initializes a new instance of the <see cref="UserService"/> class.</summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="reviewRepository">The review repository.</param>
        public UserService(UserRepository userRepository, ReviewRepository reviewRepository)
        {
            this.userRepository = userRepository;
            this.reviewRepository = reviewRepository;
        }

        /// <summary>Adds the user.</summary>
        /// <param name="user">The user to be added.</param>
        /// <returns>Returns a boolean value if user added successfully.</returns>
        public bool AddUser(User user)
        {
            if (user == null)
            {
                LoggerUtil.LogInfo("Cannot create a null user!");
                return false;
            }

            if (user.FirstName.IsNullOrEmpty())
            {
                LoggerUtil.LogInfo("FirstName can not be null or empty!");
                return false;
            }

            if ((user.FirstName.Length < 3) || (user.FirstName.Length > 100))
            {
                LoggerUtil.LogInfo("FirstName can not be under 3 characters!");
                return false;
            }

            if (!user.FirstName.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                LoggerUtil.LogInfo("FirstName can not be under 3 characters!");
                return false;
            }

            if (char.IsLower(user.FirstName.First()))
            {
                LoggerUtil.LogInfo("FirstName can not start with a lower case letter!");
                return false;
            }

            if (user.LastName.IsNullOrEmpty())
            {
                LoggerUtil.LogInfo("LastName can not be null or empty!");
                return false;
            }

            if ((user.LastName.Length < 3) || (user.LastName.Length > 100))
            {
                LoggerUtil.LogInfo("LastName can not be under 3 characters!");
                return false;
            }

            if (!user.LastName.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                LoggerUtil.LogInfo("LastName can not be under 3 characters!");
                return false;
            }

            if (char.IsLower(user.LastName.First()))
            {
                LoggerUtil.LogInfo("LastName can not start with a lower case letter!");
                return false;
            }

            if (user.Email.IsNullOrEmpty())
            {
                LoggerUtil.LogInfo("Email can not be null or empty!");
                return false;
            }

            if (!user.Email.Contains('@'))
            {
                LoggerUtil.LogInfo("Email can not be null or empty!");
                return false;
            }

            if ((user.Email.Length < 3) || (user.Email.Length > 100))
            {
                LoggerUtil.LogInfo("Email can not be under 3 characters!");
                return false;
            }

            if (char.IsUpper(user.Email.First()))
            {
                LoggerUtil.LogInfo("Email can not start with a upper case letter!");
                return false;
            }

            if (user.PhoneNumber.IsNullOrEmpty())
            {
                LoggerUtil.LogInfo("PhoneNumber can not be null or empty!");
                return false;
            }

            if ((user.PhoneNumber.Length < 3) || (user.FirstName.Length > 100))
            {
                LoggerUtil.LogInfo("PhoneNumber can not be under 3 characters!");
                return false;
            }

            if (!user.PhoneNumber.All(a => char.IsNumber(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                LoggerUtil.LogInfo("Phone number can contain only numbers!");
                return false;
            }

            if (user.Address.IsNullOrEmpty())
            {
                LoggerUtil.LogInfo("Address can not be null or empty!");
                return false;
            }

            if ((user.Address.Length < 3) || (user.Address.Length > 100))
            {
                LoggerUtil.LogInfo("Address can not be under 3 characters!");
                return false;
            }

            if (!user.Address.All(a => char.IsLetter(a) || char.IsNumber(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                LoggerUtil.LogInfo("Address can not contains special characters!");
                return false;
            }

            if (char.IsUpper(user.Address.First()))
            {
                LoggerUtil.LogInfo("Address can not start with a upper case letter!");
                return false;
            }

            if (user.Gender.IsNullOrEmpty())
            {
                LoggerUtil.LogInfo("Gender can not be null or empty!");
                return false;
            }

            if (user.Gender.Length > 1)
            {
                LoggerUtil.LogInfo("Gender can not be over 1 characters!");
                return false;
            }

            if (!user.Gender.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                LoggerUtil.LogInfo("Gender must have letters!");
                return false;
            }

            if (!(user.Gender.Equals("M") || user.Gender.Equals("F")))
            {
                LoggerUtil.LogInfo("Gender needs to be M or F!");
                return false;
            }

            user.Score = double.Parse(ConfigurationManager.AppSettings["DEFAULT_SCORE"]);

            return this.userRepository.AddUser(user);
        }

        /// <summary>Gets the user.</summary>
        /// <param name="id">The id to get the user.</param>
        /// <returns>Return user from db.</returns>
        public User GetUser(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return this.userRepository.GetUser(id);
        }

        /// <summary>Calculate Score.</summary>
        /// <param name="user">The user for calculating the score.</param>
        public void CalculateScore(User user)
        {
            if (user == null)
            {
                LoggerUtil.LogError("The user can not be null");
            }
            else
            {
                IQueryable<Review> reviews = this.reviewRepository.GetReviews(user);
                int i = 0;
                while (i < reviews.Count() || i < int.Parse(ConfigurationManager.AppSettings["NUMBER_OF_USERS_TO_SCORE"]))
                {
                    foreach (var review in reviews)
                    {
                        user.Score = (user.Score + review.Score) / 2;
                        i++;
                    }
                }
            }
        }
    }
}
