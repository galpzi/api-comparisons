using ApiComparisons.Shared.DAL;
using ApiComparisons.Shared.GRPC;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc.Client
{
    public class TransactionCommandBuilder
    {
        public RootCommand Root()
        {
            var root = new RootCommand("CLI client for the ApiComparisons GRPC server.")
            {
                new Command("dummy", "dummy data GRPC service commands.")
                {
                    GetPeopleCommand(),
                    GetStoresCommand(),
                    GetProductsCommand(),
                    GetPurchasesCommand(),
                    GetTransactionsCommand()
                }
            };
            root.TreatUnmatchedTokensAsErrors = true;
            return root;
        }

        internal static void Print(dynamic response) =>
            Console.WriteLine((string)JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }));

        #region People
        private Command GetPeopleCommand()
        {
            var people = new Command("people", "Return, add or remove people.")
            {
                new Option<Guid>("--person-id", "The persons's ID. The default is empty to indicate no ID specified.")
            };
            people.Handler = CommandHandler.Create<IHost, Guid>(GetPeople);
            var add = new Command("add", "Add a person.") { new Option<string>("--name", "The name of the person to add.") { Required = true } };
            add.Handler = CommandHandler.Create<IHost, Person>(AddPerson);
            var remove = new Command("remove", "Remove a person.") { new Option<Guid>("--id", "The ID of the person to remove.") { Required = true } };
            remove.Handler = CommandHandler.Create<IHost, Person>(RemovePerson);
            people.Add(add);
            people.Add(remove);
            return people;
        }

        private async Task GetPeople(IHost host, Guid personID)
        {
            dynamic response;
            var options = host.Services.GetRequiredService<IOptions<AppSettings>>();
            using var channel = GrpcChannel.ForAddress(options.Value.ServerUri);
            var client = new Transactions.TransactionsClient(channel);
            if (personID == Guid.Empty)
                response = await client.GetPeopleAsync(new Empty());
            else
                response = await client.GetPersonAsync(new Shared.GRPC.Models.PersonRequest { Id = personID.ToString() });
            Print(response);
        }

        private async Task AddPerson(IHost host, Person person)
        {
            var options = host.Services.GetRequiredService<IOptions<AppSettings>>();
            using var channel = GrpcChannel.ForAddress(options.Value.ServerUri);
            var client = new Transactions.TransactionsClient(channel);
            var response = await client.AddPersonAsync(new Shared.GRPC.Models.PersonRequest { Name = person.Name });
            Print(response);
        }

        private async Task RemovePerson(IHost host, Person person)
        {
            var options = host.Services.GetRequiredService<IOptions<AppSettings>>();
            using var channel = GrpcChannel.ForAddress(options.Value.ServerUri);
            var client = new Transactions.TransactionsClient(channel);
            var response = await client.RemovePersonAsync(new Shared.GRPC.Models.PersonRequest { Id = person.ID.ToString() });
            Print(response);
        }
        #endregion

        #region Stores 
        private Command GetStoresCommand()
        {
            var stores = new Command("stores", "Return, add or remove stores.")
            {
                new Option<Guid>("--product-id", "The store product ID.") { Required = true },
                new Option<Guid>("--store-id", "The store ID.") { Required = true }
            };
            stores.Handler = CommandHandler.Create<IHost, Product>(GetStores);
            var add = new Command("add", "Add a store.") { new Option<Store>("--store", "The store to add.") { Required = true } };
            add.Handler = CommandHandler.Create<IHost, Store>(AddStore);
            var remove = new Command("remove", "Remove a store.") { new Option<Store>("--store", "The store to remove.") { Required = true } };
            remove.Handler = CommandHandler.Create<IHost, Store>(RemoveStore);
            stores.Add(add);
            stores.Add(remove);
            return stores;
        }

        private async Task GetStores(IHost host, Product product)
        {
            var options = host.Services.GetRequiredService<IOptions<AppSettings>>();
            using var channel = GrpcChannel.ForAddress(options.Value.ServerUri);
            var client = new Transactions.TransactionsClient(channel);
            var response = await client.GetStoreAsync(new Shared.GRPC.Models.StoreRequest
            {
                Product = new Shared.GRPC.Models.Product
                {
                    Id = product.ID.ToString(),
                    StoreId = product.StoreID.ToString()
                }
            });
            Print(response);
        }

        private async Task AddStore(IHost host, Store store)
        {
            var options = host.Services.GetRequiredService<IOptions<AppSettings>>();
            using var channel = GrpcChannel.ForAddress(options.Value.ServerUri);
            var client = new Transactions.TransactionsClient(channel);
            var response = await client.AddStoreAsync(new Shared.GRPC.Models.StoreRequest
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

        private async Task RemoveStore(IHost host, Store store)
        {
            var options = host.Services.GetRequiredService<IOptions<AppSettings>>();
            using var channel = GrpcChannel.ForAddress(options.Value.ServerUri);
            var client = new Transactions.TransactionsClient(channel);
            var response = await client.RemoveStoreAsync(new Shared.GRPC.Models.StoreRequest
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
        private Command GetProductsCommand()
        {
            var products = new Command("products", "Return, add or remove products.")
            {
                new Option<Guid>("--product-id", "The purchase product ID.") { Required = true },
                new Option<Guid>("--transaction-id", "The purchase transaction ID.") { Required = true }
            };
            products.Handler = CommandHandler.Create<IHost, Purchase>(GetProduct);
            // TODO: implement add/remove methods
            //var add = new Command("add", "Add a product.") { new Option<Product>("--product", "The product to add.") { Required = true } };
            //add.Handler = CommandHandler.Create<IHost, Product>(AddProduct);
            //var remove = new Command("remove", "Remove a product.") { new Option<Product>("--product", "The product to remove.") { Required = true } };
            //remove.Handler = CommandHandler.Create<IHost, Product>(RemoveProduct);
            //products.Add(add);
            //products.Add(remove);
            return products;
        }

        private async Task GetProduct(IHost host, Purchase purchase)
        {
            dynamic response;
            var options = host.Services.GetRequiredService<IOptions<AppSettings>>();
            using var channel = GrpcChannel.ForAddress(options.Value.ServerUri);
            var client = new Transactions.TransactionsClient(channel);
            response = await client.GetProductAsync(new Shared.GRPC.Models.ProductRequest
            {
                Purchase = new Shared.GRPC.Models.Purchase
                {
                    ProductId = purchase.ProductID.ToString(),
                    TransactionId = purchase.TransactionID.ToString()
                }
            });
            Print(response);
        }

        private async Task AddProduct(IHost host, Product product)
        {
            throw new NotImplementedException();
        }

        private async Task RemoveProduct(IHost host, Product product)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Purchases
        private Command GetPurchasesCommand()
        {
            var purchases = new Command("purchases", "Return, add or remove purchases")
            {
                new Option<Guid>("--transaction-id", "The transaction ID.") { Required = true },
                new Option<Guid>("--person-id", "The transaction person ID.") { Required = true }
            };
            purchases.Handler = CommandHandler.Create<IHost, Transaction>(GetPurchases);
            var add = new Command("add", "Add a purchase.")
            {
                new Option<Guid>("--purchase-id", "The purchase ID.") { Required = true },
                new Option<Guid>("--transaction-id", "The transaction ID.") { Required = true },
                new Option<decimal>("--price", "The purchase price.") { Required = true },
                new Option<int>("--count", "The purchase count.") { Required = true },
            };
            add.Handler = CommandHandler.Create<IHost, Purchase>(AddPurchase);
            var remove = new Command("remove", "Remove a purchase.")
            {
                new Option<Guid>("--purchase-id", "The purchase ID.") { Required = true },
                new Option<Guid>("--transaction-id", "The transaction ID.") { Required = true },
            };
            remove.Handler = CommandHandler.Create<IHost, Purchase>(RemovePurchase);
            purchases.Add(add);
            purchases.Add(remove);
            return purchases;
        }

        private async Task GetPurchases(IHost host, Transaction transaction)
        {
            dynamic response;
            var options = host.Services.GetRequiredService<IOptions<AppSettings>>();
            using var channel = GrpcChannel.ForAddress(options.Value.ServerUri);
            var client = new Transactions.TransactionsClient(channel);
            response = await client.GetPurchasesAsync(new Shared.GRPC.Models.PurchaseRequest
            {
                Transaction = new Shared.GRPC.Models.Transaction
                {
                    Id = transaction.ID.ToString(),
                    PersonId = transaction.PersonID.ToString()
                }
            });
            Print(response);
        }

        private async Task AddPurchase(IHost host, Purchase purchase)
        {
            dynamic response;
            var options = host.Services.GetRequiredService<IOptions<AppSettings>>();
            using var channel = GrpcChannel.ForAddress(options.Value.ServerUri);
            var client = new Transactions.TransactionsClient(channel);
            response = await client.AddPurchaseAsync(new Shared.GRPC.Models.PurchaseRequest
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

        private async Task RemovePurchase(IHost host, Purchase purchase)
        {
            dynamic response;
            var options = host.Services.GetRequiredService<IOptions<AppSettings>>();
            using var channel = GrpcChannel.ForAddress(options.Value.ServerUri);
            var client = new Transactions.TransactionsClient(channel);
            response = await client.RemovePurchaseAsync(new Shared.GRPC.Models.PurchaseRequest
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
        private Command GetTransactionsCommand()
        {
            var transactions = new Command("transactions", "Return, add or remove transactions")
            {
                new Option<Guid>("--person-id", "The transaction person ID. The default is null to indicate no person specified.")
            };
            transactions.Handler = CommandHandler.Create<IHost, Guid>(GetTransactions);
            var add = new Command("add", "Add a transaction.")
            {
                new Option<Guid>("--person-id", "The transaction person ID.") { Required = true },
                new Option<decimal>("--total", "The transaction total.") { Required = true }
            };
            add.Handler = CommandHandler.Create<IHost, Transaction>(AddTransaction);
            var remove = new Command("remove", "Remove a transaction.")
            {
                new Option<Guid>("--id", "The transaction ID.") { Required = true },
                new Option<Guid>("--person-id", "The transaction person ID.") { Required = true },
            };
            remove.Handler = CommandHandler.Create<IHost, Transaction>(RemoveTransaction);
            transactions.Add(add);
            transactions.Add(remove);
            return transactions;
        }

        private async Task GetTransactions(IHost host, Guid personID)
        {
            dynamic response;
            var options = host.Services.GetRequiredService<IOptions<AppSettings>>();
            using var channel = GrpcChannel.ForAddress(options.Value.ServerUri);
            var client = new Transactions.TransactionsClient(channel);
            if (personID == Guid.Empty)
            {
                response = await client.GetTransactionsAsync(new Empty());
            }
            else
            {
                response = await client.GetPersonTransactionsAsync(new Shared.GRPC.Models.TransactionRequest
                {
                    Person = new Shared.GRPC.Models.Person { Id = personID.ToString() }
                });
            }
            Print(response);
        }

        private async Task AddTransaction(IHost host, Transaction transaction)
        {
            dynamic response;
            var options = host.Services.GetRequiredService<IOptions<AppSettings>>();
            using var channel = GrpcChannel.ForAddress(options.Value.ServerUri);
            var client = new Transactions.TransactionsClient(channel);
            response = await client.AddTransactionAsync(new Shared.GRPC.Models.TransactionRequest
            {
                Transaction = new Shared.GRPC.Models.Transaction
                {
                    PersonId = transaction.PersonID.ToString(),
                    Total = Convert.ToInt32(transaction.Total)
                }
            });
            Print(response);
        }

        private async Task RemoveTransaction(IHost host, Transaction transaction)
        {
            dynamic response;
            var options = host.Services.GetRequiredService<IOptions<AppSettings>>();
            using var channel = GrpcChannel.ForAddress(options.Value.ServerUri);
            var client = new Transactions.TransactionsClient(channel);
            response = await client.RemoveTransactionAsync(new Shared.GRPC.Models.TransactionRequest
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
