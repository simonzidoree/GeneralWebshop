using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
                    IsAdmin = false
                },
                new User
                {
                    Username = "aa",
                    PasswordHash = passwordHashAA,
                    PasswordSalt = passwordSaltAA,
                    IsAdmin = true
                }
            };

            var products = new List<Product>
            {
                new Product
                {
                    Title = "title1",
                    Description = "description1",
                    Price = 11.11,
                    Image =
                        "https://images.pexels.com/photos/67636/rose-blue-flower-rose-blooms-67636.jpeg?auto=compress&cs=tinysrgb&h=350",
                    AmountInStock = 1,
                    Featured = true
                },
                new Product
                {
                    Title = "title2",
                    Description = "description2",
                    Price = 22.22,
                    Image =
                        "https://images.pexels.com/photos/67636/rose-blue-flower-rose-blooms-67636.jpeg?auto=compress&cs=tinysrgb&h=350",
                    AmountInStock = 2,
                    Featured = false
                },
                new Product
                {
                    Title = "title3",
                    Description = "description3",
                    Price = 33.33,
                    Image =
                        "https://images.pexels.com/photos/67636/rose-blue-flower-rose-blooms-67636.jpeg?auto=compress&cs=tinysrgb&h=350",
                    AmountInStock = 3,
                    Featured = false
                },
                new Product
                {
                    Title = "title4",
                    Description = "description4",
                    Price = 44.44,
                    Image =
                        "https://images.pexels.com/photos/67636/rose-blue-flower-rose-blooms-67636.jpeg?auto=compress&cs=tinysrgb&h=350",
                    AmountInStock = 4,
                    Featured = true
                }
            };


            var orders = new List<Order>
            {
                new Order
                {
                    OrderNumber = 4353453,
                    FullName = "FM",
                    Address = "A 404",
                    Zipcode = 6700,
                    City = "City",
                    Country = "Denmark",
                    PhoneNumber = 35335353,
                    Email = "e@e.com",
                    Comment = "Nice comment",
                    OrderDate = DateTime.Now.ToString("dd-MM-yyyy"),
                    IsDelivered = false,
                    Products = products
                }
            };

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