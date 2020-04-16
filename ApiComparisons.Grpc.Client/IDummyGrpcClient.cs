using ApiComparisons.Shared.GRPC.Models;
using System;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc.Client
{
    public interface IDummyGrpcClient
    {
        Task<PersonResponse> AddPersonAsync(Shared.DAL.Person person);
        Task<PersonResponse> GetPeopleAsync(Guid personID);
        Task<PersonResponse> RemovePersonAsync(Shared.DAL.Person person);
        Task<StoreResponse> GetStoresAsync(Shared.DAL.Product product);
        Task<StoreResponse> AddStoreAsync(Shared.DAL.Store store);
        Task<StoreResponse> RemoveStoreAsync(Shared.DAL.Store store);
        Task<ProductResponse> GetProductAsync(Shared.DAL.Purchase purchase);
        Task<ProductResponse> AddProductAsync(Shared.DAL.Product product);
        Task<ProductResponse> RemoveProductAsync(Shared.DAL.Product product);
        Task<PurchaseResponse> GetPurchasesAsync(Shared.DAL.Transaction transaction);
        Task<PurchaseResponse> AddPurchaseAsync(Shared.DAL.Purchase purchase);
        Task<PurchaseResponse> RemovePurchaseAsync(Shared.DAL.Purchase purchase);
        Task<TransactionResponse> GetTransactionsAsync(Guid personID);
        Task<TransactionResponse> AddTransactionAsync(Shared.DAL.Transaction transaction);
        Task<TransactionResponse> RemoveTransactionAsync(Shared.DAL.Transaction transaction);
    }
}
