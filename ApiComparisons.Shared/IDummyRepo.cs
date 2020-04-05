using ApiComparisons.Shared.DAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiComparisons.Shared
{
    public interface IDummyRepo
    {
        #region Queries
        Task<IEnumerable<Store>> GetStoresAsync();
        Task<IEnumerable<Person>> GetPeopleAsync();
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Purchase>> GetPurchasesAsync();
        Task<IEnumerable<Transaction>> GetTransactionsAsync();
        Task<Person> GetPersonAsync(Guid id);
        Task<Store> GetStoreAsync(Product product);
        Task<Product> GetProductAsync(Purchase purchase);
        Task<IEnumerable<Transaction>> GetTransactionsAsync(Person person);
        Task<IEnumerable<Purchase>> GetPurchasesAsync(Transaction transaction);
        #endregion

        #region Mutations
        Task<Store> AddStoreAsync(Store store);
        Task<Store> RemoveStoreAsync(Guid id);
        Task<Person> AddPersonAsync(string name);
        Task<Person> RemovePersonAsync(Guid id);
        Task<Product> AddProductAsync(Product product);
        Task<Product> RemoveProductAsync(Guid id);
        Task<Purchase> AddPurchaseAsync(Purchase purchase);
        Task<Purchase> RemovePurchaseAsync(Guid productID, Guid transactionID);
        Task<Transaction> AddTransactionAsync(Transaction transaction);
        Task<Transaction> RemoveTransactionAsync(Guid id, Guid personID);
        #endregion
    }
}
