using System;
using SQLite;

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

        [Ignore]
        public string FullName => $"{FirstName} {Surname} {LastName}";
    }
}
