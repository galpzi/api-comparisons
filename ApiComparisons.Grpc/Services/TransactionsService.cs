using ApiComparisons.Shared;
using ApiComparisons.Shared.GRPC;
using ApiComparisons.Shared.GRPC.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc
{
    public class TransactionsService : Transactions.TransactionsBase
    {
        private readonly ITransactionRepo repo;
        private readonly ILogger<TransactionsService> logger;

        public TransactionsService(ILogger<TransactionsService> logger, ITransactionRepo repo)
        {
            this.logger = logger;
            this.repo = repo;
        }

        #region Queries
        public override async Task<PersonResponse> GetPerson(PersonRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Id, out Guid id))
                throw new ArgumentException($"invalid person id {request.Id}");

            var person = await this.repo.GetPersonAsync(id);
            return new PersonResponse
            {
                Person = new Person
                {
                    Name = person.Name,
                    Id = person.ID.ToString(),
                    Created = Timestamp.FromDateTime(person.Created)
                }
            };
        }

        public override async Task<PeopleResponse> GetPeople(Empty request, ServerCallContext context)
        {
            var response = new PeopleResponse();
            var people = await this.repo.GetPeopleAsync();
            var repeated = people.Select(o => new Person
            {
                Name = o.Name,
                Id = o.ID.ToString(),
                Created = Timestamp.FromDateTime(o.Created)
            });
            response.People.AddRange(repeated);
            return response;
        }

        public override async Task<TransactionResponse> GetTransactions(Empty request, ServerCallContext context)
        {
            var response = new TransactionResponse();
            var transactions = await this.repo.GetTransactionsAsync();
            var repeated = transactions.Select(o => new Transaction
            {
                Id = o.ID.ToString(),
                PersonId = o.PersonID.ToString(),
                Total = Convert.ToInt32(o.Total),
                Created = Timestamp.FromDateTime(o.Created)
            });
            response.Transactions.AddRange(repeated);
            return response;
        }

        public override async Task<TransactionResponse> GetPersonTransactions(TransactionRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Person.Id, out Guid id))
                throw new ArgumentException($"invalid person id {request.Person.Id}");

            var response = new TransactionResponse();
            var person = new Shared.DAL.Person
            {
                ID = id,
                Name = request.Person.Name,
                Created = request.Person.Created.ToDateTime()
            };
            var transactions = await this.repo.GetTransactionsAsync(person);
            var repeated = transactions.Select(o => new Transaction
            {
                Id = o.ID.ToString(),
                PersonId = o.PersonID.ToString(),
                Total = Convert.ToInt32(o.Total),
                Created = Timestamp.FromDateTime(o.Created)
            });
            response.Transactions.AddRange(repeated);
            return response;
        }

        public override async Task<PurchaseResponse> GetPurchases(PurchaseRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Transaction.Id, out Guid id))
                throw new ArgumentException($"invalid transaction id {request.Transaction.Id}");

            var response = new PurchaseResponse();
            var transaction = new Shared.DAL.Transaction { ID = id };
            var purchases = await this.repo.GetPurchasesAsync(transaction);
            var repeated = purchases.Select(o => new Purchase
            {
                ProductId = o.ProductID.ToString(),
                TransactionId = o.TransactionID.ToString(),
                Count = o.Count,
                Price = Convert.ToInt32(o.Price),
                Created = Timestamp.FromDateTime(o.Created)
            });
            response.Purchases.AddRange(repeated);
            return response;
        }

        public override async Task<ProductResponse> GetProduct(ProductRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Purchase.ProductId, out Guid productID))
                throw new ArgumentException($"invalid purchase product id {request.Purchase.ProductId}");

            if (!Guid.TryParse(request.Purchase.TransactionId, out Guid transactionID))
                throw new ArgumentException($"invalid purchase transaction id {request.Purchase.TransactionId}");

            var purchase = new Shared.DAL.Purchase { ProductID = productID, TransactionID = transactionID };
            var product = await this.repo.GetProductAsync(purchase);
            return new ProductResponse
            {
                Product = new Product
                {
                    Id = product.ID.ToString(),
                    StoreId = product.StoreID.ToString(),
                    Name = product.Name,
                    Description = product.Description,
                    Price = Convert.ToInt32(product.Price),
                    Created = Timestamp.FromDateTime(product.Created)
                }
            };
        }

        public override async Task<StoreResponse> GetStore(StoreRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Product.Id, out Guid id))
                throw new ArgumentException($"invalid product id {request.Product.Id}");

            if (!Guid.TryParse(request.Product.StoreId, out Guid storeID))
                throw new ArgumentException($"invalid product store id {request.Product.StoreId}");

            var product = new Shared.DAL.Product { ID = id, StoreID = storeID };
            var store = await this.repo.GetStoreAsync(product);
            return new StoreResponse
            {
                Store = new Store
                {
                    Id = store.ID.ToString(),
                    Name = store.Name,
                    Country = store.Country,
                    Address = store.Address,
                    Created = Timestamp.FromDateTime(store.Created)
                }
            };
        }
        #endregion

        #region Mutations
        public override async Task<PersonResponse> AddPerson(PersonRequest request, ServerCallContext context)
        {
            var person = await this.repo.AddPersonAsync(request.Name);
            return new PersonResponse
            {
                Person = new Person
                {
                    Name = person.Name,
                    Id = person.ID.ToString(),
                    Created = Timestamp.FromDateTime(person.Created)
                }
            };
        }

        public override async Task<PersonResponse> RemovePerson(PersonRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Id, out Guid id))
                throw new ArgumentException($"invalid person id {request.Id}");

            var response = new PersonResponse();
            var person = await this.repo.RemovePersonAsync(id);
            if (person != null)
            {
                response.Person = new Person
                {
                    Name = person.Name,
                    Id = person.ID.ToString(),
                    Created = Timestamp.FromDateTime(person.Created)
                };
            }
            return response;
        }

        public override async Task<StoreResponse> AddStore(StoreRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Store.Id, out Guid id))
                throw new ArgumentException($"invalid store id {request.Store.Id}");

            var store = new Shared.DAL.Store
            {
                ID = id,
                Name = request.Store.Name,
                Address = request.Store.Address,
                Country = request.Store.Country,
                Created = request.Store.Created.ToDateTime()
            };
            var entry = await this.repo.AddStoreAsync(store);
            return new StoreResponse
            {
                Store = new Store
                {
                    Id = entry.ID.ToString(),
                    Name = entry.Name,
                    Country = entry.Country,
                    Address = entry.Address,
                    Created = Timestamp.FromDateTime(entry.Created)
                }
            };
        }

        public override async Task<StoreResponse> RemoveStore(StoreRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Store.Id, out Guid id))
                throw new ArgumentException($"invalid store id {request.Store.Id}");

            var response = new StoreResponse();
            var store = await this.repo.RemoveStoreAsync(id);
            if (store != null)
                response.Store = request.Store;

            return response;
        }

        public override async Task<PurchaseResponse> AddPurchase(PurchaseRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Purchase.ProductId, out Guid productID))
                throw new ArgumentException($"invalid product id {request.Purchase.ProductId}");

            if (!Guid.TryParse(request.Purchase.TransactionId, out Guid transactionID))
                throw new ArgumentException($"invalid transaction id {request.Purchase.TransactionId}");

            var purchase = new Shared.DAL.Purchase
            {
                ProductID = productID,
                TransactionID = transactionID,
                Count = request.Purchase.Count,
                Price = request.Purchase.Price,
                Created = request.Purchase.Created.ToDateTime()
            };
            var response = new PurchaseResponse();
            var entry = await this.repo.AddPurchaseAsync(purchase);
            response.Purchases.Add(new Purchase
            {
                ProductId = entry.ProductID.ToString(),
                TransactionId = entry.TransactionID.ToString(),
                Count = entry.Count,
                Price = Convert.ToInt32(entry.Price),
                Created = Timestamp.FromDateTime(entry.Created)
            });
            return response;
        }

        public override async Task<PurchaseResponse> RemovePurchase(PurchaseRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Purchase.ProductId, out Guid productID))
                throw new ArgumentException($"invalid product id {request.Purchase.ProductId}");

            if (!Guid.TryParse(request.Purchase.TransactionId, out Guid transactionID))
                throw new ArgumentException($"invalid transaction id {request.Purchase.TransactionId}");

            var response = new PurchaseResponse();
            var entry = await this.repo.RemovePurchaseAsync(productID, transactionID);
            if (entry != null)
                response.Purchases.Add(request.Purchase);
            return response;
        }

        public override async Task<TransactionResponse> AddTransaction(TransactionRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Transaction.Id, out Guid id))
                throw new ArgumentException($"invalid transaction id {request.Transaction.Id}");

            if (!Guid.TryParse(request.Transaction.PersonId, out Guid personID))
                throw new ArgumentException($"invalid person id {request.Transaction.PersonId}");

            var transaction = new Shared.DAL.Transaction
            {
                ID = id,
                PersonID = personID,
                Total = request.Transaction.Total,
                Created = request.Transaction.Created.ToDateTime()
            };
            var response = new TransactionResponse();
            var entry = await this.repo.AddTransactionAsync(transaction);
            response.Transactions.Add(new Transaction
            {
                Id = entry.ID.ToString(),
                PersonId = entry.PersonID.ToString(),
                Total = Convert.ToInt32(entry.Total),
                Created = Timestamp.FromDateTime(entry.Created)
            });
            return response;
        }

        public override async Task<TransactionResponse> RemoveTransaction(TransactionRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Transaction.Id, out Guid id))
                throw new ArgumentException($"invalid transaction id {request.Transaction.Id}");

            if (!Guid.TryParse(request.Transaction.PersonId, out Guid personID))
                throw new ArgumentException($"invalid person id {request.Transaction.PersonId}");

            var response = new TransactionResponse();
            var transaction = await this.repo.RemoveTransactionAsync(id, personID);
            if (transaction != null)
                response.Transactions.Add(request.Transaction);
            return response;
        }
        #endregion
    }
}
