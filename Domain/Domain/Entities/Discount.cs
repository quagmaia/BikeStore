using BikeCommon;
using System;

namespace Domain.Entities
{
    [Serializable]
    public class Discount : IQueryableEntity, IEquatable<Discount>
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public decimal DiscountFactor { get; set; }
        public override bool Equals(object obj) => obj is Discount other && Equals(other);
        public bool Equals(Discount other) => Id.Equals(other.Id);
        public override int GetHashCode() => Id.GetHashCode();
    }
}
