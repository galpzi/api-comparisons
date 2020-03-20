using ApiComparisons.Shared.StarWars.DAL;
using ApiComparisons.Shared.StarWars.GraphQL.Types;
using ApiComparisons.Shared.StarWars.Models;
using GraphQL;
using GraphQL.Types;

namespace ApiComparisons.Shared.StarWars.GraphQL
{
    /// <example>
    /// This is an example JSON request for a mutation
    /// {
    ///   "query": "mutation ($human:HumanInput!){ createHuman(human: $human) { id name } }",
    ///   "variables": {
    ///     "human": {
    ///       "name": "Boba Fett"
    ///     }
    ///   }
    /// }
    /// </example>
    public class StarWarsMutation : ObjectGraphType
    {
        public StarWarsMutation(IStarWarsRepo repo)
        {
            Name = "Mutation";

            Field<HumanType>(
                "createHuman",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<HumanInputType>> { Name = "human" }
                ),
                resolve: context =>
                {
                    Human human = context.GetArgument<Human>("human");
                    return repo.AddHumanAsync(human);
                });

            Field<HumanType>(
                name: "deleteHuman",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<HumanInputType>>
                    {
                        Name = "human"
                    }),
                resolve: context =>
                {
                    Human human = context.GetArgument<Human>("human");
                    return repo.DeleteHumanAsync(human);
                });
        }
    }
}
