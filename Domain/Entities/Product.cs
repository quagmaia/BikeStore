using BikeCommon;
using System;

namespace Domain.Entities
{
    [Serializable]
    public class Product : QuertableEntity, IEquatable<Product>
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public int QtyOnHand { get; set; }
        public decimal CommissionFactor { get; set; }
        public override bool Equals(object obj) => obj is Product other && Equals(other);
        public bool Equals(Product other) => other != null && Id.Equals(other.Id);
        public override int GetHashCode() => Id.GetHashCode();

        public Product(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }

        public bool Matches(Product other)
        {
            return other != null && Name.Equals(other.Name) && Manufacturer.Equals(other.Manufacturer);
        }
    }
}
