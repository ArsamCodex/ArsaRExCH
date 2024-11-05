using ArsaRExCH.Data;

namespace ArsaRExCH.Model.Prop
{
    public class PropTrdaes
    {
        public int PropTrdaesId { get; set; }
        public DateTime TradeOpened { get; set; }
        public DateTime TradeClosedOrTakeProfitOrStopLossOrLiquidated { get; set; }
        public decimal OrderPriceOpened { get; set; }
        public decimal OrderPriceClosed { get; set; }
        //3 5 8 10 20 // I go for max 15
        public int Leverage { get; set; }
        public decimal ProfitInCase { get; set; }
        public decimal FeeInCase { get; set; }
        public bool IsOrderClosedManually { get; set; }
        public bool IsOrderTakeProfit { get; set; }
        public bool IsOrderStopLoss { get; set; }
        public bool IsOrderLiquidated { get; set; }
        public decimal LiquidationPrice { get; set; }
        public decimal StopLoss { get; set; }
        public decimal TakeProfit { get; set; }
        public decimal MarketOrderClose { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser user { get; set; }
    }
}
