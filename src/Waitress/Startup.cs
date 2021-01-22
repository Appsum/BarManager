using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Waitress.Configuration;

namespace Waitress
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
            services.AddControllers();

            services.AddMediatR(typeof(Startup));

            services.AddHostedService<BusListener>();

            services.AddSignalR();

            services.Configure<ApplicationInsightsSettings>(Configuration.GetSection(ApplicationInsightsSettings.ConfigurationKey));
            services.Configure<ServiceBusSettings>(Configuration.GetSection(ServiceBusSettings.ConfigurationKey));
            services.Configure<BlobStorageSettings>(Configuration.GetSection(BlobStorageSettings.ConfigurationKey));

            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(Configuration[$"{BlobStorageSettings.ConfigurationKey}:ConnectionString"]);
            });

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Waitress.Api", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Waitress.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<TablesHub>("/tables");
                endpoints.MapControllers();
            });
        }
    }
}