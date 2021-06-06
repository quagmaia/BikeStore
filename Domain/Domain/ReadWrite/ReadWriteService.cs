using Domain.Entities;
using Domain.Entities.Humans;
using Domain.Events;
using Persistence;

namespace Domain.ReadWrite
{
    public class ReadWriteService
    {
        public static IEntityReadWrite<Product> ProductRw 
            = new EntityReadWrite<Product>(FileNames.Products, ReadWriteEventHandler.Handle);

        public static IEntityReadWrite<Discount> DiscountRw 
            = new EntityReadWrite<Discount>(FileNames.Discounts, ReadWriteEventHandler.Handle);

        public static IEntityReadWrite<Sale> SaleRw 
            = new EntityReadWrite<Sale>(FileNames.Sales, ReadWriteEventHandler.Handle);

        public static IEntityReadWrite<Customer> CustomerRw 
            = new EntityReadWrite<Customer>(FileNames.Customers, ReadWriteEventHandler.Handle);

        public static IEntityReadWrite<Salesperson> SalespersonRw 
            = new EntityReadWrite<Salesperson>(FileNames.Salespeople, ReadWriteEventHandler.Handle);
    }
}
