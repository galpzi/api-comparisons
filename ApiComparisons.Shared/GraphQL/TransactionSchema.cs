using GraphQL.Types;
using GraphQL.Utilities;
using System;

namespace ApiComparisons.Shared.GraphQL
{
    public class TransactionSchema : Schema
    {
        public TransactionSchema(IServiceProvider services) : base(services)
        {
            Services = services;
            Query = services.GetRequiredService<TransactionQuery>();
            Mutation = services.GetRequiredService<TransactionMutation>();
        }
    }
}
