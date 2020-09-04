// <copyright file="AuctionDb.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the AuctionDb context.</summary>
namespace PublicAuction.Data_Mapper
{
    using System.Data.Entity;
    using PublicAuction.Domain_Model;

    /// <summary>Database entities.</summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class AuctionDb : DbContext
    {
        // Your context has been configured to use a 'AuctionDb' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'PublicAuction.Data_Mapper.AuctionDb' database on your LocalDb instance. 
        // If you wish to target a different database and/or database provider, modify the 'AuctionDb' 
        // connection string in the application configuration file.

        /// <summary>Initializes a new instance of the <see cref="AuctionDb"/> class.</summary>
        public AuctionDb()
            : base("name=AuctionDB")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AuctionDb>());
        }

        /// <summary>Gets or sets the auctions.</summary>
        /// <value>The auctions.</value>
        public DbSet<Auction> Auctions { get; set; }

        /// <summary>Gets or sets the prices.</summary>
        /// <value>The prices.</value>
        public DbSet<Price> Prices { get; set; }

        /// <summary>Gets or sets the review.</summary>
        /// <value>The review.</value>
        public DbSet<Review> Reviews { get; set; }

        /// <summary>Gets or sets the products books.</summary>
        /// <value>The products.</value>
        public DbSet<Product> Products { get; set; }

        /// <summary>Gets or sets the categories.</summary>
        /// <value>The categories.</value>
        public DbSet<Category> Categories { get; set; }

        /// <summary>Gets or sets the users.</summary>
        /// <value>The users.</value>
        public DbSet<User> Users { get; set; }

        /// <summary>Gets or sets the bids.</summary>
        /// <value>The auction bids.</value>
        public DbSet<Bid> Bids { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    // public class MyEntity
    // {
    // public int Id { get; set; }
    // public string Name { get; set; }
    // }
}