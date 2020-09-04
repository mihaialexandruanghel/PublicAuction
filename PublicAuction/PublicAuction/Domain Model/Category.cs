// <copyright file="Category.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the Category entity class.</summary>
namespace PublicAuction.Domain_Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>Category entity.</summary>
    public class Category
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The category name.</value>
        [Index(IsUnique = false)]
        [StringLength(450)]
        public string CategoryName { get; set; }

        /// <summary>Gets or sets the list of products.</summary>
        /// <value>The list of products.</value>
        public ICollection<Product> Products { get; set; }

        /// <summary>Gets or sets the list of parent categories.</summary>
        /// <value>The list of parent categories.</value>
        public Category ParentCategory { get; set; }
    }
}
