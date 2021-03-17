using LawFirm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawFirm.Data
{
    public class DbInitializer
    {
        public static void Initialize(LawFirmContext context)
        {
            context.Database.EnsureCreated();

            if (context.Customers.Any())
            {
                return;
            }

            var customer = new Customer[]
            {
                new Customer{INN = "123456789123", Name = "Rich", Type = "Individual entrepreneur", FounderDate = DateTime.Parse("2021-03-15"), FouderUpdate = DateTime.Now},

                new Customer{INN = "9876543210", Name = "Tom", Type = "Person", FounderDate = DateTime.Parse("2021-03-15"), FouderUpdate = DateTime.Now}
            };
            foreach (Customer c in customer)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();

            var founder = new Founder[]
            {
                new Founder{CustomerId = 1, INN = "0123456789", FirstName = "Benjamin", LastName = "Piana", Patronymic = "Miller", CustomerData = DateTime.Parse("2021-03-15"), CustomerUpdate = DateTime.Now},
                new Founder{CustomerId = 2, INN = "0123444789", FirstName = "Mike", LastName = "Johnson", CustomerData = DateTime.Parse("2021-03-15"), CustomerUpdate = DateTime.Now}
            };
            foreach(Founder f in founder)
            {
                context.Founders.Add(f);
                context.SaveChanges();
            }
        }
    }
}
