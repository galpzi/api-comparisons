using ApiComparisons.Shared.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiComparisons.Shared
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly TransactionContext context;
        private readonly ILogger<TransactionRepo> logger;

        public TransactionRepo(ILogger<TransactionRepo> logger, TransactionContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task<Person> AddPersonAsync(string name)
        {
            var person = await this.context.Persons.AddAsync(new Person { Name = name, Created = DateTime.Now });
            await this.context.SaveChangesAsync();
            return person.Entity;
        }

        public async Task<IEnumerable<Person>> GetPeopleAsync()
        {
            return await this.context.Persons.ToListAsync();
        }

        public async Task<Store> GetStoreAsync(Product product)
        {
            return await this.context.Stores.FindAsync(product.StoreID);
        }

        public async Task<Person> GetPersonAsync(Guid id)
        {
            return await this.context.Persons.FindAsync(id);
        }

        public async Task<Product> GetProductAsync(Purchase purchase)
        {
            return await this.context.Products.FindAsync(purchase.ProductID);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            return await this.context.Transactions.ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync(Person person)
        {
            return await this.context.Transactions
                .Where(o => o.PersonID == person.ID)
                .ToListAsync();
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesAsync(Transaction transaction)
        {
            return await this.context.Purchases
                .Where(o => o.TransactionID == transaction.ID)
                .ToListAsync();
        }
    }
}
