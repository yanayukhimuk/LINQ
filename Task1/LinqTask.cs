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
            var query = from customer in customers
                        let total = GetTotalExpenses(customer)
                        where total > limit
                        select customer;
            return query.ToList();
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            var result = new List<(Customer customer, DateTime dateOfEntry)>();
            if (customers == null)
                throw new ArgumentNullException();
            else
            {
                foreach (var customer in customers)
                    if (customer.Orders != null && customer.Orders.Count() > 0)
                    {
                        var orders = new List<Order>();
                        foreach (var order in customer.Orders)
                        {
                            orders.Add(order);
                        }
                        var minOrder = orders.OrderBy(o => o.OrderDate).FirstOrDefault().OrderDate;
                        result.Insert(0, (customer, minOrder));
                    }
                    else
                        continue;
            }
            return result;
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            var result = new List<(Customer customer, DateTime dateOfEntry)>();
            if (customers == null)
                throw new ArgumentNullException();
            else
            {
                foreach (var customer in customers)
                    if (customer.Orders != null && customer.Orders.Count() > 0)
                    {
                        var orders = new List<Order>();
                        foreach (var order in customer.Orders)
                        {
                            orders.Add(order);
                        }
                        var minOrder = orders.OrderBy(o => o.OrderDate).FirstOrDefault().OrderDate;
                        result.Insert(0, (customer, minOrder));
                    }
                    else
                        continue;
            }
            
            var orderedResult = from res in result
                                let total = GetTotalExpenses(res.customer)
                                orderby res.dateOfEntry.Year
                                orderby res.dateOfEntry.Month
                                orderby total descending
                                orderby res.customer.CompanyName
                                select res;

            return orderedResult;
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            if (customers == null)
                throw new ArgumentNullException();

            var result = new List<Customer>();
            foreach (var customer in customers)
            {
                var postalCode = customer.PostalCode;
                var phone = customer.Phone;
                var region = customer.Region;
                int p = 0;

                if (int.TryParse(postalCode, out p) == false || region == null || phone.StartsWith("(") == false)
                {
                    result.Add(customer);
                }
                else
                    continue;
            }
            return result;
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */

            throw new NotImplementedException();
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            throw new NotImplementedException();
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            throw new NotImplementedException();
        }
    }
}