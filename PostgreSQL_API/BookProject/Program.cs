using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BookProject
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
                    webBuilder.UseKestrel()
                              .UseStartup<Startup>()
                              .UseUrls("http://localhost:8000");
                });
    }
}
