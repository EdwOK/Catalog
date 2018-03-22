using System;
using SQLite;

namespace Catalog.Models
{
    public class Product : Entity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public double Price { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime DeliveryDate { get; set; }
    }
}
