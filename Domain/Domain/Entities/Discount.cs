using Domain.Entities.Common;
using System;

namespace Domain.Entities
{
    public class Discount : IQueryableEntity
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public decimal DiscountFactor { get; set; }
    }
}
