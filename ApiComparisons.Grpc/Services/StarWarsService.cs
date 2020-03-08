using ApiComparisons.Shared.Protos;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc
{
    public class StarWarsService : StarWars.StarWarsBase
    {
        private readonly ILogger<StarWarsService> logger;

        public StarWarsService(ILogger<StarWarsService> logger)
        {
            this.logger = logger;
        }

        public override Task<CharacterResponse> GetCharacter(CharacterRequest request, ServerCallContext context)
        {
            var character = new Character
            {
                Name = "Boba Fett",
                Id = System.Guid.NewGuid().ToString()
            };
            character.AppearsIn.AddRange(new[] { Episode.Empire, Episode.Jedi });

            return Task.FromResult(new CharacterResponse
            {
                Character = character
            });
        }
    }
}
