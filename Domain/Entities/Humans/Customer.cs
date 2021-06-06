using System;
using BikeCommon;

namespace Domain.Entities.Humans
{
    [Serializable]
    public class Customer : Human, IEquatable<Customer>
    {
        public override bool Equals(object obj) => obj is Customer other && Equals(other);
        public bool Equals(Customer other) => other != null && Id.Equals(other.Id);
        public override int GetHashCode() => Id.GetHashCode();

        public Customer(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }
    }
}
