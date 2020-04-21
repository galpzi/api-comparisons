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
        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<InitializerService>();
            services.Configure<InitializerSettings>(Configuration.GetSection("Settings:Initializer"));
            services.AddSingleton(provider => new DummyContext(new DbContextOptionsBuilder<DummyContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options));
            services.AddSingleton<IDummyRepo, DummyRepo>();
            services.AddSingleton<DummyQuery>();
            services.AddSingleton<DummyMutation>();
            services.AddSingleton<StoreType>();
            services.AddSingleton<PersonType>();
            services.AddSingleton<ProductType>();
            services.AddSingleton<PurchaseType>();
            services.AddSingleton<TransactionType>();
            services.AddSingleton<ISchema, DummySchema>();

            services.AddLogging();
            services.AddGraphQL(options =>
            {
                options.EnableMetrics = Environment.IsDevelopment();
                options.ExposeExceptions = Environment.IsDevelopment();
                options.UnhandledExceptionDelegate = context => Console.WriteLine($"Error: {context.OriginalException}");
            })
                .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })
                .AddGraphTypes(typeof(DummySchema));

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
