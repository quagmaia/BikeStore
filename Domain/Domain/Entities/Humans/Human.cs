using BikeCommon;
using System;

namespace Domain.Entities.Humans
{
    public abstract class Human : IQueryableEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FormattedName => $"{LastName}, {FirstName}";
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTimeOffset StartDate { get; set; }
    }
}
