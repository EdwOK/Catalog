using System;
using SQLite;

namespace Catalog.BusinessLayer.Entities
{
    public class Product : Entity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime DeliveryDate { get; set; }
    }
}
