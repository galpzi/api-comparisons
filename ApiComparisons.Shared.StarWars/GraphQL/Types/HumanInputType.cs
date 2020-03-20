using ApiComparisons.Shared.StarWars.Models;
using GraphQL.Types;

namespace ApiComparisons.Shared.StarWars.GraphQL.Types
{
    public class HumanInputType : InputObjectGraphType<Human>
    {
        public HumanInputType()
        {
            Name = "HumanInput";

            Field(x => x.Name);
            Field(x => x.Id, nullable: true);
            Field(x => x.HomePlanet, nullable: true);
        }
    }
}
