using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc.Client
{
    public class Program
    {
        static Task Main(string[] args)
        {
            var commands = new TransactionCommandBuilder();
            var parser = new CommandLineBuilder(commands.Root())
                .UseDefaults()
                .UseHost(CreateHostBuilder)
                .Build();
            return parser.InvokeAsync(args);
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.SetBasePath(AppContext.BaseDirectory);
                    builder.AddJsonFile("appsettings.json", optional: false);
                    builder.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true);
                    builder.AddEnvironmentVariables();
                    builder.AddCommandLine(args);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddLogging();
                    services.AddSingleton<IDummyGrpcClient, DummyGrpcClient>();
                    services.Configure<AppSettings>(context.Configuration.GetSection("Settings"));
                })
                .ConfigureLogging((context, builder) =>
                {
                    builder.AddConsole();
                    builder.AddConfiguration(context.Configuration);
                });
        }
    }
}
