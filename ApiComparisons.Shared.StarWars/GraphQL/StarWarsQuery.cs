using ApiComparisons.Shared.StarWars.DAL;
using ApiComparisons.Shared.StarWars.GraphQL.Types;
using GraphQL;
using GraphQL.Types;
using System;

namespace ApiComparisons.Shared.StarWars.GraphQL
{
    public class StarWarsQuery : ObjectGraphType<object>
    {
        public StarWarsQuery(IStarWarsRepo repo)
        {
            Name = "Query";

            Field<ListGraphType<HumanType>>(
                name: "humans",
                resolve: context => repo.GetHumansAsync()
                );

            Field<HumanType>(
                name: "human",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the human" }
                ),
                resolve: context => repo.GetHumanAsync(context.GetArgument<Guid>("id"))
            );

            Field<ListGraphType<DroidType>>(
                name: "droids",
                resolve: context => repo.GetDroidsAsync()
                );

            Field<DroidType>(
                name: "droid",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "droid ID" }),
                resolve: context => repo.GetDroidAsync(context.GetArgument<Guid>("id")));
        }
    }
}
