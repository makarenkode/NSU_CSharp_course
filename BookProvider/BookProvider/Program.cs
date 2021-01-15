using Microsoft.AspNetCore.Hosting;
#warning неиспользуемые using'и, проверь везде
#warning ready
using Microsoft.Extensions.Hosting;

#warning namespace не соответствует реальному, проверь везде
#warning ready
namespace BookProvider
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
