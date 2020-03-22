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
        Task<Person> GetPersonAsync(Guid id);
        Task<IEnumerable<Transaction>> GetTransactionsAsync();
        Task<IEnumerable<Transaction>> GetTransactionsAsync(Person person);
        #endregion

        #region Mutations
        Task<Person> AddPersonAsync(string name);
        #endregion
    }
}
