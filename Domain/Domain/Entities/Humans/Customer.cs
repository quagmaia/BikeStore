using System;
using BikeCommon;

namespace Domain.Entities.Humans
{
    [Serializable]
    public class Customer : Human, IEquatable<Customer>
    {
        public override bool Equals(object obj) => obj is Customer other && Equals(other);
        public bool Equals(Customer other) => Id.Equals(other.Id);
        public override int GetHashCode() => Id.GetHashCode();
    }
}
