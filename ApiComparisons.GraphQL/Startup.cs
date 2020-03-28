using ApiComparisons.Shared;
using ApiComparisons.Shared.DAL;
using ApiComparisons.Shared.GraphQL;
using ApiComparisons.Shared.GraphQL.Types.Outputs;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ApiComparisons.GraphQL
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // TODO: move to IHostedService
            var options = new DbContextOptionsBuilder<TransactionContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new TransactionContext(options);
            var initializer = new ContextInitializer(
                persons: 1,
                stores: 1,
                products: 10,
                purchases: 1,
                transactions: 1);
            initializer.Seed(context);

            services.AddSingleton(provider => new TransactionContext(options));
            services.AddSingleton<ITransactionRepo, TransactionRepo>();
            services.AddSingleton<TransactionQuery>();
            services.AddSingleton<TransactionMutation>();
            services.AddSingleton<StoreType>();
            services.AddSingleton<PersonType>();
            services.AddSingleton<ProductType>();
            services.AddSingleton<PurchaseType>();
            services.AddSingleton<TransactionType>();
            services.AddSingleton<ISchema, TransactionSchema>();

            services.AddLogging();
            services.AddGraphQL(options =>
            {
                options.EnableMetrics = Environment.IsDevelopment();
                options.ExposeExceptions = Environment.IsDevelopment();
                options.UnhandledExceptionDelegate = context => Console.WriteLine($"Error: {context.OriginalException}");
            })
                .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })
                .AddGraphTypes(typeof(TransactionSchema));

            // Add GraphQL.Server.Transports.WebSockets package for websockets support
            //.AddWebSockets()
            //.AddDataLoader();                            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseWebSockets();
            // app.UseGraphQLWebSockets<Schema>("/graphql");

            app.UseGraphQL<ISchema>("/graphql");
        }
    }
}
