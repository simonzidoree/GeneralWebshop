using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Webshop.Core.ApplicationService;
using Webshop.Core.Entities;

namespace Webshop.RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController(IProductService productService)
        {
            ProductService = productService;
        }

        private IProductService ProductService { get; }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(ProductService.GetAllProducts());
        }

        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            if (product.Title == null || Math.Abs(product.Price) < 0.01 || product.AmountInStock == 0)
            {
                return BadRequest("The Product has to have info for Title, Price & AmountInStock!");
            }

            return Ok(ProductService.CreateProduct(product));
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            return Ok(ProductService.DeleteProduct(id));
        }

        [HttpPut("{id}")]
        public ActionResult<Product> Put(int id, [FromBody] Product product)
        {
            if (id < 1 || id != product.Id)
            {
                return BadRequest("Parameter Id and product ID must be the same");
            }

            return Ok(ProductService.UpdateProduct(id, product));
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = ProductService.FindProductById(id);

            if (product == null)
            {
                return BadRequest($"There is no product with the ID: {id}");
            }

            return Ok(product);
        }
    }
}