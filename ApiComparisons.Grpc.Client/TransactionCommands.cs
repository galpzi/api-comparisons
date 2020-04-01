using ApiComparisons.Shared.DAL;
using System;
using System.CommandLine;

namespace ApiComparisons.Grpc.Client
{
    public static class TransactionCommands
    {
        public static RootCommand BuildRoot()
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
            return new Command("query", "GRPC service queries")
            {
                new Command("people", "Returns people") { new Option<Guid>("id", "The persons's ID") },
                new Command("transactions", "Returns transactions") { new Option<Person>("person", "The transaction owner") },
                new Command("products", "Returns products") { new Argument<Purchase> { Name = "purchase", Description = "The parent purchase" } },
                new Command("stores", "Returns stores") { new Argument<Product> { Name = "product", Description = "The product sold at this store" } },
                new Command("purchases", "Returns purchases") { new Argument<Transaction> { Name = "transaction", Description = "The parent transaction" } },
            };
        }

        private static Command GetMutationCommands()
        {
            return new Command("mutation", "GRPC service mutations")
            {
                new Command("stores", "Add or remove stores") { new Argument<Store> { Name = "store", Description = "The store to add or remove" } },
                new Command("people", "Add or remove people") { new Argument<Store> { Name = "people", Description = "The person to add or remove" } },
                new Command("products", "Add or remove products") { new Argument<Store> { Name = "products", Description = "The product to add or remove" } },
                new Command("purchases", "Add or remove purchases") { new Argument<Store> { Name = "purchases", Description = "The purchase to add or remove" } },
                new Command("transactions", "Add or remove transactions") { new Argument<Store> { Name = "transactions", Description = "The transaction to add or remove" } },
            };
        }
    }
}
