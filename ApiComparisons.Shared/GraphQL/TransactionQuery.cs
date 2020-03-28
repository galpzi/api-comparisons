using ApiComparisons.Shared.GraphQL.Types.Outputs;
using GraphQL;
using GraphQL.Types;
using System;

namespace ApiComparisons.Shared.GraphQL
{
    public class TransactionQuery : ObjectGraphType<object>
    {
        public TransactionQuery(ITransactionRepo repo)
        {
            Name = "TransactionsQuery";
            FieldAsync<ListGraphType<StoreType>>(name: "stores", resolve: async context => await repo.GetStoresAsync());
            FieldAsync<ListGraphType<PersonType>>(name: "people", resolve: async context => await repo.GetPeopleAsync());
            FieldAsync<ListGraphType<ProductType>>(name: "products", resolve: async context => await repo.GetProductsAsync());
            FieldAsync<ListGraphType<PurchaseType>>(name: "purchases", resolve: async context => await repo.GetPurchasesAsync());
            FieldAsync<ListGraphType<TransactionType>>(name: "transactions", resolve: async context => await repo.GetTransactionsAsync());
            FieldAsync<PersonType>(
                name: "person",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "id", Description = "The persons's ID." }),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    return await repo.GetPersonAsync(id);
                });
        }
    }
}
