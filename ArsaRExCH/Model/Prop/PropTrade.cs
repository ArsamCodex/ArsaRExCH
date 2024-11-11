﻿using ArsaRExCH.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace ArsaRExCH.Model.Prop
{
    public class PropTrade
    {
        public int PropTradeId { get; set; }
        public DateTime TradeOpened { get; set; }
        public DateTime? TradeClosedDate { get; set; }

 
        public decimal OrderPriceOpened { get; set; }


        public decimal? OrderPriceClosed { get; set; }

 
        public int Leverage { get; set; }


        public decimal ProfitInCase { get; set; }

    
        public decimal FeeInCase { get; set; }

  
        public decimal LiquidationPrice { get; set; }

        public decimal? StopLoss { get; set; }

        public decimal? TakeProfit { get; set; }

        [Precision(18, 8)]
        [Required(ErrorMessage = "AmountForOrder is required")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "AmountForOrder must be greater than zero")]
        public decimal AmountForOrder { get; set; }

        public OrderStatus Status { get; set; }
        public AccountType AccountType { get; set; }
        public OrderTypeProp orderTypeProp { get; set; }
        public int PropUserId { get; set; }
        public PropUser PropUser { get; set; }
    }

}

