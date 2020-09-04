// <copyright file="UserRepository.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the UserRepository class.</summary>

namespace PublicAuction.Data_Mapper
{
    using System.Linq;
    using PublicAuction.Domain_Model;

    /// <summary>Class used for communicating directly to database, performing operations on users.</summary>
    public class UserRepository
    {
        /// <summary>The auction database.</summary>
        private readonly AuctionDb _auctionDb;

        /// <summary>Initializes a new instance of the <see cref="UserRepository"/> class.</summary>
        /// <param name="auctionDb">The auction database.</param>
        public UserRepository(AuctionDb auctionDb)
        {
            this._auctionDb = auctionDb;
        }

        /// <summary>Gets the user.</summary>
        /// <param name="id">The id to get the user.</param>
        /// <returns>User returned from database.</returns>
        public User GetUser(int id)
        {
            return this._auctionDb.Users.FirstOrDefault(
                a => a.Id.Equals(id));
        }

        /// <summary>Adds the user.</summary>
        /// <param name="user">The user to be added.</param>
        /// <returns>True if the user was added successfully.</returns>
        public bool AddUser(User user)
        {
            this._auctionDb.Users.Add(user);
            return this._auctionDb.SaveChanges() != 0;
        }
    }
}
