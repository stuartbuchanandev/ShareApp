using BuyLowSellHigh.Models;
using Microsoft.Extensions.Logging;
using System;

namespace Logic
{
    public class StockPriceOperations
    {
        private readonly ILogger _logger;

        public StockPriceOperations(ILogger<StockPriceOperations> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Evaluates and returns the best valid trade for a set of opening stock prices
        /// </summary>
        /// <remarks>Assumes that you can't buy and sell on the same day and always returns a trade if one is possible
        /// ie. if a profit is not possible it will return the lowest loss.</remarks>
        /// <param name="stockPrices"></param>
        /// <returns></returns>
        public Trade GetBestTrade(decimal[] stockPrices)
        {
            if (stockPrices == null)
            {
                _logger.LogError("Null stock prices parameter");
                return null;
            }

            var N = stockPrices.Length;

            if (N == 0)
            {
                _logger.LogInformation("no stock price data was given");
                return null;
            }

            if (N == 1)
            {
                _logger.LogInformation("not enough stock price data was given");
                return null;
            }

            var bestBuyDay = 1;
            var bestSellDay = 2;
            var currentBuyDay = 1;
            var currentSellDay = 2;

            var bestGain = stockPrices[1] - stockPrices[0];

            // move two pointers through the array, one looking for best possible buy day and the other best possible sell day
            while (currentSellDay < N)
            {
                currentSellDay++;

                if (stockPrices[currentSellDay - 1] - stockPrices[currentBuyDay - 1] > bestGain)
                {
                    // this is the best trade so far - store the details
                    bestBuyDay = currentBuyDay;
                    bestSellDay = currentSellDay;
                    bestGain = stockPrices[currentSellDay - 1] - stockPrices[currentBuyDay - 1];
                }

                if (stockPrices[currentSellDay - 1] < stockPrices[currentBuyDay - 1])
                {
                    // reset the current buy day because we have found a new best possible buy price
                    currentBuyDay = currentSellDay;
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
