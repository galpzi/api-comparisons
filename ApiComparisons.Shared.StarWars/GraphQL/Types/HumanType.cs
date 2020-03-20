using ApiComparisons.Shared.StarWars.DAL;
using ApiComparisons.Shared.StarWars.Models;
using GraphQL.Types;

namespace ApiComparisons.Shared.StarWars.GraphQL.Types
{
    public class HumanType : ObjectGraphType<Human>
    {
        public HumanType(IStarWarsRepo repo)
        {
            Name = "Human";

            Field(h => h.Id).Description("The id of the human.");
            Field(h => h.Name, nullable: true).Description("The name of the human.");

            Field<ListGraphType<CharacterInterface>>(
                "friends",
                resolve: context => repo.GetFriendsAsync(context.Source)
            );
            Field<ListGraphType<EpisodeEnum>>("appearsIn", "Which movie they appear in.");

            Field(h => h.HomePlanet, nullable: true).Description("The home planet of the human.");

            Interface<CharacterInterface>();
        }
    }
}
