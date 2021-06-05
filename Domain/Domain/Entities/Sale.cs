using Domain.Entities.Common;
using Domain.Entities.Humans;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    [Serializable]
    public class Sale : IQueryableEntity
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public Salesperson Salesperson { get; set; }
        public Customer Customer { get; set; }
        public DateTimeOffset SaleDate { get; set; }

    }
}
