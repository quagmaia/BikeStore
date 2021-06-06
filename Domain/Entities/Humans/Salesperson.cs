using System;
using BikeCommon;

namespace Domain.Entities.Humans
{
    [Serializable]
    public class Salesperson : Human, IEquatable<Salesperson>
    {
        public DateTimeOffset TerminationDate { get; set; }
        public Salesperson Manager
        {
            get => _Manager;
            set 
            { 
                EnsureManagerIsNotSelf(value); 
                _Manager = value; 
            }
        }

        public override bool Equals(object obj) => obj is Salesperson other && Equals(other);
        public bool Equals(Salesperson other) => Id.Equals(other.Id);
        public override int GetHashCode() => Id.GetHashCode();

        private Salesperson _Manager { get; set; }
        private void EnsureManagerIsNotSelf(Salesperson newManager)
        {
            if (Equals(newManager))
            {
                throw new Exception($"{FormattedName} cannot be their own manager!");
            }
        }

        public Salesperson(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }

        public bool Matches(Salesperson other)
        {
            return other != null && FormattedName.Equals(other.FormattedName);
        }
    }
}
