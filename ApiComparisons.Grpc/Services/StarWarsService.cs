using ApiComparisons.Shared.Protos.StarWars;
using ApiComparisons.Shared.Protos.StarWars.Characters;
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

        public override Task<HumanResponse> GetHuman(HumanRequest request, ServerCallContext context)
        {
            return base.GetHuman(request, context);
        }

        public override Task<HumanResponse> CreateHuman(HumanRequest request, ServerCallContext context)
        {
            return base.CreateHuman(request, context);
        }

        public override Task<HumanResponse> DeleteHuman(HumanRequest request, ServerCallContext context)
        {
            return base.DeleteHuman(request, context);
        }

        public override Task<DroidResponse> GetDroid(DroidRequest request, ServerCallContext context)
        {
            return base.GetDroid(request, context);
        }
    }
}
