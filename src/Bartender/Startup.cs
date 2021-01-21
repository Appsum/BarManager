using System;

using Bartender.Configuration;
using Bartender.Drinks.Application.EventBus;
using Bartender.Drinks.Domain.Repositories;
using Bartender.Drinks.Infrastructure.EventBus;
using Bartender.Drinks.Infrastructure.Repositories;

using FluentValidation.AspNetCore;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Bartender
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));

            // services.AddSingleton<IDrinksRepository, InMemoryDrinksRepository>();
            services.AddTransient<IDrinksRepository, AzureTableStorageDrinksRepository>();
            services.AddTransient<IEventBus, NullEventBus>();

            services.Configure<ApplicationInsightsSettings>(Configuration.GetSection(ApplicationInsightsSettings.ConfigurationKey));
            services.Configure<EventHubSettings>(Configuration.GetSection(EventHubSettings.ConfigurationKey));
            services.Configure<ServiceBusSettings>(Configuration.GetSection(ServiceBusSettings.ConfigurationKey));
            services.Configure<TableStorageSettings>(Configuration.GetSection(TableStorageSettings.ConfigurationKey));

            RegisterCloudTableClient(services);

            RegisterAzureServiceBusTopicClient(services);

            services.AddControllers()
                    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "BarManager.Api", Version = "v1"}); });
        }

        private static void RegisterCloudTableClient(IServiceCollection services)
        {
            services.AddTransient(provider =>
            {
                var options = provider.GetService<IOptions<TableStorageSettings>>();
                if (options == null)
                {
                    throw new NullReferenceException("Table Storage Settings are not set in the configuration");
                }

                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(options.Value.ConnectionString);
                return cloudStorageAccount.CreateCloudTableClient();
            });
        }

        private static void RegisterAzureServiceBusTopicClient(IServiceCollection services)
        {
            services.AddSingleton<ITopicClient>(serviceProvider =>
            {
                ServiceBusSettings serviceBusSettings = serviceProvider.GetRequiredService<IOptions<ServiceBusSettings>>().Value;
                return new TopicClient(serviceBusSettings.ConnectionString, serviceBusSettings.TopicName, RetryPolicy.Default);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BarManager.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}