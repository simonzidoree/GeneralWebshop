using System.Collections.Generic;
using Moq;
using Webshop.Core.ApplicationService;
using Webshop.Core.ApplicationService.Services;
using Webshop.Core.DomainService;
using Webshop.Core.Entities;
using Xunit;

namespace Webshop.xUnitTests.Core.ApplicationService.Services
{
    public class ProductServiceTest
    {
        private Mock<IProductRepository> CreateProductMockRepository()
        {
            var products = new List<Product>
            {
                new Product
                {
                    ProductId = 1,
                    Title = "Lenovo",
                    Description = "description1",
                    Price = 11.11,
                    Image =
                        "https://bilkadk.imgix.net/medias/sys_master/root/hc7/h18/10276123148318.jpg",
                    AmountInStock = 1,
                    Featured = true,
                    Category = "Computere"
                },
                new Product
                {
                    ProductId = 2,
                    Title = "Flot stol",
                    Description = "description2",
                    Price = 22.22,
                    Image =
                        "https://www.room21.dk/bilder/artiklar/zoom/124366_1.jpg",
                    AmountInStock = 2,
                    Featured = false,
                    Category = "Stole"
                },
                new Product
                {
                    ProductId = 3,
                    Title = "Asus",
                    Description = "description3",
                    Price = 33.33,
                    Image =
                        "https://www.asus.com/dk/Commercial-Laptops/ASUSPRO-P4540UQ/websites/global/products/skTsLtvMny8HiIaf/images/asus_p4540_notebook_1.png",
                    AmountInStock = 3,
                    Featured = false,
                    Category = "Computere"
                },
                new Product
                {
                    ProductId = 4,
                    Title = "Sovesofa",
                    Description = "description4",
                    Price = 44.44,
                    Image =
                        "https://ilva.dk/webshop/images/Ide-faelles/100003281057-001.JPG",
                    AmountInStock = 4,
                    Featured = true,
                    Category = "Sofaer"
                }
            };


            var repository = new Mock<IProductRepository>();
            repository.Setup(r => r.ReadAllProducts())
                .Returns(products);

            repository.Setup(x => x.FindProductById(1)).Returns(products[0]);

            return repository;
        }

        [Theory]
        [InlineData(1, 1)]
        private void GetProductById(int id, int expected)
        {
            var repository = CreateProductMockRepository();
            IProductService service = new ProductService(repository.Object);

            var actual = service.FindProductById(id).ProductId;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAllProductCountThrowsNoException()
        {
            var mockProductRepo = CreateProductMockRepository();
            IProductService service = new ProductService(mockProductRepo.Object);

            var expectedProductsCount = 4;
            var actualProductsCount = service.GetAllProducts().Count;

            Assert.Equal(expectedProductsCount, actualProductsCount);
        }

        [Fact]
        public void VerifyCreateProductCallProductRepoCreateProductOnce()
        {
            var mockProductRepo = new Mock<IProductRepository>();
            IProductService service = new ProductService(mockProductRepo.Object);

            var product = new Product
            {
                ProductId = 1,
                Title = "Lenovo",
                Description = "description1",
                Price = 11.11,
                Image =
                    "https://bilkadk.imgix.net/medias/sys_master/root/hc7/h18/10276123148318.jpg",
                AmountInStock = 1,
                Featured = true,
                Category = "Computere"
            };


            service.CreateProduct(product);
            mockProductRepo.Verify(x => x.CreateProduct(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void VerifyDeleteProductCallProductRepoDeleteProductOnce()
        {
            var mockProductRepo = new Mock<IProductRepository>();
            IProductService service = new ProductService(mockProductRepo.Object);

            var product = new Product
            {
                ProductId = 1,
                Title = "Lenovo",
                Description = "description1",
                Price = 11.11,
                Image =
                    "https://bilkadk.imgix.net/medias/sys_master/root/hc7/h18/10276123148318.jpg",
                AmountInStock = 1,
                Featured = true,
                Category = "Computere"
            };


            service.DeleteProduct(product.ProductId);
            mockProductRepo.Verify(x => x.DeleteProduct(product.ProductId), Times.Once);
        }

        [Fact]
        public void VerifyUpdateProductCallProductRepoUpdateProductOnce()
        {
            var mockProductRepo = CreateProductMockRepository();
            IProductService service = new ProductService(mockProductRepo.Object);

            var product = new Product
            {
                ProductId = 1,
                Title = "Lenovo",
                Description = "description1",
                Price = 11.11,
                Image =
                    "https://bilkadk.imgix.net/medias/sys_master/root/hc7/h18/10276123148318.jpg",
                AmountInStock = 1,
                Featured = true,
                Category = "Computere"
            };


            service.UpdateProduct(product.ProductId, product);
            mockProductRepo.Verify(x => x.UpdateProduct(It.IsAny<Product>()), Times.Once);
        }
    }
}