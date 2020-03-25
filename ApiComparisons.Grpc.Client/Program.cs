using ApiComparisons.Shared.GRPC;
using ApiComparisons.Shared.GRPC.Models;
using ApiComparisons.Shared.Protos;
using ApiComparisons.Shared.StarWars.GRPC;
using ApiComparisons.Shared.StarWars.GRPC.Characters;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // await Greet();

            // await StarWars();

            await Transactions();
        }

        static async Task Greet()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest
            {
                Name = "Client"
            });

            Console.WriteLine($"Greeting: {reply.Message}");
            Console.WriteLine($"Press any key to exit...");
            Console.ReadKey();
        }

        static async Task StarWars()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new StarWars.StarWarsClient(channel);
            var response = await client.GetCharacterAsync(new CharacterRequest
            {
                Name = "Boba Fett"
            });

            Console.WriteLine($"Character ID: {response.Character.Id}, Name: {response.Character.Name}");
            Console.WriteLine($"Character Friends: {response.Character.Friends.Count}, Appears In: {response.Character.AppearsIn.Count}");
            Console.WriteLine($"Press any key to exit...");
            Console.ReadKey();
        }

        static async Task Transactions()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Transactions.TransactionsClient(channel);
            var response = await client.GetPersonAsync(new PersonRequest
            {
                Id = ""
            });

            Console.WriteLine($"Character ID: {response.Person.Id}, Name: {response.Person.Name}, Created: {response.Person.Created}");
            Console.WriteLine($"Press any key to exit...");
            Console.ReadKey();
        }
    }
}
