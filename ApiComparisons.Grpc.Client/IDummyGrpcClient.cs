using ApiComparisons.Shared.DAL;
using System;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc.Client
{
    public interface IDummyGrpcClient
    {
        Task AddPersonAsync(Person person);
        Task GetPeopleAsync(Guid personID);
        Task RemovePersonAsync(Person person);
    }
}
