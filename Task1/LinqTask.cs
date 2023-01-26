using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            var query = from customer in customers
                        let total = GetTotalExpenses(customer)
                        where total > limit
                        select customer;
            return query.ToList();
        }

        private static decimal GetTotalExpenses(Customer customer)
        {
            var orderTotal = from order in customer.Orders
                             select order.Total;
            return orderTotal.Sum();
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers
                        .Select(customer => (customer, suppliers.Where(s => s.Country == customer.Country && s.City == customer.City)));
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers
                        .Select(customer => (customer, suppliers.Where(s => s.Country == customer.Country && s.City == customer.City)));
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(x => x.Orders.Select(o => o.Total)
            .Sum() > limit && x.Orders.Any());
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            if (customers == null)
                throw new ArgumentNullException();

            return customers.Where(c => c.Orders.Any())
                    .Select(x => (customer: x, dateOfEntry: x.Orders.OrderBy(o => o.OrderDate)
                    .FirstOrDefault().OrderDate));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            var result = new List<(Customer customer, DateTime dateOfEntry)>();
            if (customers == null)
                throw new ArgumentNullException();

            return customers.Where(c => c.Orders.Any())
                     .Select(x => (customer: x, dateOfEntry: x.Orders.OrderBy(o => o.OrderDate)
                     .FirstOrDefault().OrderDate))
                     .OrderBy(c => c.dateOfEntry.Year)
                     .ThenBy(c => c.dateOfEntry.Month)
                     .ThenByDescending(c => c.customer.Orders.Select(o => o.Total).Sum())
                     .ThenBy(x => x.customer.CompanyName);
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            if (customers == null)
                throw new ArgumentNullException();

            return customers
                .Where(c => int.TryParse(c.PostalCode, out _) != true || c.Region == null || c.Phone.StartsWith("(") == false);
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            if (products == null)
                throw new ArgumentNullException();

            return products.GroupBy(item => item.Category,
              (key, group) => new Linq7CategoryGroup
              {
                  Category = key,
                  UnitsInStockGroup = group.GroupBy(unit => unit.UnitsInStock, (key, newGroup)
              => new Linq7UnitsInStockGroup
              {
                  UnitsInStock = key,
                  Prices = newGroup
              .Select(i => i.UnitPrice).OrderBy(i => i)
              })
              }).ToList();

        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            if (products == null)
                throw new ArgumentNullException();

            var resultgroup1 = from product in products
                               where (decimal)product.UnitPrice <= cheap
                               group product by product.UnitPrice into rowGroup
                               select new
                               {
                                   category = (decimal)rowGroup.Key,
                                   products = rowGroup.ToList()
                               };

            var resultgroup2 = from product in products
                               where (decimal)product.UnitPrice > cheap && (decimal)product.UnitPrice <= middle
                               group product by product.UnitPrice into rowGroup
                               select new
                               {
                                   category = (decimal)rowGroup.Key,
                                   products = rowGroup.ToList()
                               };

            var resultgroup3 = from product in products
                               where (decimal)product.UnitPrice > middle && (decimal)product.UnitPrice <= expensive
                               group product by product.UnitPrice into rowGroup
                               select new
                               {
                                   category = (decimal)rowGroup.Key,
                                   products = rowGroup.ToList()
                               };
            //var list = new List<(decimal category, IEnumerable<Product> products)>() { resultgroup1, resultgroup2, resultgroup3 };
            return null;
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            if (customers == null)
                throw new ArgumentNullException();

            //var x =  from customer in customers


            return null;
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            if (suppliers == null)
                throw new ArgumentNullException();

            return suppliers.OrderBy(s => s.Country.Length).ThenBy(s => s.Country)
                .Select(s => s.Country).Distinct().Aggregate((i, j) => i + j);
        }
    }
}