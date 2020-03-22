using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiComparisons.Shared.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ApiComparisons.Shared.DAL.Tests
{
    [TestClass()]
    public class TransactionContextTests
    {
        [TestMethod()]
        public void TransactionContext_ShouldBuildWithoutErrors()
        {
            string connectionString = $"Server=DESKTOP-6AQTT80;Database=TransactionContextTest;Trusted_Connection=True";
            var options = new DbContextOptionsBuilder<TransactionContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new TransactionContext(options);
            context.Database.EnsureCreated();
            context.Database.EnsureDeleted();
        }
    }
}