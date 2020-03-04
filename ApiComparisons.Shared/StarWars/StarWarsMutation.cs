using ApiComparisons.Shared.StarWars.Types;
using GraphQL;
using GraphQL.Types;

namespace ApiComparisons.Shared.StarWars
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
        public StarWarsMutation(StarWarsData data)
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
                    return data.AddHuman(human);
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
                    return data.RemoveHuman(human);
                });
        }
    }
}
