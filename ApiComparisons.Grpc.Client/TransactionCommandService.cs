using ApiComparisons.Shared.GRPC;
using Grpc.Net.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.CommandLine.Parsing;
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
                using var channel = GrpcChannel.ForAddress(this.settings.ServerUri);
                var client = new Transactions.TransactionsClient(channel);
                var response = await client.GetPeopleAsync(new Google.Protobuf.WellKnownTypes.Empty());
            }
            catch (Exception)
            {
                throw;
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
    }
}