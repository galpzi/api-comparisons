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
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc.Client
{
    public static class TransactionCommands
    {
        public static RootCommand Root()
        {
            var root = new RootCommand("CLI client for the ApiComparisons GRPC server")
            {
                new Command("transactions", "transaction GRPC service commands")
                {
                    GetPeopleCommand(),
                    GetStoreCommand(),
                    //new Command("products", "Return, add or remove products.")
                    //{
                    //    new Option<Purchase>("--purchase", "The parent purchase") { Required = true },
                    //    new Command("add", "Add a product.") { new Option<Product>("--product", "The product to add.") { Required = true } },
                    //    new Command("remove", "Remove a product.") { new Option<Product>("--product", "The product to remove.") { Required = true } }
                    //},
                    //new Command("purchases", "Return, add or remove purchases")
                    //{
                    //    new Option<Transaction>("--transaction", "The parent transaction") { Required = true },
                    //    new Command("add", "Add a purchase.") { new Option<Purchase>("--purchase", "The purchase to add.") { Required = true }},
                    //    new Command("remove", "Remove a purchase.") { new Option<Purchase>("--purchase", "The purchase to remove.") { Required = true }}
                    //},
                    //new Command("transactions", "Return, add or remove transactions")
                    //{
                    //    new Option<Person>("--person", "The transaction owner. The default is null to indicate no person specified."),
                    //    new Command("add", "Add a transaction.") { new Option<Transaction>("--transaction", "The transaction to add.") { Required = true } },
                    //    new Command("remove", "Remove a transaction.") { new Option<Transaction>("--transaction", "The transaction to remove.") { Required = true } },
                    //}
                }
            };
            root.Handler = CommandHandler.Create<IHost>(Run);
            return root;
        }

        internal static void Handle(dynamic response) =>
            Console.WriteLine((string)JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }));

        internal static Task Run(IHost host)
        {
            var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();
            return Task.Delay(Timeout.InfiniteTimeSpan, lifetime.ApplicationStopped);
        }

        #region People
        private static Command GetPeopleCommand()
        {
            var people = new Command("people", "Return, add or remove people.")
            {
                new Option<Guid>("--id", "The persons's ID. The default is empty to indicate no ID specified.")
            };
            people.Handler = CommandHandler.Create(typeof(TransactionCommands).GetMethod(nameof(GetPeople), BindingFlags.Static | BindingFlags.NonPublic));

            var add = new Command("add", "Add a person.") { new Option<string>("--name", "The name of the person to add.") { Required = true } };
            add.Handler = CommandHandler.Create(typeof(TransactionCommands).GetMethod(nameof(AddPerson), BindingFlags.Static | BindingFlags.NonPublic));

            var remove = new Command("remove", "Remove a person.") { new Option<Guid>("--id", "The ID of the person to remove.") { Required = true } };
            remove.Handler = CommandHandler.Create(typeof(TransactionCommands).GetMethod(nameof(RemovePerson), BindingFlags.Static | BindingFlags.NonPublic));

            people.Add(add);
            people.Add(remove);
            return people;
        }

        private static async Task GetPeople(IHost host, Guid id)
        {
            dynamic response;
            var options = host.Services.GetRequiredService<IOptions<AppSettings>>();
            using var channel = GrpcChannel.ForAddress(options.Value.ServerUri);
            var client = new Transactions.TransactionsClient(channel);
            if (id == Guid.Empty)
                response = await client.GetPeopleAsync(new Google.Protobuf.WellKnownTypes.Empty());
            else
                response = await client.GetPersonAsync(new Shared.GRPC.Models.PersonRequest { Id = id.ToString() });
            Handle(response);
        }

        private static async Task AddPerson(IHost host, Person person)
        {
            var options = host.Services.GetRequiredService<IOptions<AppSettings>>();
            using var channel = GrpcChannel.ForAddress(options.Value.ServerUri);
            var client = new Transactions.TransactionsClient(channel);
            var response = await client.AddPersonAsync(new Shared.GRPC.Models.PersonRequest { Name = person.Name });
            Handle(response);
        }

        private static async Task RemovePerson(IHost host, Person person)
        {
            var options = host.Services.GetRequiredService<IOptions<AppSettings>>();
            using var channel = GrpcChannel.ForAddress(options.Value.ServerUri);
            var client = new Transactions.TransactionsClient(channel);
            var response = await client.RemovePersonAsync(new Shared.GRPC.Models.PersonRequest { Id = person.ID.ToString() });
            Handle(response);
        }
        #endregion

        #region Stores 
        private static Command GetStoreCommand()
        {
            var stores = new Command("stores", "Return, add or remove stores.")
            {
                new Option<Product>("--product", "The product sold at this store") { Required = true }
            };
            stores.Handler = CommandHandler.Create(typeof(TransactionCommands).GetMethod(nameof(GetStores), BindingFlags.Static | BindingFlags.NonPublic));

            var add = new Command("add", "Add a store.") { new Option<Store>("--store", "The store to add.") { Required = true } };
            add.Handler = CommandHandler.Create(typeof(TransactionCommands).GetMethod(nameof(AddStore), BindingFlags.Static | BindingFlags.NonPublic));

            var remove = new Command("remove", "Remove a store.") { new Option<Store>("--store", "The store to remove.") { Required = true } };
            remove.Handler = CommandHandler.Create(typeof(TransactionCommands).GetMethod(nameof(RemoveStore), BindingFlags.Static | BindingFlags.NonPublic));
            stores.Add(add);
            stores.Add(remove);
            return stores;
        }

        private static async Task GetStores(IHost host, Product product)
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
            Handle(response);
        }

        private static async Task AddStore(IHost host, Store store)
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
            Handle(response);
        }

        private static async Task RemoveStore(IHost host, Store store)
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
            Handle(response);
        }
        #endregion

        #region Products
        #endregion

        #region Purchases
        #endregion

        #region Transactions
        #endregion
    }
}
