using BuyLowSellHigh.Models;
using System;

namespace Logic
{
    public class StockPriceOperations
    {
        /// <summary>
        /// Evaluates and returns the best valid trade for a set of opening stock prices
        /// </summary>
        /// <param name="stockPrices"></param>
        /// <returns></returns>
        public Trade GetBestTrade(decimal[] stockPrices)
        {
            if (stockPrices == null)
            {
                Console.WriteLine("ERROR: null stock prices parameter");
                return null;
            }

            var N = stockPrices.Length;

            if (N == 0)
            {
                Console.WriteLine("INFO: no stock price data was given");
                return null;
            }

            var bestBuyDay = 1;
            var bestSellDay = 1;

            var bestGain = stockPrices[0] - stockPrices[0];

            for (var i = 0; i < N - 1; i++) // iterate through the possible buy days, ie not the last day
            {
                for (var j = i + 1; j < N; j++) // iterate through the possible sell days, ie all days after the current buy day
                {
                    if (stockPrices[j] - stockPrices[i] > bestGain)
                    {
                        // this is the best trade so far - store the details
                        bestBuyDay = i + 1;
                        bestSellDay = j + 1;
                        bestGain = stockPrices[j] - stockPrices[i];
                    }
                }
            }

            return new Trade { 
                BuyDay = bestBuyDay, 
                BuyPrice = stockPrices[bestBuyDay - 1], 
                SellDay = bestSellDay, 
                SellPrice = stockPrices[bestSellDay - 1] 
            };

        }
    }
}
