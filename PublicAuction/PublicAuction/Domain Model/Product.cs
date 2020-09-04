// <copyright file="Product.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the Product entity class.</summary>
namespace PublicAuction.Domain_Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>Product entity.</summary>
    public class Product
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The product name.</value>
        [Index(IsUnique = false)]
        [StringLength(450)]
        public string Name { get; set; }

        /// <summary>Gets or sets the price.</summary>
        /// <value>The price.</value>
        public Price Price { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        public IList<Category> Category { get; set; }
    }
}