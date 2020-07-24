namespace BuyLowSellHigh.Models
{
    /// <summary>
    /// An object that describes a particular trade
    /// </summary>
    public class Trade
    {
        /// <summary>
        /// The day of the month to carry out the buy part of the trade
        /// </summary>
        public int BuyDay { get; set; }

        /// <summary>
        /// The opening stock price on the day of the buy part of the trade
        /// </summary>
        public decimal BuyPrice { get; set; }

        /// <summary>
        /// The day of the month to carry out the sell part of the trade
        /// </summary>
        public int SellDay { get; set; }

        /// <summary>
        /// The opening stock price on the day of the sell part of the trade
        /// </summary>
        public decimal SellPrice { get; set; }


        public override string ToString()
        {
            return $"{BuyDay}({BuyPrice}),{SellDay}({SellPrice})";
        }
    }
}
