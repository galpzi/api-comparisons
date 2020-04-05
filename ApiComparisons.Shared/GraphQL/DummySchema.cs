using GraphQL.Types;
using GraphQL.Utilities;
using System;

namespace ApiComparisons.Shared.GraphQL
{
    public class DummySchema : Schema
    {
        public DummySchema(IServiceProvider services) : base(services)
        {
            Services = services;
            Query = services.GetRequiredService<DummyQuery>();
            Mutation = services.GetRequiredService<DummyMutation>();
        }
    }
}
