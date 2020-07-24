using Logic;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace BuyLowSellHigh.Tests
{
    [TestFixture]
    public class StockPriceOperationsTests
    {
        private StockPriceOperations _stockPriceOperations;

        [SetUp]
        public void Setup()
        {
            _stockPriceOperations = new StockPriceOperations(new NullLogger<StockPriceOperations>());
        }

        [Test]
        public void GetBestTrade_NullStockPriceArg_ReturnsNull()
        {
            var testResult = _stockPriceOperations.GetBestTrade(null);

            Assert.IsNull(testResult);
        }

        [Test]
        public void GetBestTrade_EmptyStockPriceArg_ReturnsNull()
        {
            decimal[] emptyStockPriceArray = { };
            var testResult = _stockPriceOperations.GetBestTrade(emptyStockPriceArray);

            Assert.IsNull(testResult);
        }

        [Test]
        public void GetBestTrade_MinimalStockPriceSet_ReturnsBestPrice()
        {
            decimal[] smallStockPriceArray = { 1, 2 };
            var testResult = _stockPriceOperations.GetBestTrade(smallStockPriceArray);

            var expectedResult = "1(1),2(2)";

            Assert.AreEqual(expectedResult, testResult.ToString());
        }

        [Test]
        public void GetBestTrade_LargeStockPriceSet_ReturnsBestPrice()
        {
            decimal[] smallStockPriceArray = { 18.93m, 20.25m, 17.05m, 16.59m, 21.09m, 16.22m, 21.43m, 27.13m, 18.62m, 21.31m, 23.96m, 25.52m, 19.64m, 23.49m, 15.28m, 22.77m, 23.1m, 26.58m, 27.03m, 23.75m, 27.39m, 15.93m, 17.83m, 18.82m, 21.56m, 25.33m, 25, 19.33m, 22.08m, 24.03m };
            var testResult = _stockPriceOperations.GetBestTrade(smallStockPriceArray);

            var expectedResult = "15(15.28),21(27.39)";

            Assert.AreEqual(expectedResult, testResult.ToString());
        }

        [Test]
        public void GetBestTrade_SmallStockPriceSetBestTradeIsFirstDayAndLastDay_ReturnsBestTrade()
        {
            decimal[] smallStockPriceArray = { 1, 2, 3 };
            var testResult = _stockPriceOperations.GetBestTrade(smallStockPriceArray);

            var expectedResult = "1(1),3(3)";

            Assert.AreEqual(expectedResult, testResult.ToString());
        }

        [Test]
        public void GetBestTrade_SmallStockPriceSetPriceOnlyDecreases_ReturnsLowestLoss()
        {
            decimal[] decreasingStockPriceArray = { 3, 2, 0.5m };
            var testResult = _stockPriceOperations.GetBestTrade(decreasingStockPriceArray);

            var expectedResult = "1(3),2(2)";

            Assert.AreEqual(expectedResult, testResult.ToString());
        }

        [Test]
        public void GetBestTrade_StockPriceDropsThenRisesSymmetrically_ReturnsBestTrade()
        {
            decimal[] decreasingStockPriceArray = { 5.5m, 5, 4, 3, 2, 1, 2, 3, 4, 5, 5.5m };
            var testResult = _stockPriceOperations.GetBestTrade(decreasingStockPriceArray);

            var expectedResult = "6(1),11(5.5)";

            Assert.AreEqual(expectedResult, testResult.ToString());
        }

        [Test]
        public void GetBestTrade_StockPriceRisesThenDropsSymmetrically_ReturnsBestTrade()
        {
            decimal[] decreasingStockPriceArray = { 1, 2, 3, 4, 5, 5.5m, 5, 4, 3, 2, 1 };
            var testResult = _stockPriceOperations.GetBestTrade(decreasingStockPriceArray);

            var expectedResult = "1(1),6(5.5)";

            Assert.AreEqual(expectedResult, testResult.ToString());
        }

        [Test]
        public void GetBestTrade_StockPriceAlternates_ReturnsEarliestBestTrade()
        {
            decimal[] decreasingStockPriceArray = { 1, 2, 1, 2, 1, 2 };
            var testResult = _stockPriceOperations.GetBestTrade(decreasingStockPriceArray);

            var expectedResult = "1(1),2(2)";

            Assert.AreEqual(expectedResult, testResult.ToString());
        }

        [Test]
        public void GetBestTrade_OnlyIncreaseIsAtStart_ReturnsBestTrade()
        {
            decimal[] decreasingStockPriceArray = { 5, 6, 5, 4, 3, 2, 1 };
            var testResult = _stockPriceOperations.GetBestTrade(decreasingStockPriceArray);

            var expectedResult = "1(5),2(6)";

            Assert.AreEqual(expectedResult, testResult.ToString());
        }

        [Test]
        public void GetBestTrade_OnlyIncreaseIsAtEnd_ReturnsBestTrade()
        {
            decimal[] decreasingStockPriceArray = { 6, 5, 4, 3, 2, 1, 2 };
            var testResult = _stockPriceOperations.GetBestTrade(decreasingStockPriceArray);

            var expectedResult = "6(1),7(2)";

            Assert.AreEqual(expectedResult, testResult.ToString());
        }

        [Test]
        public void GetBestTrade_OnlyIncreaseIsInMiddle_ReturnsBestTrade()
        {
            decimal[] decreasingStockPriceArray = { 6, 5, 4, 5, 4, 3, 2 };
            var testResult = _stockPriceOperations.GetBestTrade(decreasingStockPriceArray);

            var expectedResult = "3(4),4(5)";

            Assert.AreEqual(expectedResult, testResult.ToString());
        }
    }
}