// <copyright file="ProductService.cs" company="Transilvania University Of Brasov">
// <author>Anghel Mihai-Alexandru</author>
// </copyright>
// <summary>This is the ProductService class.</summary>
namespace PublicAuction.BusinessLayer
{
    using System.Linq;
    using Castle.Core.Internal;
    using PublicAuction.Data_Mapper;
    using PublicAuction.Domain_Model;

    /// <summary> Product service class using product repository.</summary>
    public class ProductService
    {
        /// <summary>The product repository.</summary>
        private readonly ProductRepository _productRepository;

        /// <summary>The categories service.</summary>
        private readonly CategoriesService _categoriesService;

        /// <summary>Initializes a new instance of the <see cref="ProductService"/> class.</summary>
        /// <param name="productRepository">The product repository.</param>
        /// /// <param name="categoriesService">The category service.</param>
        public ProductService(ProductRepository productRepository, CategoriesService categoriesService)
        {
            this._productRepository = productRepository;
            this._categoriesService = categoriesService;
        }

        /// <summary>Adds the product.</summary>
        /// <param name="product">The product.</param>
        /// <returns>Returns a boolean value if product added successfully.</returns>
        public bool AddProduct(Product product)
        {
            if (product == null)
            {
                LoggerUtil.LogInfo("Cannot create a null product!");
                return false;
            }

            if (product.Name.IsNullOrEmpty())
            {
                LoggerUtil.LogInfo("Name can not be null or empty!");
                return false;
            }

            if ((product.Name.Length < 3) || (product.Name.Length > 100))
            {
                LoggerUtil.LogInfo("Name can not be under 3 characters!");
                return false;
            }

            if (!product.Name.All(a => char.IsLetter(a) || char.IsWhiteSpace(a) || (a == '-')))
            {
                LoggerUtil.LogInfo("Cannot set a Name which has no characters!");
                return false;
            }

            if (product.Price == null)
            {
                LoggerUtil.LogInfo("The Price of an product can not be null!");
                return false;
            }

            if (product.Category == null)
            {
                LoggerUtil.LogInfo("The Category of an product can not be null!");
                return false;
            }

            foreach (var productCategory in product.Category)
            {
                var categories = product.Category.ToList();
                categories.Remove(productCategory);
                if (this._categoriesService.CategoryIsPartOfCategories(productCategory, categories))
                {
                    return false;
                }
            }

            return this._productRepository.AddProduct(product);
        }

        /// <summary>Gets the auction.</summary>
        /// <param name="id">The id for getting the product.</param>
        /// <returns>An Auction from database.</returns>
        public Product GetProduct(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return this._productRepository.GetProduct(id);
        }
    }
}
