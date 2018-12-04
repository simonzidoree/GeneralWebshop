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
    }
}