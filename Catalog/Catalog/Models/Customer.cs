using SQLite;

namespace Catalog.Models
{
    public class Customer : Entity
    {
        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(20)]
        public string PostalCode { get; set; }

        [MaxLength(24)]
        public string PhoneNumber { get; set; }
    }
}
