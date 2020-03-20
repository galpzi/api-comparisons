using ApiComparisons.Shared.StarWars.Models;
using GraphQL.Types;

namespace ApiComparisons.Shared.StarWars.GraphQL.Types
{
    public class EpisodeEnum : EnumerationGraphType
    {
        public EpisodeEnum()
        {
            Name = "Episode";
            Description = "One of the films in the Star Wars Trilogy.";
            AddValue(Episodes.NEWHOPE.ToString(), "Released in 1977.", 4);
            AddValue(Episodes.EMPIRE.ToString(), "Released in 1980.", 5);
            AddValue(Episodes.JEDI.ToString(), "Released in 1983.", 6);
        }
    }
}
