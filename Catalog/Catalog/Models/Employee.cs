using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Catalog.Models
{
    public class Employee : Entity
    {
        [MaxLength(40)]
        public string FirstName { get; set; }

        [MaxLength(40)]
        public string Surname { get; set; }

        [MaxLength(40)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(20)]
        public string Position { get; set; }

        [MaxLength(24)]
        public string PhoneNumber { get; set; }

        public double Salary { get; set; }

        public DateTime DateOfBirth { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public List<Order> Orders { get; set; }

        [Ignore]
        public string FullName => $"{FirstName} {Surname} {LastName}";
    }
}
