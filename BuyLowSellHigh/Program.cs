using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Logic;
using System;
using System.IO;
using System.Drawing;
using Serilog;
using BuyLowSellHigh.Models;

namespace BuyLowSellHigh
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("ComputerShareApp.log") // logs to the build output dir
                .CreateLogger();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            serviceProvider.GetService<SourceOperations>();
            serviceProvider.GetService<StockPriceOperations>();

            var logger = serviceProvider.GetService<ILogger<Program>>();

            logger.LogInformation("Buy low, sell high!");
            Console.WriteLine("Buy low, sell high!");

            var input = "";

            while (input.Length < 1)
            {
                Console.WriteLine("Enter the file path of the stock price source or q to quit:");

                input = Console.ReadLine();

                if (input == "q") break;

                var source = input;

                input = "";

                if (source.Length == 0 || !File.Exists(source))
                {
                    Console.WriteLine("Please enter a valid source:");
                    continue;
                }

                var sourceOps = serviceProvider.GetService<SourceOperations>();
                var stockPriceOps = serviceProvider.GetService<StockPriceOperations>();

                Trade bestTrade = null;

                try
                {
                    var stockPrices = sourceOps.GetStockPrices(source);
                    bestTrade = stockPriceOps.GetBestTrade(stockPrices);
                }
                catch(ArgumentNullException e)
                {
                    logger.LogError("Unable to calculate the best trade for the given stock prices!", e);
                }
                catch(ArgumentException e)
                {
                    logger.LogError("Unable to calculate the best trade for the given stock prices!", e);
                }
                catch(FileNotFoundException e)
                {
                    logger.LogError("Unable to calculate the best trade for the given stock prices!", e);
                }
                catch(FormatException e)
                {
                    logger.LogError("Unable to calculate the best trade for the given stock prices!", e);
                }

                if (bestTrade == null)
                {
                    Console.WriteLine("An error occurred: unable to calculate the best trade for the given stock prices! Please check the log for details.", Color.Red);                    
                }
                else
                {
                    Console.WriteLine(bestTrade.ToString());
                }
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddSerilog())
                .AddTransient<SourceOperations>()
                .AddTransient<StockPriceOperations>();
        }
    }
}
