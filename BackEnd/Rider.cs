using System.Collections.Generic;
using System.Linq;
using TourTrader.TO;
using System.Diagnostics.Contracts;
using System;

namespace TourTrader
{       
        /// <summary>
        /// This object stores all the information associated with an rider.
        /// </summary>
        public class Rider
        {
            Runner runner;
            CurrentOrderSummaryReport orders;
            RunnerDescription runnerdescription;
            RunnerProfitAndLoss runnerPNL;

            public long selectionID { get; set; }           
            public bool hasAutoOrder { get; set; }                  // True if there is an AutoOrder set on the rider. 
            public double maxPrice { get; set; }                    // The max price we want to bid.
            public double minPrice { get; set; }                    // The min price we want to bid.
            public double overround { get; set; }                   // The cumulative overround of the rider
            public double latestMarketprice { get; private set; }   // The latest price settled in the market.    
            public double totalmarketamount { get; private set; }   // The total amount matched after updating.
            public string name { get; private set; }                // The name of the rider
            public double pnl { get; private set; }                 // The profit or loss given that the rider wins
            public bool isInthemoney { get; private set; }          // True if our price is higher than the market price.
            public bool isOpen { get; private set; }                // True is we have an unmatched order on this rider.                                           
            public double turnover { get; private set; }            // Total amount matched on this rider * price of the rider.
            public double marketBid { get; private set; }           // Highest bid price in the market.
            public double marketAsk { get; private set; }           // Highest ask price in the market.
            public double myBid { get; private set; }               // Our bid price.
            public double myAsk { get; private set; }               // Our ask price.

            public List<CurrentOrderSummary> layorders;
            public List<CurrentOrderSummary> backorders;
            
            public Rider(Runner runner, CurrentOrderSummaryReport orders, RunnerDescription runnerdescription, RunnerProfitAndLoss runnerPNL, double minPrice, double maxPrice)
            {
                this.runner = runner;
                this.orders = orders;
                this.runnerdescription = runnerdescription;
                this.runnerPNL = runnerPNL;
                this.minPrice = minPrice;
                this.maxPrice = maxPrice;
                selectionID = runner.SelectionId;
                latestMarketprice = LatestMarketPrice();
                totalmarketamount = TotalMarketAmount();
                name = Name();
                pnl = PNL();
                isInthemoney = IsInthemoney();
                isOpen = IsOpen();
                turnover = Turnover();
                marketBid = MarketBid();
                marketAsk = MarketAsk();
                myBid = MyBid();
                myAsk = MyAsk();
                layorders = LayOrders();
                backorders = BackOrders();
            }            
            public double LatestMarketPrice()
            { myAsk = 100; return runner.LastPriceTraded.HasValue ? runner.LastPriceTraded.Value : 10000; }
            public double TotalMarketAmount()
            { return runner.TotalMatched; }
            public double Turnover()
            { return runner.LastPriceTraded.HasValue ? (runner.LastPriceTraded.Value * runner.TotalMatched) : 0; }
            public string Name()
            { return runnerdescription.RunnerName; }
            public double PNL()
            { return runnerPNL.IfWin; }
            public double MyBid()
            {
                var myOrderList = (from order in orders.CurrentOrders
                                   where (order.SizeRemaining > 0 && Convert.ToInt64(order.SelectionId) == selectionID && order.Side == Side.LAY)
                                   select order.PriceSize.Price);
                return myOrderList.Count() > 0 ? myOrderList.Max() : 0;
            }
            public double MyAsk()
            {
                var myOrderList = (from order in orders.CurrentOrders
                                   where (order.SizeRemaining > 0 && order.SelectionId == selectionID.ToString() && order.Side == Side.BACK)
                                   select order.PriceSize.Price);
                return myOrderList.Count() > 0 ? myOrderList.Min() : 0;
            }
            public double MarketBid()
            {
                var marketBid = (from runner in runner.ExchangePrices.AvailableToBack
                                 where runner.Size > 2
                                 select runner.Price).ToList<double>();
                return marketBid.Count > 0 ? marketBid.Max() : 0;
            }
            public double MarketAsk()
            {
                List<double> marketAsk = (from runner in runner.ExchangePrices.AvailableToLay
                                          where runner.Size > 2
                                          select runner.Price).ToList<double>();
                return marketAsk.Count > 0 ? marketAsk.Min() : 0;
            }
            public bool IsInthemoney()
            {
                return MyBid() > MarketBid() ? true : false;
            }
            public bool IsOpen()
            {
                return MyBid() > 0 ? true : false;
            }
            public List<CurrentOrderSummary> LayOrders()
            {
                return (from orders_ in orders.CurrentOrders where orders_.SizeRemaining > 0 && orders_.Side == Side.LAY select orders_).ToList<CurrentOrderSummary>();
            }
            public List<CurrentOrderSummary> BackOrders()
            {
            return (from orders_ in orders.CurrentOrders where orders_.SizeRemaining > 0 && orders_.Side == Side.BACK select orders_).ToList<CurrentOrderSummary>();
            }
    }
    }



