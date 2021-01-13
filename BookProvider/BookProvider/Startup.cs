using ExternalApi.Consumer;
using ExternalApi.Producer;
using ExternalApi.Services;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExternalApi
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
            services.AddSingleton<HttpClient>();
            services.AddSingleton<ApiService>();
            services.AddSingleton<BookProducer>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddMassTransit(isp =>
            {
                var hostConfig = new MassTransitConfiguration();
                Configuration.GetSection("MassTransit").Bind(hostConfig);

                return Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(
                        new Uri(hostConfig.RabbitMqAddress),
                        h =>
                        {
                            h.Username(hostConfig.UserName);
                            h.Password(hostConfig.Password);
                        });

                    cfg.Durable = hostConfig.Durable;
                    cfg.PurgeOnStartup = hostConfig.PurgeOnStartup;

                    cfg.ReceiveEndpoint(host,
                        "Request-queue", ep =>
                        {
                            ep.PrefetchCount = 1;
                            ep.ConfigureConsumer<BookConsumer>(isp);
                        });
                });
            },
                ispc =>
                {
                    ispc.AddConsumers(typeof(BookConsumer).Assembly);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
