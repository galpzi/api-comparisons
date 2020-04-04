using ApiComparisons.Shared.DAL;
using ApiComparisons.Shared.GraphQL.Types.Inputs;
using ApiComparisons.Shared.GraphQL.Types.Outputs;
using GraphQL;
using GraphQL.Types;
using System;

namespace ApiComparisons.Shared.GraphQL
{
    public class TransactionMutation : ObjectGraphType
    {
        public TransactionMutation(IDummyRepo repo)
        {
            Name = "TransactionsMutation";
            FieldAsync<PersonType>(
                name: "addPerson",
                description: "Adds a person",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }),
                resolve: async context =>
                {
                    var name = context.GetArgument<string>("name");
                    return await repo.AddPersonAsync(name);
                });
            FieldAsync<PersonType>(
                name: "removePerson",
                description: "Removes a person",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "id" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    return await repo.RemovePersonAsync(id);
                });
            FieldAsync<StoreType>(
                name: "addStore",
                description: "Adds a store",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StoreInputType>> { Name = "store" }),
                resolve: async context =>
                {
                    var store = context.GetArgument<Store>("store");
                    return await repo.AddStoreAsync(store);
                });
            FieldAsync<StoreType>(
                name: "removeStore",
                description: "Removes a store",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "id" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    return await repo.RemoveStoreAsync(id);
                });
            FieldAsync<ProductType>(
                name: "addProduct",
                description: "Adds a product",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProductInputType>> { Name = "product" }),
                resolve: async context =>
                {
                    var product = context.GetArgument<Product>("product");
                    return await repo.AddProductAsync(product);
                });
            FieldAsync<ProductType>(
                name: "removeProduct",
                description: "Removes a product",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "id" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    return await repo.RemoveProductAsync(id);
                });
            FieldAsync<PurchaseType>(
                name: "addPurchase",
                description: "Adds a purchase",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<PurchaseInputType>> { Name = "purchase" }),
                resolve: async context =>
                {
                    var purchase = context.GetArgument<Purchase>("purchase");
                    return await repo.AddPurchaseAsync(purchase);
                });
            FieldAsync<PurchaseType>(
                name: "removePurchase",
                description: "Removes a purchase",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "productID" },
                    new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "transactionID" }),
                resolve: async context =>
                {
                    var productID = context.GetArgument<Guid>("productID");
                    var transactionID = context.GetArgument<Guid>("transactionID");
                    return await repo.RemovePurchaseAsync(productID, transactionID);
                });
            FieldAsync<TransactionType>(
                name: "addTransaction",
                description: "Adds a transaction",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<TransactionInputType>> { Name = "transaction" }),
                resolve: async context =>
                {
                    var transaction = context.GetArgument<Transaction>("transaction");
                    return await repo.AddTransactionAsync(transaction);
                });
            FieldAsync<TransactionType>(
                name: "removeTransaction",
                description: "Removes a transaction",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "personID" }),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var personID = context.GetArgument<Guid>("personID");
                    return await repo.RemoveTransactionAsync(id, personID);
                });
        }
    }
}
