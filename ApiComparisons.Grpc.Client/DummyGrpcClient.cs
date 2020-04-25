using ApiComparisons.Shared.GRPC;
using ApiComparisons.Shared.GRPC.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc.Client
{
    public class DummyGrpcClient : GrpcClient, IDummyGrpcClient
    {
        private readonly Metadata headers;

        public DummyGrpcClient(IOptions<AppSettings> options) : base(options)
        {
            this.headers = new Metadata
            {
                { "grpc-internal-encoding-request", "gzip" }
            };
        }

        public Dummy.DummyClient Client => new Dummy.DummyClient(this.channel);

        #region People
        public async Task<PersonResponse> GetPeopleAsync(Guid personID) =>
            await Client.GetPeopleAsync(new PersonRequest { Id = personID.ToString() }, headers: this.headers);

        public async Task<PersonResponse> AddPersonAsync(Shared.DAL.Person person) =>
            await Client.AddPersonAsync(new PersonRequest { Name = person.Name });

        public async Task<PersonResponse> RemovePersonAsync(Shared.DAL.Person person) =>
            await Client.RemovePersonAsync(new PersonRequest { Id = person.ID.ToString() });
        #endregion

        #region Stores
        public async Task<StoreResponse> GetStoresAsync(Shared.DAL.Product product) =>
            await Client.GetStoreAsync(new StoreRequest
            {
                Product = new Product
                {
                    Id = product.ID.ToString(),
                    StoreId = product.StoreID.ToString()
                }
            });

        public async Task<StoreResponse> AddStoreAsync(Shared.DAL.Store store) =>
            await Client.AddStoreAsync(new StoreRequest
            {
                Store = new Store
                {
                    Name = store.Name,
                    Id = store.ID.ToString(),
                    Address = store.Address,
                    Country = store.Country,
                    Created = Timestamp.FromDateTime(store.Created)
                }
            });

        public async Task<StoreResponse> RemoveStoreAsync(Shared.DAL.Store store) =>
            await Client.RemoveStoreAsync(new StoreRequest
            {
                Store = new Store
                {
                    Name = store.Name,
                    Id = store.ID.ToString(),
                    Address = store.Address,
                    Country = store.Country,
                    Created = Timestamp.FromDateTime(store.Created)
                }
            });
        #endregion

        #region Products
        public async Task<ProductResponse> GetProductsAsync(Shared.DAL.Purchase purchase) =>
            await Client.GetProductAsync(new ProductRequest
            {
                Purchase = new Purchase
                {
                    ProductId = purchase.ProductID.ToString(),
                    TransactionId = purchase.TransactionID.ToString()
                }
            });

        public async Task<ProductResponse> AddProductAsync(Shared.DAL.Product product) => throw new NotImplementedException();

        public async Task<ProductResponse> RemoveProductAsync(Shared.DAL.Product product) => throw new NotImplementedException();
        #endregion

        #region Purchases        
        public async Task<PurchaseResponse> GetPurchasesAsync(Shared.DAL.Transaction transaction) =>
            await Client.GetPurchasesAsync(new PurchaseRequest
            {
                Transaction = new Transaction
                {
                    Id = transaction.ID.ToString(),
                    PersonId = transaction.PersonID.ToString()
                }
            });

        public async Task<PurchaseResponse> AddPurchaseAsync(Shared.DAL.Purchase purchase) =>
            await Client.AddPurchaseAsync(new PurchaseRequest
            {
                Purchase = new Purchase
                {
                    ProductId = purchase.ProductID.ToString(),
                    TransactionId = purchase.TransactionID.ToString(),
                    Price = Convert.ToInt32(purchase.Price),
                    Count = purchase.Count
                }
            });

        public async Task<PurchaseResponse> RemovePurchaseAsync(Shared.DAL.Purchase purchase) =>
            await Client.RemovePurchaseAsync(new PurchaseRequest
            {
                Purchase = new Purchase
                {
                    ProductId = purchase.ProductID.ToString(),
                    TransactionId = purchase.TransactionID.ToString()
                }
            });
        #endregion

        #region Transactions      
        public async Task<TransactionResponse> GetTransactionsAsync(Guid personID) =>
            await Client.GetTransactionsAsync(new TransactionRequest
            {
                Person = new Person { Id = personID.ToString() }
            });

        public async Task<TransactionResponse> AddTransactionAsync(Shared.DAL.Transaction transaction) =>
            await Client.AddTransactionAsync(new TransactionRequest
            {
                Transaction = new Transaction
                {
                    PersonId = transaction.PersonID.ToString(),
                    Total = Convert.ToInt32(transaction.Total)
                }
            });

        public async Task<TransactionResponse> RemoveTransactionAsync(Shared.DAL.Transaction transaction) =>
            await Client.RemoveTransactionAsync(new TransactionRequest
            {
                Transaction = new Transaction
                {
                    Id = transaction.ID.ToString(),
                    PersonId = transaction.PersonID.ToString()
                }
            });
        #endregion
    }
}
