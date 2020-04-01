using ApiComparisons.Shared.GRPC;
using Grpc.Net.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc.Client
{
    public class TransactionCommandService : IHostedService
    {
        private readonly ILogger<TransactionCommandService> logger;
        private readonly IHostApplicationLifetime lifetime;
        private readonly AppSettings settings;
        private readonly ParseResult result;

        public TransactionCommandService(ILogger<TransactionCommandService> logger, IHostApplicationLifetime lifetime, IOptions<AppSettings> settings, ParseResult result)
        {
            this.logger = logger;
            this.lifetime = lifetime;
            this.settings = settings.Value;
            this.result = result;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await RunCommandAsync(this.result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
            }
            finally
            {
                this.lifetime?.StopApplication();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task RunCommandAsync(ParseResult result)
        {
            this.logger.LogInformation($"Parsing result: {result}.");
            using var channel = GrpcChannel.ForAddress(this.settings.ServerUri);
            var client = new Transactions.TransactionsClient(channel);
            var response = await client.GetPeopleAsync(new Google.Protobuf.WellKnownTypes.Empty());

            // TODO: match client commands to parse result

            var options = new JsonSerializerOptions { WriteIndented = true };
            this.logger.LogInformation(JsonSerializer.Serialize(response, options));
        }
    }
}