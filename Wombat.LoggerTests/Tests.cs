using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using Wombat.Logger;
using Xunit;
using Wombat;

namespace Wombat.LoggerTests
{
    public class Tests
    {
        [Fact]
        public void Test1()
        {
            var logger = LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(level: LogLevel.Trace);
                builder.AddConsole();
                builder.AddDefalutFileLogger(splitTypes: SplitTypes.SplitByLogLevel);
            }).CreateLogger<Tests>();

            //loggerFactory.LogDebug("Program");
            var services = new ServiceCollection();

            //var configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json")
            //    .Build();


            //services.AddLogging(builder =>
            //{
            //    builder.AddConfiguration(configuration.GetSection("Logging"));

            //    // the "standard" provider which logs all messages with severity warning or above to 'warn+err.log' (see appsettings.json for configuration settings)
            //    builder.AddFile(o => o.RootPath = AppContext.BaseDirectory);

            //    //builder.AddFilter("Microsoft", LogLevel.Warning);
            //    //builder.AddFilter("System", LogLevel.Warning);//CategoryName��System��ͷ��������־�������ΪWarning

            //    builder.AddConsole();
            //    // a custom one which only logs messages with level information or below to 'info.log'
            //    //builder.AddFile<InfoFileLoggerProvider>(configure: o => o.RootPath = AppContext.BaseDirectory);
            //});

            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(level: LogLevel.Trace);
                builder.AddConsole();
                builder.AddDefalutFileLogger(splitTypes: SplitTypes.SplitByLogLevel);
            });
            ServiceProvider sp = services.BuildServiceProvider();
            // create logger
            //ILogger<Program> logger = sp.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();

            try
            {
                for (int i = 0; i < 100; i++)
                {
                    logger.LogTrace("This is a trace message. Should be discarded.");
                    logger.LogDebug("This is a debug message. Should be discarded.");
                    logger.LogInformation("This is an info message. Should go into 'info.log' only.");
                    logger.LogWarning("This is a warning message. Should go into 'warn+err.log' only.");
                    logger.LogError("This is an error message. Should go into 'warn+err.log' only.");
                    logger.LogCritical("This is a critical message. Should go into 'warn+err.log' only.");
                    logger.LogDebug(i.ToString());
                    Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }
    }
}
