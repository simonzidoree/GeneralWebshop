using System;
using System.Collections.Generic;

namespace Webshop.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int Zipcode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public string OrderDate { get; set; }
        public bool IsDelivered { get; set; }
        public bool EmailSent { get; set; }
        public List<Product> Products { get; set; }
    }
}