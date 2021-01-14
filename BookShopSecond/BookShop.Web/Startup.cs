using BookShop.Web.Consumer;
using BookShop.Web.Extension;
using BookShop.Web.Producer;
using BookShop.Web.Services;
using BookShopSecond.Data;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BookShop.Web
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
            services.AddSingleton<BSContext>();
            services.AddSingleton<ShopService>();
            services.AddSingleton(isp => new BookShopContextDbContextFactory(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<BookService>();
            
            
            
            services.AddSingleton<NeedBookProducer>();
            services.AddBackgroundJobs();
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
                        "Receive-queue", ep =>
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
