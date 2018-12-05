using System.Collections.Generic;
using Webshop.Core.DomainService;
using Webshop.Core.Entities;

namespace Webshop.Infrastructure.Data.RepositoriesSQL
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebshopContext _ctx;

        public ProductRepository(WebshopContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Product> ReadAllProducts()
        {
            return _ctx.Products;
        }

        public Product CreateProduct(Product product)
        {
            var productFromDb = _ctx.Products.Add(product).Entity;
            _ctx.SaveChanges();
            return productFromDb;
        }

        public Product DeleteProduct(int id)
        {
            var productRemoved = _ctx.Remove(new Product {Id = id}).Entity;
            _ctx.SaveChanges();
            return productRemoved;
        }
    }
}