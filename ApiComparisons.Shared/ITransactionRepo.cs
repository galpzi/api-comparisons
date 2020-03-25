using ApiComparisons.Shared.DAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiComparisons.Shared
{
    public interface ITransactionRepo
    {
        #region Queries
        Task<IEnumerable<Person>> GetPeopleAsync();
        Task<IEnumerable<Transaction>> GetTransactionsAsync();
        Task<Person> GetPersonAsync(Guid id);
        Task<Store> GetStoreAsync(Product product);
        Task<Product> GetProductAsync(Purchase purchase);
        Task<IEnumerable<Transaction>> GetTransactionsAsync(Person person);
        Task<IEnumerable<Purchase>> GetPurchasesAsync(Transaction transaction);
        #endregion

        #region Mutations
        Task<Person> AddPersonAsync(string name);
        #endregion
    }
}
