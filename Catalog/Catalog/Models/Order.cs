using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Catalog.Models
{
    public class Order : Entity
    {
        public Order()
        {
            Products = new List<Product>();
            CreatedDate = DateTime.UtcNow.Date;
        }

        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        [Ignore]
        public double TotalPrice
        {
            get
            {
                return Products.Sum(product => product.Price);
            }
        }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead | CascadeOperation.CascadeDelete)]
        public List<Product> Products { get; set; }

        [ForeignKey(typeof(Employee))]
        public int EmployeeId { get; set; }

        [ManyToOne]
        public Employee Employee { get; set; }

        [ForeignKey(typeof(Customer))]
        public int CustomerId { get; set; }

        [ManyToOne]
        public Customer Customer { get; set; }
    }
}
