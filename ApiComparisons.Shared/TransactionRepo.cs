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

        #region Queries        
        public async Task<IEnumerable<Person>> GetPeopleAsync() => await this.context.Persons.ToListAsync();

        public async Task<IEnumerable<Store>> GetStoresAsync() => await this.context.Stores.ToListAsync();

        public async Task<IEnumerable<Product>> GetProductsAsync() => await this.context.Products.ToListAsync();

        public async Task<IEnumerable<Purchase>> GetPurchasesAsync() => await this.context.Purchases.ToListAsync();

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync() => await this.context.Transactions.ToListAsync();

        public async Task<Person> GetPersonAsync(Guid id) => await this.context.Persons.FindAsync(id);

        public async Task<Store> GetStoreAsync(Product product) => await this.context.Stores.FindAsync(product.StoreID);

        public async Task<Product> GetProductAsync(Purchase purchase) => await this.context.Products.FindAsync(purchase.ProductID);

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync(Person person) =>
            await this.context.Transactions
                .Where(o => o.PersonID == person.ID)
                .ToListAsync();

        public async Task<IEnumerable<Purchase>> GetPurchasesAsync(Transaction transaction) =>
            await this.context.Purchases
                .Where(o => o.TransactionID == transaction.ID)
                .ToListAsync();
        #endregion

        #region Mutations
        public async Task<Store> AddStoreAsync(Store store)
        {
            this.context.Entry(store).State = store.ID == Guid.Empty ? EntityState.Added : EntityState.Modified;
            await this.context.SaveChangesAsync();
            return store;
        }

        public async Task<Store> RemoveStoreAsync(Guid id)
        {
            var store = await this.context.Stores.FindAsync(id);
            if (store != null)
            {
                this.context.Remove(store);
                await this.context.SaveChangesAsync();
            }
            return store;
        }

        public async Task<Person> AddPersonAsync(string name)
        {
            var person = await this.context.Persons.AddAsync(new Person { Name = name, Created = DateTime.UtcNow });
            await this.context.SaveChangesAsync();
            return person.Entity;
        }

        public async Task<Person> RemovePersonAsync(Guid id)
        {
            var person = await this.context.Persons.FindAsync(id);
            if (person != null)
            {
                this.context.Persons.Remove(person);
                await this.context.SaveChangesAsync();
            }
            return person;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            this.context.Entry(product).State = product.ID == Guid.Empty ? EntityState.Added : EntityState.Modified;
            await this.context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> RemoveProductAsync(Guid id)
        {
            var product = await this.context.Products.FindAsync(id);
            if (product != null)
            {
                this.context.Remove(product);
                await this.context.SaveChangesAsync();
            }
            return product;
        }

        public async Task<Purchase> AddPurchaseAsync(Purchase purchase)
        {
            var added = purchase.ProductID == Guid.Empty && purchase.TransactionID == Guid.Empty;
            this.context.Entry(purchase).State = added ? EntityState.Added : EntityState.Modified;
            await this.context.SaveChangesAsync();
            return purchase;
        }

        public async Task<Purchase> RemovePurchaseAsync(Guid purchaseID, Guid transactionID)
        {
            var purchase = await this.context.Purchases.FindAsync(purchaseID, transactionID);
            if (purchase != null)
            {
                this.context.Purchases.Remove(purchase);
                await this.context.SaveChangesAsync();
            }
            return purchase;
        }

        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            var added = transaction.ID == Guid.Empty && transaction.PersonID == Guid.Empty;
            this.context.Entry(transaction).State = added ? EntityState.Added : EntityState.Modified;
            await this.context.SaveChangesAsync();
            return transaction;
        }

        public async Task<Transaction> RemoveTransactionAsync(Guid id, Guid personID)
        {
            var transaction = await this.context.Transactions.FindAsync(id, personID);
            if (transaction != null)
            {
                this.context.Transactions.Remove(transaction);
                await this.context.SaveChangesAsync();
            }
            return transaction;
        }
        #endregion
    }
}
