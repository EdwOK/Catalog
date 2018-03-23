using System;
using SQLite;

namespace Catalog.Models
{
    public class Employee : Entity
    {
        [MaxLength(40)]
        public string FirstName { get; set; }

        [MaxLength(40)]
        public string SecondName { get; set; }

        [MaxLength(40)]
        public string MiddleName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(20)]
        public string Position { get; set; }

        [MaxLength(24)]
        public string PhoneNumber { get; set; }

        public double Salary { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Ignore]
        public string FullName => $"{SecondName} {FirstName} {MiddleName}";
    }
}
