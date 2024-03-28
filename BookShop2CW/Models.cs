using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop2CW
{
    public class Models
    {
        public class Country
        {
            public int CountryId { get; set; }
            public string CountryName { get; set; }
        }

        public class City
        {
            public int CityId { get; set; }
            public string CityName { get; set; }
            public int CountryId { get; set; }
        }

        public class Publisher
        {
            public int PublisherId { get; set; }
            public string PublisherName { get; set; }
            public int CountryId { get; set; }
        }

        public class Author
        {
            public int AuthorId { get; set; }
            public string AuthorName { get; set; }
            public DateTime BirthDate { get; set; }
            public int CountryId { get; set; }
        }

        public class Customer
        {
            public int CustomerId { get; set; }
            public string CustomerName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public int CountryId { get; set; }
        }

        public class Book
        {
            public int BookId { get; set; }
            public string Title { get; set; }
            public int AuthorId { get; set; }
            public decimal Price { get; set; }
            public bool Availability { get; set; }
        }

        public class Order
        {
            public int OrderId { get; set; }
            public int CustomerId { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal TotalAmount { get; set; }
        }

        public class OrderContent
        {
            public int OrderContentId { get; set; }
            public int OrderId { get; set; }
            public int BookId { get; set; }
        }
    }
}
