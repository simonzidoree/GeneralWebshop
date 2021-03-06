using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Webshop.Core.Entities;

namespace Webshop.Infrastructure.Data
{
    public class DBInitializer
    {
        public static void SeedDB(WebshopContext ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            // Create two users with hashed and salted passwords
            var password = "b";
            byte[] passwordHashA, passwordSaltA, passwordHashAA, passwordSaltAA;
            CreatePasswordHash(password, out passwordHashA, out passwordSaltA);
            CreatePasswordHash(password, out passwordHashAA, out passwordSaltAA);

            var users = new List<User>
            {
                new User
                {
                    Username = "a",
                    PasswordHash = passwordHashA,
                    PasswordSalt = passwordSaltA,
                    IsAdmin = true
                }
            };

            var products = new List<Product>
            {
                new Product
                {
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

            var orders = new List<Order>
            {
                new Order
                {
                    OrderNumber = 23534534,
                    FullName = "FM",
                    Address = "A 404",
                    Zipcode = 6700,
                    City = "City",
                    Country = "Denmark",
                    PhoneNumber = 35335353,
                    Email = "e@e.com",
                    Comment = "Nice comment",
                    OrderDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"),
                    IsDelivered = false
                }
            };

            var order1 = ctx.Orders.Add(new Order
            {
                OrderNumber = 23000001,
                FullName = "Jens Jensen",
                Address = "FO Street 1",
                Zipcode = 6700,
                City = "Esbjerg",
                Country = "Denmark",
                PhoneNumber = 11111111,
                Email = "j@j.dk",
                Comment = "Nice comment",
                OrderDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"),
                IsDelivered = false
            }).Entity;

            var product1 = ctx.Products.Add(new Product
            {
                Title = "ASUS ROG G703",
                Description =
                    "ROG G703 is a beast of a gaming laptop that has the power to take on today’s gaming desktops, thanks to its factory-overclocked 8th Generation Intel Core i9 processor and overclockable NVIDIA GeForce GTX 1080 graphics.",
                Price = 33.33,
                Image =
                    "https://www.asus.com/media/global/products/MR9WA0YsXc377gln/P_setting_xxx_0_90_end_300.png",
                AmountInStock = 11,
                Featured = false,
                Category = "Computere"
            }).Entity;

            ctx.OrderLines.Add(new OrderLine
            {
                Product = product1,
                Order = order1,
                Qty = 1,
                PriceWhenBought = 1000
            });

            ctx.Users.AddRange(users);
            ctx.Products.AddRange(products);
            ctx.Orders.AddRange(orders);

            ctx.SaveChanges();
        }

        // This method computes a hashed and salted password using the HMACSHA512 algorithm.
        // The HMACSHA512 class computes a Hash-based Message Authentication Code (HMAC) using
        // the SHA512 hash function. When instantiated with the parameterless constructor (as
        // here) a randomly Key is generated. This key is used as a password salt.

        // The computation is performed as shown below:
        //   passwordHash = SHA512(password + Key)

        // A password salt randomizes the password hash so that two identical passwords will
        // have significantly different hash values. This protects against sophisticated attempts
        // to guess passwords, such as a rainbow table attack.
        // The password hash is 512 bits (=64 bytes) long.
        // The password salt is 1024 bits (=128 bytes) long.
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}