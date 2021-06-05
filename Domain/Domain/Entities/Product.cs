using Domain.Entities.Common;
using System;

namespace Domain.Entities
{
    [Serializable]
    public class Product : IQueryableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public int QtyOnHand { get; set; }
        public decimal CommissionFactor { get; set; }
    }
}
