using System.Collections.Generic;
using System.Linq;
using Webshop.Core.DomainService;
using Webshop.Core.Entities;

namespace Webshop.Core.ApplicationService.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.ReadAllProducts().ToList();
        }

        public Product CreateProduct(Product product)
        {
            return _productRepository.CreateProduct(product);
        }

        public Product DeleteProduct(int id)
        {
            return _productRepository.DeleteProduct(id);
        }

        public Product FindProductById(int id)
        {
            return _productRepository.FindProductById(id);
        }

        public Product UpdateProduct(int id, Product productUpdate)
        {
            var product = FindProductById(id);
            product.Title = productUpdate.Title;
            product.Description = productUpdate.Description;
            product.Price = productUpdate.Price;
            product.Image = productUpdate.Image;
            product.AmountInStock = productUpdate.AmountInStock;
            product.Featured = productUpdate.Featured;
            product.Category = productUpdate.Category;
            return _productRepository.UpdateProduct(product);
        }
    }
}