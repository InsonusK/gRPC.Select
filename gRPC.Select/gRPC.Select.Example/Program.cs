using gRPC.Select.TestDB.TestTools;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace GrpcService.Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var _init = new DBInit("gRPCSelectExampleDB");
            try
            {
                _init.InitDB();
                CreateHostBuilder(args).Build().Run();
            }
            finally
            {
                _init.CleanupDB();
            }

        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
