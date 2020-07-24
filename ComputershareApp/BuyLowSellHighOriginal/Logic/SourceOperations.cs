using BuyLowSellHigh.Models;
using System;
using System.IO;
using System.Linq;

namespace Logic
{
    /// <summary>
    /// Manages the formatting of source data
    /// </summary>
    public class SourceOperations
    {
        /// <summary>
        /// Retrieves and formats the stock prices from a given source location
        /// </summary>
        /// <param name="sourceLocation"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public decimal[] GetStockPrices(string sourceLocation)
        {
            if (sourceLocation == null) throw new ArgumentNullException("sourceLocation argument cannot be null.");
            if (sourceLocation.Length == 0) throw new ArgumentException("sourceLocation argument cannot be empty.");

            try
            {
                var source = File.ReadAllText(sourceLocation);

                var stockPrices = Array.ConvertAll(source.Split(','), Decimal.Parse);

                return stockPrices;
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine("ERROR: could not find the source file");
                return null;
            }            
        }
    }
}
