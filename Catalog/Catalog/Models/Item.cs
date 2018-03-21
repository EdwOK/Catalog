using Catalog.Domain.Entities;

namespace Catalog.Models
{
    public class Item : Entity
    {
        public string Text { get; set; }

        public string Description { get; set; }
    }
}