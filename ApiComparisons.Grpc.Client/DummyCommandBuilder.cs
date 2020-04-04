using ApiComparisons.Shared.DAL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text.Json;

namespace ApiComparisons.Grpc.Client
{
    public class DummyCommandBuilder
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

        private Command GetPeopleCommand()
        {
            var people = new Command("people", "Return, add or remove people.")
            {
                new Option<Guid>("--person-id", "The persons's ID. The default is empty to indicate no ID specified.")
            };
            people.Handler = CommandHandler.Create<IHost, Guid>(async (host, personID) =>
            {
                var client = host.Services.GetRequiredService<IDummyGrpcClient>();
                await client.GetPeopleAsync(personID);
            });

            var add = new Command("add", "Add a person.") { new Option<string>("--name", "The name of the person to add.") { Required = true } };
            add.Handler = CommandHandler.Create<IHost, Person>(async (host, person) =>
            {
                var client = host.Services.GetRequiredService<IDummyGrpcClient>();
                await client.AddPersonAsync(person);
            });

            var remove = new Command("remove", "Remove a person.") { new Option<Guid>("--id", "The ID of the person to remove.") { Required = true } };
            remove.Handler = CommandHandler.Create<IHost, Person>(async (host, person) =>
            {
                var client = host.Services.GetRequiredService<IDummyGrpcClient>();
                await client.RemovePersonAsync(person);
            });
            people.Add(add);
            people.Add(remove);
            return people;
        }

        private Command GetStoresCommand()
        {
            var stores = new Command("stores", "Return, add or remove stores.")
            {
                new Option<Guid>("--product-id", "The store product ID.") { Required = true },
                new Option<Guid>("--store-id", "The store ID.") { Required = true }
            };
            stores.Handler = CommandHandler.Create<IHost, Product>(async (host, product) =>
            {
                var client = host.Services.GetRequiredService<IDummyGrpcClient>();
                await client.GetStoresAsync(product);
            });

            var add = new Command("add", "Add a store.")
            {
                new Option<string>("--name", "The store name.") { Required = true },
                new Option<string>("--address", "The store address.") { Required = true },
                new Option<string>("--country", "The store country.") { Required = true }
            };
            add.Handler = CommandHandler.Create<IHost, Store>(async (host, store) =>
            {
                var client = host.Services.GetRequiredService<IDummyGrpcClient>();
                await client.AddStoreAsync(store);
            });

            var remove = new Command("remove", "Remove a store.")
            {
                new Option<Guid>("--store-id", "The store ID.") { Required = true }
            };
            remove.Handler = CommandHandler.Create<IHost, Store>(async (host, store) =>
            {
                var client = host.Services.GetRequiredService<IDummyGrpcClient>();
                await client.RemoveStoreAsync(store);
            });
            stores.Add(add);
            stores.Add(remove);
            return stores;
        }

        private Command GetProductsCommand()
        {
            var products = new Command("products", "Return, add or remove products.")
            {
                new Option<Guid>("--product-id", "The purchase product ID.") { Required = true },
                new Option<Guid>("--transaction-id", "The purchase transaction ID.") { Required = true }
            };
            products.Handler = CommandHandler.Create<IHost, Purchase>(async (host, purchase) =>
            {
                var client = host.Services.GetRequiredService<IDummyGrpcClient>();
                await client.GetProductAsync(purchase);
            });

            var add = new Command("add", "Add a product.")
            {
            };
            add.Handler = CommandHandler.Create<IHost, Product>(async (host, product) =>
            {
                var client = host.Services.GetRequiredService<IDummyGrpcClient>();
                await client.AddProductAsync(product);
            });

            var remove = new Command("remove", "Remove a product.")
            {
            };
            remove.Handler = CommandHandler.Create<IHost, Product>(async (host, product) =>
            {
                var client = host.Services.GetRequiredService<IDummyGrpcClient>();
                await client.RemoveProductAsync(product);
            });
            products.Add(add);
            products.Add(remove);
            return products;
        }

        private Command GetPurchasesCommand()
        {
            var purchases = new Command("purchases", "Return, add or remove purchases")
            {
                new Option<Guid>("--transaction-id", "The transaction ID.") { Required = true },
                new Option<Guid>("--person-id", "The transaction person ID.") { Required = true }
            };
            purchases.Handler = CommandHandler.Create<IHost, Transaction>(async (host, transaction) =>
            {
                var client = host.Services.GetRequiredService<IDummyGrpcClient>();
                await client.GetPurchasesAsync(transaction);
            });
            var add = new Command("add", "Add a purchase.")
            {
                new Option<Guid>("--purchase-id", "The purchase ID.") { Required = true },
                new Option<Guid>("--transaction-id", "The transaction ID.") { Required = true },
                new Option<decimal>("--price", "The purchase price.") { Required = true },
                new Option<int>("--count", "The purchase count.") { Required = true },
            };
            add.Handler = CommandHandler.Create<IHost, Purchase>(async (host, purchase) =>
            {
                var client = host.Services.GetRequiredService<IDummyGrpcClient>();
                await client.AddPurchaseAsync(purchase);
            });
            var remove = new Command("remove", "Remove a purchase.")
            {
                new Option<Guid>("--purchase-id", "The purchase ID.") { Required = true },
                new Option<Guid>("--transaction-id", "The transaction ID.") { Required = true },
            };
            remove.Handler = CommandHandler.Create<IHost, Purchase>(async (host, purchase) =>
            {
                var client = host.Services.GetRequiredService<IDummyGrpcClient>();
                await client.RemovePurchaseAsync(purchase);
            });
            purchases.Add(add);
            purchases.Add(remove);
            return purchases;
        }

        private Command GetTransactionsCommand()
        {
            var transactions = new Command("transactions", "Return, add or remove transactions")
            {
                new Option<Guid>("--person-id", "The transaction person ID. The default is null to indicate no person specified.")
            };
            transactions.Handler = CommandHandler.Create<IHost, Guid>(async (host, personID) =>
            {
                var client = host.Services.GetRequiredService<IDummyGrpcClient>();
                await client.GetTransactionsAsync(personID);
            });

            var add = new Command("add", "Add a transaction.")
            {
                new Option<Guid>("--person-id", "The transaction person ID.") { Required = true },
                new Option<decimal>("--total", "The transaction total.") { Required = true }
            };
            add.Handler = CommandHandler.Create<IHost, Transaction>(async (host, transaction) =>
            {
                var client = host.Services.GetRequiredService<IDummyGrpcClient>();
                await client.AddTransactionAsync(transaction);
            });

            var remove = new Command("remove", "Remove a transaction.")
            {
                new Option<Guid>("--id", "The transaction ID.") { Required = true },
                new Option<Guid>("--person-id", "The transaction person ID.") { Required = true },
            };
            remove.Handler = CommandHandler.Create<IHost, Transaction>(async (host, transaction) =>
            {
                var client = host.Services.GetRequiredService<IDummyGrpcClient>();
                await client.RemoveTransactionAsync(transaction);
            });
            transactions.Add(add);
            transactions.Add(remove);
            return transactions;
        }
    }
}
