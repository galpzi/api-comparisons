using GraphQL.Types;

namespace ApiComparisons.Shared.StarWars.Types
{
    public class HumanInputType : InputObjectGraphType<Human>
    {
        public HumanInputType()
        {
            Name = "HumanInput";
            Field(x => x.Name);
            Field(x => x.HomePlanet, nullable: true);
        }
    }
}
