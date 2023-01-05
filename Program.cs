using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_Cursos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .UseSerilog((context, config) =>
               {
                   config.WriteTo.Console();
                   config.WriteTo.File("Logs.txt", Serilog.Events.LogEventLevel.Information);
                   config.WriteTo.ApplicationInsights(new TelemetryClient()
                   {
                       InstrumentationKey = "ed057ebb-e6fc-4fc1-8e72-02f160ee09f7",
                   }, TelemetryConverter.Events);
               })
                   .ConfigureWebHostDefaults(webBuilder =>
                   {
                       webBuilder.UseStartup<Startup>();
                   });
    }
}
