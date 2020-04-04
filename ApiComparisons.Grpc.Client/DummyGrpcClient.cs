using ApiComparisons.Shared.DAL;
using ApiComparisons.Shared.GRPC;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc.Client
{
    public class DummyGrpcClient : GrpcClient, IDummyGrpcClient
    {
        public DummyGrpcClient(IOptions<AppSettings> options) : base(options)
        {
        }

        public Transactions.TransactionsClient Client => new Transactions.TransactionsClient(this.channel);

        internal static void Print(dynamic response) =>
            Console.WriteLine((string)JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }));

        public async Task GetPeopleAsync(Guid personID)
        {
            dynamic response;
            if (personID == Guid.Empty)
                response = await Client.GetPeopleAsync(new Empty());
            else
                response = await Client.GetPersonAsync(new Shared.GRPC.Models.PersonRequest { Id = personID.ToString() });
            Print(response);
        }

        public async Task AddPersonAsync(Person person)
        {
            var response = await Client.AddPersonAsync(new Shared.GRPC.Models.PersonRequest { Name = person.Name });
            Print(response);
        }

        public async Task RemovePersonAsync(Person person)
        {
            var response = await Client.RemovePersonAsync(new Shared.GRPC.Models.PersonRequest { Id = person.ID.ToString() });
            Print(response);
        }
    }
}
