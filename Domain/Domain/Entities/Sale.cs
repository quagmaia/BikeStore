using Domain.Entities.Common;
using System;

namespace Domain.Entities
{
    public class Sale : IQueryableEntity
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public Salesperson Salesperson { get; set; }
        public Customer Customer { get; set; }
        public DateTimeOffset SaleDate { get; set; }

    }
}
