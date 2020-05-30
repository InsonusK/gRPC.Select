using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using gRPC.Select.Test.TestTools;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GrpcService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var init = new DBInit("gRPCSelectExampleDB");
            try
            {
                init.InitDB();
                CreateHostBuilder(args).Build().Run();
            }
            finally
            {
                init.CleanupDB();
            }

        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
