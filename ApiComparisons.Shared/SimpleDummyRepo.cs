using ApiComparisons.Shared.DAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiComparisons.Shared
{
    public class SimpleDummyRepo : IDummyRepo
    {
        public Task<Person> AddPersonAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Product> AddProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<Purchase> AddPurchaseAsync(Purchase purchase)
        {
            throw new NotImplementedException();
        }

        public Task<Store> AddStoreAsync(Store store)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> GetPeopleAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetPersonAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductAsync(Purchase purchase)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Purchase>> GetPurchasesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Purchase>> GetPurchasesAsync(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task<Store> GetStoreAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Store>> GetStoresAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GetTransactionsAsync(Person person)
        {
            throw new NotImplementedException();
        }

        public Task<Person> RemovePersonAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> RemoveProductAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Purchase> RemovePurchaseAsync(Guid productID, Guid transactionID)
        {
            throw new NotImplementedException();
        }

        public Task<Store> RemoveStoreAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> RemoveTransactionAsync(Guid id, Guid personID)
        {
            throw new NotImplementedException();
        }
    }
}
