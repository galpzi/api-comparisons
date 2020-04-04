using ApiComparisons.Shared.DAL;
using ApiComparisons.Shared.GRPC;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc.Client
{
    public class DummyGrpcClient : GrpcClient, IDummyGrpcClient
    {
        public DummyGrpcClient(IOptions<AppSettings> options) : base(options)
        {
        }

        public Transactions.TransactionsClient Client => new Transactions.TransactionsClient(this.channel);

        internal static void Print(dynamic response) =>
            Console.WriteLine((string)JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }));

        #region People
        public async Task GetPeopleAsync(Guid personID)
        {
            dynamic response;
            if (personID == Guid.Empty)
                response = await Client.GetPeopleAsync(new Empty());
            else
                response = await Client.GetPersonAsync(new Shared.GRPC.Models.PersonRequest { Id = personID.ToString() });
            Print(response);
        }

        public async Task AddPersonAsync(Person person)
        {
            var response = await Client.AddPersonAsync(new Shared.GRPC.Models.PersonRequest { Name = person.Name });
            Print(response);
        }

        public async Task RemovePersonAsync(Person person)
        {
            var response = await Client.RemovePersonAsync(new Shared.GRPC.Models.PersonRequest { Id = person.ID.ToString() });
            Print(response);
        }
        #endregion

        #region Stores
        public async Task GetStoresAsync(Product product)
        {
            var response = await Client.GetStoreAsync(new Shared.GRPC.Models.StoreRequest
            {
                Product = new Shared.GRPC.Models.Product
                {
                    Id = product.ID.ToString(),
                    StoreId = product.StoreID.ToString()
                }
            });
            Print(response);
        }

        public async Task AddStoreAsync(Store store)
        {
            var response = await Client.AddStoreAsync(new Shared.GRPC.Models.StoreRequest
            {
                Store = new Shared.GRPC.Models.Store
                {
                    Name = store.Name,
                    Id = store.ID.ToString(),
                    Address = store.Address,
                    Country = store.Country,
                    Created = Timestamp.FromDateTime(store.Created)
                }
            });
            Print(response);
        }

        public async Task RemoveStoreAsync(Store store)
        {
            var response = await Client.RemoveStoreAsync(new Shared.GRPC.Models.StoreRequest
            {
                Store = new Shared.GRPC.Models.Store
                {
                    Name = store.Name,
                    Id = store.ID.ToString(),
                    Address = store.Address,
                    Country = store.Country,
                    Created = Timestamp.FromDateTime(store.Created)
                }
            });
            Print(response);
        }
        #endregion

        #region Products
        public async Task GetProductAsync(Purchase purchase)
        {
            var response = await Client.GetProductAsync(new Shared.GRPC.Models.ProductRequest
            {
                Purchase = new Shared.GRPC.Models.Purchase
                {
                    ProductId = purchase.ProductID.ToString(),
                    TransactionId = purchase.TransactionID.ToString()
                }
            });
            Print(response);
        }

        public async Task AddProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Purchases        
        public async Task GetPurchasesAsync(Transaction transaction)
        {
            dynamic response;
            response = await Client.GetPurchasesAsync(new Shared.GRPC.Models.PurchaseRequest
            {
                Transaction = new Shared.GRPC.Models.Transaction
                {
                    Id = transaction.ID.ToString(),
                    PersonId = transaction.PersonID.ToString()
                }
            });
            Print(response);
        }

        public async Task AddPurchaseAsync(Purchase purchase)
        {
            var response = await Client.AddPurchaseAsync(new Shared.GRPC.Models.PurchaseRequest
            {
                Purchase = new Shared.GRPC.Models.Purchase
                {
                    ProductId = purchase.ProductID.ToString(),
                    TransactionId = purchase.TransactionID.ToString(),
                    Price = Convert.ToInt32(purchase.Price),
                    Count = purchase.Count
                }
            });
            Print(response);
        }

        public async Task RemovePurchaseAsync(Purchase purchase)
        {
            var response = await Client.RemovePurchaseAsync(new Shared.GRPC.Models.PurchaseRequest
            {
                Purchase = new Shared.GRPC.Models.Purchase
                {
                    ProductId = purchase.ProductID.ToString(),
                    TransactionId = purchase.TransactionID.ToString()
                }
            });
            Print(response);
        }
        #endregion

        #region Transactions      
        public async Task GetTransactionsAsync(Guid personID)
        {
            dynamic response;
            if (personID == Guid.Empty)
            {
                response = await Client.GetTransactionsAsync(new Empty());
            }
            else
            {
                response = await Client.GetPersonTransactionsAsync(new Shared.GRPC.Models.TransactionRequest
                {
                    Person = new Shared.GRPC.Models.Person { Id = personID.ToString() }
                });
            }
            Print(response);
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            var response = await Client.AddTransactionAsync(new Shared.GRPC.Models.TransactionRequest
            {
                Transaction = new Shared.GRPC.Models.Transaction
                {
                    PersonId = transaction.PersonID.ToString(),
                    Total = Convert.ToInt32(transaction.Total)
                }
            });
            Print(response);
        }

        public async Task RemoveTransactionAsync(Transaction transaction)
        {
            var response = await Client.RemoveTransactionAsync(new Shared.GRPC.Models.TransactionRequest
            {
                Transaction = new Shared.GRPC.Models.Transaction
                {
                    Id = transaction.ID.ToString(),
                    PersonId = transaction.PersonID.ToString()
                }
            });
            Print(response);
        }
        #endregion
    }
}
