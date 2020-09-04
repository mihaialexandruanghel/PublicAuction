// <copyright file="CategoriesRepository.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the CategoriesRepository class.</summary>
namespace PublicAuction.Data_Mapper
{
    using System.Linq;

    using PublicAuction.Domain_Model;

    /// <summary>Class used to perform categories operations on database.</summary>
    public class CategoriesRepository
    {
        /// <summary>The auction database.</summary>
        private readonly AuctionDb _auctionDb;

        /// <summary>Initializes a new instance of the <see cref="CategoriesRepository"/> class.</summary>
        /// <param name="auctionDb">The auction database.</param>
        public CategoriesRepository(AuctionDb auctionDb)
        {
            this._auctionDb = auctionDb;
        }

        /// <summary>Adds the category.</summary>
        /// <param name="category">The category.</param>
        /// <returns>True if category was added to database.</returns>
        public bool AddCategory(Category category)
        {
            this._auctionDb.Categories.Add(category);
            var successful = this._auctionDb.SaveChanges() != 0;
            if (successful)
            {
                LoggerUtil.LogInfo($"Category added successfully : {category.CategoryName} ");
            }
            else
            {
                LoggerUtil.LogError($"Category failed to add to db : {category.CategoryName}");
            }

            return successful;
        }

        /// <summary>Gets the category.</summary>
        /// <param name="name">The name of category.</param>
        /// <returns>Category from database.</returns>
        public Category GetCategory(string name)
        {
            return this._auctionDb.Categories.FirstOrDefault(c => c.CategoryName.Equals(name));
        }
    }
}