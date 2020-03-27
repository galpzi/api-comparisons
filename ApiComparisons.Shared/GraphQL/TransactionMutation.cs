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
        public TransactionMutation(ITransactionRepo repo)
        {
            Name = "TransactionsMutation";
            Field<PersonType>(
                name: "addPerson",
                description: "Adds a person",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    return repo.AddPersonAsync(name);
                });
            Field<PersonType>(
                name: "removePerson",
                description: "Removes a person",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    return repo.RemovePersonAsync(id);
                });
            Field<StoreType>(
                name: "addStore",
                description: "Adds a store",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StoreInputType>> { Name = "store" }),
                resolve: context =>
                {
                    var store = context.GetArgument<Store>("store");
                    return repo.AddStoreAsync(store);
                });
            Field<StoreType>(
                name: "removeStore",
                description: "Removes a store",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    return repo.RemoveStoreAsync(id);
                });
            Field<ProductType>(
                name: "addProduct",
                description: "Adds a product",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProductInputType>> { Name = "product" }),
                resolve: context =>
                {
                    var product = context.GetArgument<Product>("product");
                    return repo.AddProductAsync(product);
                });
            Field<ProductType>(
                name: "removeProduct",
                description: "Removes a product",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    return repo.RemoveProductAsync(id);
                });
            Field<PurchaseType>(
                name: "addPurchase",
                description: "Adds a purchase",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<PurchaseInputType>> { Name = "purchase" }),
                resolve: context =>
                {
                    var purchase = context.GetArgument<Purchase>("purchase");
                    return repo.AddPurchaseAsync(purchase);
                });
            Field<PurchaseType>(
                name: "removePurchase",
                description: "Removes a purchase",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "productID" },
                    new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "transactionID" }),
                resolve: context =>
                {
                    var productID = context.GetArgument<Guid>("productID");
                    var transactionID = context.GetArgument<Guid>("transactionID");
                    return repo.RemovePurchaseAsync(productID, transactionID);
                });
            Field<TransactionType>(
                name: "addTransaction",
                description: "Adds a transaction",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<TransactionInputType>> { Name = "transaction" }),
                resolve: context =>
                {
                    var transaction = context.GetArgument<Transaction>("transaction");
                    return repo.AddTransactionAsync(transaction);
                });
            Field<TransactionType>(
                name: "removeTransaction",
                description: "Removes a transaction",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "personID" }),
                resolve: context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var personID = context.GetArgument<Guid>("personID");
                    return repo.RemoveTransactionAsync(id, personID);
                });
        }
    }
}
