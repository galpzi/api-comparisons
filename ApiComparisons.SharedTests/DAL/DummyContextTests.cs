using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiComparisons.Shared.DAL.Tests
{
    [TestClass()]
    public class DummyContextTests
    {
        [TestMethod()]
        public void DummyContext_ShouldBuildWithoutErrors()
        {
            string connectionString = $"Server=DESKTOP-6AQTT80;Database=TransactionContextTest;Trusted_Connection=True";
            var options = new DbContextOptionsBuilder<DummyContext>()
                .UseSqlServer(connectionString)
                .Options;

            using var context = new DummyContext(options);
            context.Database.EnsureCreated();
            context.Database.EnsureDeleted();
        }
    }
}