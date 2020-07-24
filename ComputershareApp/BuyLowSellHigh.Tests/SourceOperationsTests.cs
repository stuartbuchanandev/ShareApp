using Logic;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using System;
using System.IO;

namespace BuyLowSellHigh.Tests
{
    [TestFixture]
    public class SourceOperationsTests
    {
        private SourceOperations _sourceOperations;

        [SetUp]
        public void Setup()
        {
            _sourceOperations = new SourceOperations(new NullLogger<SourceOperations>());
        }

        [Test]
        public void TestGetStockPrices_NullSource_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _sourceOperations.GetStockPrices(null));
        }

        [Test]
        public void TestGetStockPrices_EmptySource_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _sourceOperations.GetStockPrices(""));
        }

        [Test]
        public void TestGetStockPrices_InvalidSource_ThrowsFormatException()
        {
            var invalidSource = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\InvalidData.txt");

            Assert.Throws<FormatException>(() => _sourceOperations.GetStockPrices(invalidSource));
        }

        [Test]
        public void TestGetStockPrices_ValidSource_ReturnsCorrectlyParsedStockPrices()
        {
            var source = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\ValidData.txt");

            var testResult = _sourceOperations.GetStockPrices(source);

            decimal[] expectedResult = { 1.10m, 2.20m };

            Assert.AreEqual( expectedResult, testResult);
        }
    }
}