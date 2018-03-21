using SQLite;

namespace Catalog.Domain.Entities
{
    public class Customer : Entity
    {
        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(40)]
        public string City { get; set; }

        [MaxLength(40)]
        public string Country { get; set; }

        [MaxLength(20)]
        public string PostalCode { get; set; }

        [MaxLength(24)]
        public string PhoneNumber { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
