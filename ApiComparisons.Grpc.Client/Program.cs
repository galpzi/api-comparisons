using ApiComparisons.Shared.GRPC;
using ApiComparisons.Shared.GRPC.Models;
using ApiComparisons.Shared.Protos;
using ApiComparisons.Shared.StarWars.GRPC;
using ApiComparisons.Shared.StarWars.GRPC.Characters;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Threading;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc.Client
{
    class Program
    {
        static Task Main(string[] args)
        {
            var root = TransactionCommands.BuildRoot();
            root.Handler = CommandHandler.Create<IHost>(Run);
            var parser = new CommandLineBuilder(root)
                .UseDefaults()
                .UseHost(host =>
                {
                    host.ConfigureAppConfiguration((context, builder) =>
                    {
                        builder.SetBasePath(AppContext.BaseDirectory);
                        builder.AddJsonFile("appsettings.json", optional: false);
                        builder.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true);
                        builder.AddEnvironmentVariables();
                        builder.AddCommandLine(args);
                    });
                    host.ConfigureServices((context, services) =>
                    {
                        services.AddLogging();
                        services.Configure<AppSettings>(context.Configuration.GetSection("Settings"));
                        services.AddHostedService<TransactionCommandService>();
                    });
                    host.ConfigureLogging((context, builder) =>
                    {
                        builder.AddConsole();
                        builder.AddConfiguration(context.Configuration);
                    });
                })
                .Build();
            return parser.InvokeAsync(args);
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

        internal static Task Run(IHost host)
        {
            var services = host.Services;
            var lifetime = services.GetRequiredService<IHostApplicationLifetime>();
            return Task.Delay(Timeout.InfiniteTimeSpan, lifetime.ApplicationStopped);
        }
    }
}
