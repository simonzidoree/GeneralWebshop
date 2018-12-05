using System.Collections.Generic;
using Webshop.Core.Entities;

namespace Webshop.Core.ApplicationService
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product CreateProduct(Product product);
        Product DeleteProduct(int id);
        Product FindProductById(int id);
        Product UpdateProduct(int id, Product productUpdate);
    }
}