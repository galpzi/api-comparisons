using ApiComparisons.Shared.GRPC;
using ApiComparisons.Shared.GRPC.Models;
using Grpc.Core;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc
{
    public class SimpleDummyService : Dummy.DummyBase
    {
        private readonly IDataHelper helper;

        public SimpleDummyService(IDataHelper helper)
        {
            this.helper = helper;
        }

        public override Task<PersonResponse> GetPeople(PersonRequest request, ServerCallContext context)
        {
            return Task.FromResult(new PersonResponse { People = { this.helper.People } });
        }
    }
}
