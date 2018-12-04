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
            _productService = productService;
        }

        private IProductService _productService { get; }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(_productService.GetAllProducts());
        }
    }
}