using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Quiz.UI
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
                    webBuilder.UseUrls("https://localhost:44376/");

                    //string dir = Directory.GetCurrentDirectory().Replace("UI", "API");

                    //string _args= $"dotnet run --project {dir}";
                    //var config = new ConfigurationBuilder()
                    //            .AddCommandLine(new string[] { _args })
                    //            .Build();
                });
    }
}