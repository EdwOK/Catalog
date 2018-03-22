using System;

namespace Catalog.BusinessLayer.Entities
{
    public interface IEntity : IEquatable<Entity>
    {
        int Id { get; set; }
    }
}
