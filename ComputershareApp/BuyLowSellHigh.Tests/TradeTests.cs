using BuyLowSellHigh.Models;
using Logic;
using NUnit.Framework;
using System;
using System.IO;

namespace BuyLowSellHigh.Tests
{
    [TestFixture]
    public class TradeTests
    {
        private Trade _trade;

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void ToString_ValidTradeObject_ReturnsCorrectString()
        {
            _trade = new Trade { BuyDay = 1, BuyPrice = 1.1m, SellDay = 10, SellPrice = 10.1m };

            var testResult = _trade.ToString();

            Assert.AreEqual("1(1.1),10(10.1)", testResult);
        }
    }
}