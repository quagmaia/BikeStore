using Domain.Entities;
using Domain.Entities.Humans;
using Domain.ReadWrite;
using Persistence.Events;
using System.Linq;

namespace Domain.Events
{
    public static class ReadWriteEventHandler
    {
        public static void Handle(ReadWriteEvent<Product> e)
        {
            var product = e.Entity;

            var nothingToHandleForAdd = e.Action == ReadWriteAction.Added;
            if (nothingToHandleForAdd)
            {
                return;
            }

            var upstreamDiscounts = ReadWriteService.DiscountRw.Entities.Values.Where(e => e.Product.Equals(product));
            var upstreamSales = ReadWriteService.SaleRw.Entities.Values.Where(e => e.Product.Equals(product));

            switch (e.Action)
            {
                case ReadWriteAction.Updated:
                    foreach (var d in upstreamDiscounts)
                    {
                        d.Product = product;
                        ReadWriteService.DiscountRw.Update(d);
                    }
                    foreach (var s in upstreamSales)
                    {
                        s.Product = product;
                        ReadWriteService.SaleRw.Update(s);
                    }
                    return;

                case ReadWriteAction.Deleted:
                    foreach (var d in upstreamDiscounts)
                    {
                        d.Product = null;
                        ReadWriteService.DiscountRw.Update(d);
                    }
                    foreach (var s in upstreamSales)
                    {
                        s.Product = null;
                        ReadWriteService.SaleRw.Update(s);
                    }
                    return;

                default:
                    return;
            }
        }

        public static void Handle(ReadWriteEvent<Discount> e)
        {
            return;
        }

        public static void Handle(ReadWriteEvent<Sale> e)
        {
            return;
        }

        public static void Handle(ReadWriteEvent<Salesperson> e)
        {
            var nothingToHandleForAdd = e.Action == ReadWriteAction.Added;
            if (nothingToHandleForAdd)
            {
                return;
            }

            var person = e.Entity;
            var upstreamEmployees = ReadWriteService.SalespersonRw.Entities.Values.Where(e => e.Manager.Equals(person));
            var upstreamSales = ReadWriteService.SaleRw.Entities.Values.Where(e => e.Salesperson.Equals(person));

            switch (e.Action)
            {
                case ReadWriteAction.Updated:
                    foreach (var p in upstreamEmployees)
                    {
                        p.Manager = person;
                        ReadWriteService.SalespersonRw.Update(p);
                    }
                    foreach (var s in upstreamSales)
                    {
                        s.Salesperson = person;
                        ReadWriteService.SaleRw.Update(s);
                    }
                    return;
                case ReadWriteAction.Deleted:
                    foreach (var p in upstreamEmployees)
                    {
                        p.Manager = null;
                        ReadWriteService.SalespersonRw.Update(p);
                    }
                    foreach (var s in upstreamSales)
                    {
                        s.Salesperson = null;
                        ReadWriteService.SaleRw.Update(s);
                    }
                    return;
                default:
                    return;
            }
        }

        public static void Handle(ReadWriteEvent<Customer> e)
        {
            var nothingToHandleForAdd = e.Action == ReadWriteAction.Added;
            if (nothingToHandleForAdd)
            {
                return;
            }

            var customer = e.Entity;
            var upstreamSales = ReadWriteService.SaleRw.Entities.Values.Where(e => e.Customer.Equals(customer));
            switch (e.Action)
            {
                case ReadWriteAction.Updated:
                    foreach (var s in upstreamSales)
                    {
                        s.Customer = customer;
                        ReadWriteService.SaleRw.Update(s);
                    }
                    return;
                case ReadWriteAction.Deleted:
                    foreach (var s in upstreamSales)
                    {
                        s.Customer = null;
                        ReadWriteService.SaleRw.Update(s);
                    }
                    return;
                default:
                    return;
            }
        }

    }
}
