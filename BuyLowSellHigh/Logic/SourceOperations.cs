using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Logic
{
    /// <summary>
    /// Manages the retrieval and formatting of source data
    /// </summary>
    public class SourceOperations
    {
        private readonly ILogger _logger;

        public SourceOperations(ILogger<SourceOperations> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Retrieves and formats the stock prices from a given source location
        /// </summary>
        /// <param name="sourceLocation"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <returns></returns>
        public decimal[] GetStockPrices(string sourceLocation)
        {
            if (sourceLocation == null)
            {
                _logger.LogError("sourceLocation argument cannot be null.");
                throw new ArgumentNullException("sourceLocation argument cannot be empty.");
            }
            if (sourceLocation.Length == 0)
            {
                _logger.LogError("sourceLocation argument cannot be empty.");
                throw new ArgumentException("sourceLocation argument cannot be empty.");
            }

            try
            {
                var source = File.ReadAllText(sourceLocation);

                var stockPrices = Array.ConvertAll(source.Split(','), Decimal.Parse);

                return stockPrices;
            }
            catch(FileNotFoundException e)
            {
                _logger.LogError($"Could not find the source file at {sourceLocation}");
                throw e;
            }
            catch(FormatException e)
            {
                _logger.LogError($"There was an error parsing the source as a comma separated list of decimal values; file: {sourceLocation}");
                throw e;
            }
        }
    }
}
