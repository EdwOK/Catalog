using System;
using System.Collections.Generic;
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

        public double TotalPrice { get; set; }

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
