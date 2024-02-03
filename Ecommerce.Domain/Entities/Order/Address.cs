using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities.Order
{
    public class Address
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Counyty { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public Address()
        {
            
        }
        public Address(string firstName, string lastName, string counyty, string city, string street)
        {
            FirstName = firstName;
            LastName = lastName;
            Counyty = counyty;
            City = city;
            Street = street;
        }
    }
}
