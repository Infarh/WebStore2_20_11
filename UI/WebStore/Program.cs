using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(host => host
                   .UseStartup<Startup>()

                   //.ConfigureLogging(log => log
                   //        .AddFilter((Namespace, Level) =>
                   //         {
                   //             if (Namespace.StartsWith("Microsoft"))
                   //                 return Level >= LogLevel.Information;
                   //             return true;
                   //         })
                   //        .AddConsole(opt => opt.LogToStandardErrorThreshold = LogLevel.Information))
                   
                   //.UseUrls("http://localhost:5000")
                   //.UseUrls("http://localhost:5001")
                   //.UseHttpSys(opt => opt.MaxAccepts = 5)
                   //.UseKestrel(opt => opt.ListenAnyIP(5001))
                );
    }
}
