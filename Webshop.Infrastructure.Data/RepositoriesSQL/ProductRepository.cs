using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            _ctx.Attach(product).State = EntityState.Added;
            _ctx.SaveChanges();
            return product;
        }

        public Product DeleteProduct(int id)
        {
            var productRemoved = _ctx.Remove(new Product {ProductId = id}).Entity;
            _ctx.SaveChanges();
            return productRemoved;
        }

        public Product FindProductById(int id)
        {
            return _ctx.Products
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Order)
                .FirstOrDefault(o => o.ProductId == id);
        }

        public Product UpdateProduct(Product productUpdate)
        {
            var newOrderLines = new List<OrderLine>(productUpdate.OrderLines);

            _ctx.Attach(productUpdate).State = EntityState.Modified;

            _ctx.OrderLines.RemoveRange(
                _ctx.OrderLines.Where(ol => ol.ProductId == productUpdate.ProductId)
            );

            foreach (var ol in newOrderLines)
            {
                _ctx.Entry(ol).State = EntityState.Added;
            }

            _ctx.SaveChanges();

            return productUpdate;

//            _ctx.Attach(productUpdate).State = EntityState.Modified;
//            _ctx.SaveChanges();
//
//            return productUpdate;
        }
    }
}