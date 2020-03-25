using ApiComparisons.Shared.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ApiComparisons.Shared.Tests
{
    [TestClass()]
    public class ContextInitializerTests
    {
        [TestMethod()]
        public void Seed_ShouldInitializeDatabase()
        {
            var stores = 10;
            var persons = 10;
            var transactions = 10;
            var productsPerStore = 100;
            var purchasesPerTransaction = 10;
            var options = new DbContextOptionsBuilder<TransactionContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new TransactionContext(options);
            var initializer = new ContextInitializer(persons, stores, productsPerStore, purchasesPerTransaction, transactions);
            initializer.Seed(context);

            Assert.AreEqual(persons, context.Persons.Count());
            Assert.AreEqual(stores, context.Stores.Count());
            Assert.AreEqual(productsPerStore, context.Stores.First().Products.Count());
            Assert.AreEqual(productsPerStore, context.Stores.Last().Products.Count());
            Assert.AreEqual(purchasesPerTransaction, context.Transactions.Include(o => o.Purchases).First().Purchases.Count());
            Assert.AreEqual(purchasesPerTransaction, context.Transactions.Include(o => o.Purchases).Last().Purchases.Count());
            Assert.AreEqual(transactions, context.Persons.First().Transactions.Count());
            Assert.AreEqual(transactions, context.Persons.Last().Transactions.Count());
        }
    }
}