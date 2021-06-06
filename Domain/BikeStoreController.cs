using System.Collections.Generic;
using Domain.Entities;
using Domain.Entities.Humans;
using BikeCommon;
using Domain.ReadWrite;
using System.Linq;
using System;
using Persistence;

namespace Domain
{
    public class BikeStoreController
    {
        public Dictionary<Guid, Product> Products => ReadWriteService.ProductRw.Entities;
        public Dictionary<Guid, Discount> Discounts => ReadWriteService.DiscountRw.Entities;
        public Dictionary<Guid, Sale> Sales => ReadWriteService.SaleRw.Entities;
        public Dictionary<Guid, Customer> Customers => ReadWriteService.CustomerRw.Entities;
        public Dictionary<Guid, Salesperson> Salepeople => ReadWriteService.SalespersonRw.Entities;
        public List<Sale> GetSalesByDataRange(DateRange range) => Sales.Values.Where(s => range.Covers(s.SaleDate)).ToList();

        public BikeStoreController()
        {
            /*
            var bike = new Product()
            {
                Name = "CoolBike",
                PurchasePrice = 120M,
                SalePrice = 209.95M,
                QtyOnHand = 3,
                Manufacturer = "BikeBros Inc",
                CommissionFactor = .3M
            };

            var thing = new Discount() {
                DiscountFactor = .10M,
                Product = bike
            };

            var person = new Salesperson()
            {
                FirstName = "Jerry",
                LastName = "Seinfeld",
                Address = "123 Ln",
                Phone = "123"
            };

            var person2 = new Salesperson()
            {
                FirstName = "George",
                LastName = "Costanza",
                Address = "456 Ln",
                Phone = "456",
                Manager = person
            };

            var person3 = new Customer()
            {
                FirstName = "Mysterious",
                LastName = "Very",
                Address = "It's a Mystery",
                Phone = "It's also a Mystery"
            };

            var sale = new Sale()
            {
                Product = bike,
                Salesperson = person2,
                Customer = person3,
                SaleDate = DateTimeOffset.Now.AddDays(-30)
            };

            foreach (var p in Salepeople.ToList())
            {
                Delete(p.Value);
            }

            foreach (var p in Products)
            {
                Delete(p.Value);
            }


            Add(bike);
            Add(thing);
            Add(person);
            Add(person2);
            Add(person3);
            Add(sale);
            Save();
            */
        }

        

        public void Save() => ReadWriteService.Save();

        public void Reload() => ReadWriteService.Reload();

        public string GetWorkingDirectory() => DirectorySetting.GetWorkingDirectory();

        public string TryGetWorkingDirectory()
        {
            try
            {
                return GetWorkingDirectory();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public void SetWorkingDirectory(string newDirectory) => DirectorySetting.SetWorkingDirectory(newDirectory);

        public void Add(Product product)
        {
            var newProductMatchesExisting = Products.Values.Any(p => p.Matches(product));
            if (newProductMatchesExisting)
            {
                throw DuplicateProduct(product);
            }

            ReadWriteService.ProductRw.Add(product);
        }

        public void Update(Product product)
        {
            var productMatchesADifferentProduct = Products.Values.Any(p => !p.Id.Equals(product.Id) && p.Matches(product));
            if (productMatchesADifferentProduct)
            {
                throw DuplicateProduct(product);
            }

            ReadWriteService.ProductRw.Update(product);
        }

        public void Delete(Product product) => ReadWriteService.ProductRw.Delete(product);

        public void Add(Salesperson person)
        {
            var newPersonMatchesExisting = Salepeople.Values.Any(p => p.Matches(person));
            if (newPersonMatchesExisting)
            {
                throw DuplicatePerson(person);
            }

            ReadWriteService.SalespersonRw.Add(person);
        }

        public void Update(Salesperson person)
        {
            var newPersonMatchesExisting = Salepeople.Values.Any(p => p.Matches(person));
            if (newPersonMatchesExisting)
            {
                throw DuplicatePerson(person);
            }

            ReadWriteService.SalespersonRw.Add(person);
        }

        public void Delete(Salesperson person) => ReadWriteService.SalespersonRw.Delete(person);

        public void Add(Discount discount) => ReadWriteService.DiscountRw.Add(discount);

        public void Update(Discount discount) => ReadWriteService.DiscountRw.Update(discount);

        public void Delete(Discount discount) => ReadWriteService.DiscountRw.Delete(discount);

        public void Add(Sale sale) => ReadWriteService.SaleRw.Add(sale);

        public void Update(Sale sale) => ReadWriteService.SaleRw.Update(sale);

        public void Delete(Sale sale) => ReadWriteService.SaleRw.Delete(sale);

        public void Add(Customer customer) => ReadWriteService.CustomerRw.Add(customer);

        public void Update(Customer customer) => ReadWriteService.CustomerRw.Update(customer);

        public void Delete(Customer customer) => ReadWriteService.CustomerRw.Delete(customer);

        private BikeException DuplicateProduct(Product product)
            => new BikeException($"Product {product.Name} {product.Manufacturer} already exists!");

        private BikeException DuplicatePerson(Salesperson person)
            => new BikeException($"Product {person.FormattedName} already exists!");
    }
}
