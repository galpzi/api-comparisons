using ApiComparisons.Shared.StarWars.GraphQL;
using ApiComparisons.Shared.StarWars.GraphQL.Types;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddSingleton<StarWarsData>();
            services.AddSingleton<StarWarsQuery>();
            services.AddSingleton<StarWarsMutation>();
            services.AddSingleton<DroidType>();
            services.AddSingleton<HumanType>();
            services.AddSingleton<EpisodeEnum>();
            services.AddSingleton<HumanInputType>();
            services.AddSingleton<CharacterInterface>();
            services.AddSingleton<ISchema, StarWarsSchema>();

            services.AddLogging();
            services.AddGraphQL(options =>
            {
                options.EnableMetrics = Environment.IsDevelopment();
                options.ExposeExceptions = Environment.IsDevelopment();
                options.UnhandledExceptionDelegate = context => Console.WriteLine($"Error: {context.OriginalException}");
            })
                .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })
                .AddGraphTypes(typeof(StarWarsSchema));

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
