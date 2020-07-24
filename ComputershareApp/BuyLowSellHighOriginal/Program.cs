using Microsoft.Extensions.DependencyInjection;
using Logic;
using System;
using System.IO;

namespace BuyLowSellHigh
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StartUp();

            Console.WriteLine("Buy low, sell high!");
            Console.WriteLine("Enter the stock price source:");

            var source = "";

            while (source.Length == 0 && !File.Exists(source))
            {
                source = Console.ReadLine();

                if (!File.Exists(source))
                {
                    Console.WriteLine("Please enter a valid source:");
                }
            }

            var sourceOps = new SourceOperations();

            var stockPrices = sourceOps.GetStockPrices(source);

            var stockPriceOps = new StockPriceOperations();

            var bestTrade = stockPriceOps.GetBestTrade(stockPrices);

            if (bestTrade == null)
            {
                Console.WriteLine("ERROR: unable to calculate the best trade for the given stock prices!");
            }
            else
            {
                Console.WriteLine(bestTrade.ToString());
            }
        }

        private static void StartUp()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            //we will configure logging here
        }
    }
}
