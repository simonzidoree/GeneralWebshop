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
    }
}