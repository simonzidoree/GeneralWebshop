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
    }
}