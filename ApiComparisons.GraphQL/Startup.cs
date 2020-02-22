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
    // TODO: Move to shared project
    public class ChatSchema : Schema
    {
    }

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
            services.AddSingleton<ChatSchema>();

            services.AddLogging();            

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = Environment.IsDevelopment();
                options.ExposeExceptions = Environment.IsDevelopment();
                options.UnhandledExceptionDelegate = context => Console.WriteLine($"Error: {context.OriginalException}");
            })
                .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })
                .AddGraphTypes(typeof(ChatSchema));
            //.AddWebSockets() // Add GraphQL.Server.Transports.WebSockets package for websockets support
            //.AddDataLoader();                            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseWebSockets(); // for websockets support
            // app.UseGraphQLWebSockets<Schema>("/graphql");

            app.UseGraphQL<ChatSchema>("/graphql");
        }
    }
}
