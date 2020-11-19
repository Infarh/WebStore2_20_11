using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(host => host
                   .UseStartup<Startup>()
                   //.UseUrls("http://localhost:5000")
                   //.UseUrls("http://localhost:5001")
                   //.UseHttpSys(opt => opt.MaxAccepts = 5)
                   //.UseKestrel(opt => opt.ListenAnyIP(5001))
                );
    }
}
