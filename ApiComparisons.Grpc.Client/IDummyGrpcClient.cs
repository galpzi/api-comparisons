using ApiComparisons.Shared.DAL;
using System;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc.Client
{
    public interface IDummyGrpcClient
    {
        Task AddPersonAsync(Person person);
        Task GetPeopleAsync(Guid personID);
        Task RemovePersonAsync(Person person);
        Task GetStoresAsync(Product product);
        Task AddStoreAsync(Store store);
        Task RemoveStoreAsync(Store store);
        Task GetProductAsync(Purchase purchase);
        Task AddProductAsync(Product product);
        Task RemoveProductAsync(Product product);
        Task GetPurchasesAsync(Transaction transaction);
        Task AddPurchaseAsync(Purchase purchase);
        Task RemovePurchaseAsync(Purchase purchase);
        Task GetTransactionsAsync(Guid personID);
        Task AddTransactionAsync(Transaction transaction);
        Task RemoveTransactionAsync(Transaction transaction);
    }
}
