using System.Collections.Generic;
using Webshop.Core.Entities;

namespace Webshop.Core.DomainService
{
    public interface IProductRepository
    {
        IEnumerable<Product> ReadAllProducts();
        Product CreateProduct(Product product);
    }
}