using ApiComparisons.Shared.GraphQL.Types;
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
            Field<ListGraphType<PersonType>>(name: "people", resolve: context => repo.GetPeopleAsync());
            Field<ListGraphType<TransactionType>>(name: "transactions", resolve: context => repo.GetTransactionsAsync());
            Field<PersonType>(
                name: "person",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<GuidGraphType>> { Name = "id", Description = "The persons's ID." }),
                resolve: context => repo.GetPersonAsync(context.GetArgument<Guid>(name: "id")));
        }
    }
}
