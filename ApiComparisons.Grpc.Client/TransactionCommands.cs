using ApiComparisons.Shared.DAL;
using System;
using System.CommandLine;

namespace ApiComparisons.Grpc.Client
{
    public static class TransactionCommands
    {
        public static RootCommand Root()
        {
            return new RootCommand("CLI client for the ApiComparisons GRPC server")
            {
                new Command("transactions", "transaction GRPC service commands")
                {
                    GetQueryCommands(),
                    GetMutationCommands()
                }
            };
        }

        private static Command GetQueryCommands()
        {
            return new Command("query", "GRPC service queries.")
            {
                new Command("people", "Returns people.") { new Option<Guid>("--id", "The persons's ID. The default is empty to indicate no ID specified.") },
                new Command("stores", "Returns stores.") { new Option<Product>("--product", "The product sold at this store") { Required = true } },
                new Command("products", "Returns products.") { new Option<Purchase>("--purchase", "The parent purchase") { Required = true } },
                new Command("purchases", "Returns purchases.") { new Option<Transaction>("--transaction", "The parent transaction") { Required = true } },
                new Command("transactions", "Returns transactions.") { new Option<Person>("--person", "The transaction owner. The default is null to indicate no person specified.") },
            };
        }

        private static Command GetMutationCommands()
        {
            return new Command("mutation", "GRPC service mutations.")
            {
                new Command("people", "Add or remove people.")
                {
                    new Command("add", "Add a person.") { new Option<Person>("--person", "The person to add.") { Required = true } },
                    new Command("remove", "Remove a person.") { new Option<Person>("--person", "The person to remove.") { Required = true } }
                },
                new Command("stores", "Add or remove stores.")
                {
                    new Command("add", "Add a store.") { new Option<Store>("--store", "The store to add.") { Required = true } },
                    new Command("remove", "Remove a store.") { new Option<Store>("--store", "The store to remove.") { Required = true } }
                },
                new Command("products", "Add or remove products.")
                {
                    new Command("add", "Add a product.") { new Option<Product>("--product", "The product to add.") { Required = true } },
                    new Command("remove", "Remove a product.") { new Option<Product>("--product", "The product to remove.") { Required = true } }
                },
                new Command("purchases", "Add or remove purchases")
                {
                    new Command("add", "Add a purchase.") { new Option<Purchase>("--purchase", "The purchase to add.") { Required = true }},
                    new Command("remove", "Remove a purchase.") { new Option<Purchase>("--purchase", "The purchase to remove.") { Required = true }}
                },
                new Command("transactions", "Add or remove transactions")
                {
                    new Command("add", "Add a transaction.") { new Option<Transaction>("--transaction", "The transaction to add.") { Required = true } },
                    new Command("remove", "Remove a transaction.") { new Option<Transaction>("--transaction", "The transaction to remove.") { Required = true } },
                }
            };
        }
    }
}
