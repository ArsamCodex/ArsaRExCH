﻿using ArsaRExCH.Data;
using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.Model.Prop
{
    public class PropTrade
    {
        public int PropTradeId { get; set; }
        public DateTime TradeOpened { get; set; }
        public DateTime? TradeClosedDate { get; set; }  // Nullable until trade closes

        //BtcPrice aat time
        public decimal OrderPriceOpened { get; set; }
        //btc price at close time
        public decimal? OrderPriceClosed { get; set; }
        public int Leverage { get; set; }
        public decimal ProfitInCase { get; set; }
        public decimal FeeInCase { get; set; }
        public OrderStatus Status { get; set; } 
        public decimal LiquidationPrice { get; set; }
        public decimal? StopLoss { get; set; }
        public decimal? TakeProfit { get; set; }
       // public bool MarketOrderClose { get; set; }
       // public decimal ForceCloseTradeAccountLost { get; set; }
        [Precision(18,0)]
        public decimal AmountForOrder { get; set; }
      //  public decimal OrderPriceForBuyOrSelP { get; set; }
        public AccountType AccountType { get; set; }
       // public string ApplicationUserId { get; set; }
       // public ApplicationUser ApplicationUser { get; set; }
        public int PropUserId { get; set; }
        public PropUser PropUser { get; set; }
    }

}
