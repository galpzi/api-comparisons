using ApiComparisons.Shared;
using ApiComparisons.Shared.DAL;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace ApiComparisons.Shared
{
    public class InitializerService : IHostedService
    {
        private readonly DummyContext context;
        private readonly InitializerSettings settings;
        private readonly ILogger<InitializerService> logger;

        public InitializerService(DummyContext context, IOptions<InitializerSettings> options, ILogger<InitializerService> logger)
        {
            this.context = context;
            this.settings = options.Value;
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation($"Seeding context with settings {this.settings}.");
            var initializer = new ContextInitializer(
                persons: this.settings.Persons,
                stores: this.settings.Stores,
                products: this.settings.Products,
                purchases: this.settings.Purchases,
                transactions: this.settings.Transactions);
            initializer.Seed(this.context);
            this.logger.LogInformation($"Context seeded.");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation($"Stopping {nameof(InitializerService)}.");
            return Task.CompletedTask;
        }
    }
}
