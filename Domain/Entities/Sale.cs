using BikeCommon;
using Domain.Entities.Humans;
using System;

namespace Domain.Entities
{
    [Serializable]
    public class Sale : QuertableEntity, IEquatable<Sale>
    {
        public Product Product { get; set; }
        public Salesperson Salesperson { get; set; }
        public Customer Customer { get; set; }
        public DateTimeOffset SaleDate { get; set; }
        public override bool Equals(object obj) => obj is Sale other && Equals(other);
        public bool Equals(Sale other) => Id.Equals(other.Id);
        public override int GetHashCode() => Id.GetHashCode();

        public Sale(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }

    }
}
