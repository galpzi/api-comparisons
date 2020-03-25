using ApiComparisons.Shared.DAL;
using System;
using System.Linq;

namespace ApiComparisons.Shared
{
    public class ContextInitializer : IContextInitializer
    {
        private readonly int persons;
        private readonly int stores;
        private readonly int products;
        private readonly int purchases;
        private readonly int transactions;

        public ContextInitializer(int persons, int stores, int products, int purchases, int transactions)
        {
            this.persons = persons;
            this.stores = stores;
            this.products = products;
            this.purchases = purchases;
            this.transactions = transactions;
        }

        public void Seed(TransactionContext context)
        {
            var random = new Random();
            foreach (var number in Enumerable.Range(1, this.persons))
            {
                context.Persons.Add(new Person
                {
                    Created = DateTime.Now,
                    Name = $"Person {number}",
                });
            }
            context.SaveChanges();

            foreach (var number in Enumerable.Range(1, this.stores))
            {
                context.Stores.Add(new Store
                {
                    Created = DateTime.Now,
                    Name = $"Store {number}",
                    Address = Guid.NewGuid().ToString(),
                    Country = Guid.NewGuid().ToString()
                });
            }
            context.SaveChanges();

            foreach (var store in context.Stores)
            {
                foreach (var number in Enumerable.Range(1, this.products))
                {
                    context.Products.Add(new Product
                    {
                        StoreID = store.ID,
                        Created = DateTime.Now,
                        Name = $"Product {number}",
                        Description = Guid.NewGuid().ToString(),
                        Price = random.Next(1, 1000)
                    });
                }
            }
            context.SaveChanges();

            foreach (var person in context.Persons)
            {
                foreach (var i in Enumerable.Range(1, this.transactions))
                {
                    var transaction = context.Transactions.Add(new Transaction
                    {
                        ID = Guid.NewGuid(),
                        PersonID = person.ID,
                        Created = DateTime.Now
                    }).Entity;
                    context.SaveChanges();

                    var randomProducts = context.Products.Local.OrderBy(o => Guid.NewGuid()).Take(this.purchases);
                    foreach (var product in randomProducts)
                    {
                        int count = random.Next(1, 10);
                        context.Purchases.Add(new Purchase
                        {
                            Count = count,
                            ProductID = product.ID,
                            Created = DateTime.Now,
                            Price = count * product.Price,
                            TransactionID = transaction.ID
                        });
                    }
                    context.SaveChanges();

                    var purchases = context.Purchases.Where(o => o.TransactionID == transaction.ID).ToList();
                    transaction.Purchases = purchases;
                    transaction.Total = purchases.Sum(o => o.Price);
                    context.SaveChanges();
                }
            }
        }
    }
}
