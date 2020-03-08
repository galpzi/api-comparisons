using ApiComparisons.Shared.Protos;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Greeter.GreeterClient(channel);
            //var reply = await client.SayHelloAsync(new HelloRequest
            //{
            //    Name = "Client"
            //});

            //Console.WriteLine($"Greeting: {reply.Message}");
            //Console.WriteLine($"Press any key to exit...");
            //Console.ReadKey();

            await StarWars();
        }

        static async Task StarWars()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new StarWars.StarWarsClient(channel);
            var reply = await client.GetCharacterAsync(new CharacterRequest
            {
                Name = "Boba Fett"
            });

            Console.WriteLine($"Character ID: {reply.Character.Id}, Name: {reply.Character.Name}");
            Console.WriteLine($"Character Friends: {reply.Character.Friends.Count}, Appears In: {reply.Character.AppearsIn.Count}");
            Console.WriteLine($"Press any key to exit...");
            Console.ReadKey();
        }
    }
}
