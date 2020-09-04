// <copyright file="ProductRepository.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the ProductRepository class.</summary>
namespace PublicAuction.Data_Mapper
{
    using System.Linq;
    using PublicAuction.Domain_Model;

    /// <summary>Class used for communicating directly to database, performing operations on product.</summary>
    public class ProductRepository
    {
        /// <summary>The auction database.</summary>
        private readonly AuctionDb _auctionDb;

        /// <summary>Initializes a new instance of the <see cref="ProductRepository"/> class.</summary>
        /// <param name="auctionDb">The auction database.</param>
        public ProductRepository(AuctionDb auctionDb)
        {
            this._auctionDb = auctionDb;
        }

        /// <summary>Gets the product.</summary>
        /// <param name="id">The id to get the product.</param>
        /// <returns>Product returned from database.</returns>
        public Product GetProduct(int id)
        {
            return this._auctionDb.Products.FirstOrDefault(
                a => a.Id.Equals(id));
        }

        /// <summary>Adds the product.</summary>
        /// <param name="product">The product.</param>
        /// <returns>True if the product was added successfully.</returns>
        public bool AddProduct(Product product)
        {
            this._auctionDb.Products.Add(product);
            return this._auctionDb.SaveChanges() != 0;
        }
    }
}
