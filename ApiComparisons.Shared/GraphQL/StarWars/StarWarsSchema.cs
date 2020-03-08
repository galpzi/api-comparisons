using GraphQL.Types;
using GraphQL.Utilities;
using System;

namespace ApiComparisons.Shared.GraphQL.StarWars
{
    public class StarWarsSchema : Schema
    {
        public StarWarsSchema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetRequiredService<StarWarsQuery>();
            Mutation = provider.GetRequiredService<StarWarsMutation>();
        }
    }
}
