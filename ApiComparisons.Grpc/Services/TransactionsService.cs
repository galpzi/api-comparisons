using ApiComparisons.Shared;
using ApiComparisons.Shared.GRPC;
using ApiComparisons.Shared.GRPC.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc
{
    public class TransactionsService : Transactions.TransactionsBase
    {
        private readonly ITransactionRepo repo;
        private readonly ILogger<TransactionsService> logger;

        public TransactionsService(ILogger<TransactionsService> logger, ITransactionRepo repo)
        {
            this.logger = logger;
            this.repo = repo;
        }

        public override async Task<PersonResponse> GetPerson(PersonRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Id, out Guid id))
                throw new ArgumentException($"invalid person id {request.Id}");

            var person = await this.repo.GetPersonAsync(id);
            return new PersonResponse
            {
                Person = new Person
                {
                    Name = person.Name,
                    Id = person.ID.ToString(),
                    Created = Timestamp.FromDateTime(person.Created)
                }
            };
        }

        public override async Task<PeopleResponse> GetPeople(Empty request, ServerCallContext context)
        {
            var response = new PeopleResponse();
            var people = await this.repo.GetPeopleAsync();
            var repeated = people.Select(o => new Person
            {
                Name = o.Name,
                Id = o.ID.ToString(),
                Created = Timestamp.FromDateTime(o.Created)
            });
            response.People.AddRange(repeated);
            return response;
        }
    }
}
