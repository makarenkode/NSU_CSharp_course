using BookProvider.Consumer;
using BookProvider.Producer;
using BookProvider.Services;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using MassTransit.Azure.ServiceBus.Core;
using Microsoft.Azure.ServiceBus.Primitives;


namespace BookProvider
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

                    return Bus.Factory.CreateUsingAzureServiceBus(cfg =>
                    {

                        var host = cfg.Host(new Uri(hostConfig.Address), h =>
                        {

                            h.OperationTimeout = TimeSpan.FromSeconds(30);
                            h.TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(hostConfig.SharedAccessKeyName, hostConfig.SharedAccessKey);
                        });


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
