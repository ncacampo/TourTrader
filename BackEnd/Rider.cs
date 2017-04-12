using System.Collections.Generic;
using System.Linq;
using TourTrader.TO;
using System.Diagnostics.Contracts;
using System;

namespace TourTrader
{
        public class Rider
        {
            Runner runner;
            CurrentOrderSummaryReport orders;
            RunnerDescription runnerdescription;
            RunnerProfitAndLoss runnerPNL;

            public long selectionID { get; set; }
            public bool hasAutoOrder { get; set; }
            public double maxPrice { get; set; }
            public double minPrice { get; set; }
            public double cumulative { get; set; }
            
            public long selectionid;                        // The selectionID of the rider.
            public double latestMarketprice;                // The latest price settled in the market.    
            public double totalmarketamount_old;            // The total amount matched before updating.
            public double totalmarketamount;                // The total amount matched after updating.
                                                            // The cumulative overround up to this rider
            public string name;                             // The name of the rider
            public double pnl;                              // The profit or loss given that the rider wins
            public bool isInthemoney;                       // True if our price is higher than the market price.
            public bool isOpen;                             // True is we have an unmatched order on this rider. 
                                                            // True if we have placed an auto order on this rider
            public double turnover;                         // Total amount matched on this rider * price of the rider.
            public double marketBid;                        // Highest bid price in the market.
            public double marketAsk;                        // Highest ask price in the market.
            public double myBid;                            // Our bid price.
            public double myAsk;                            // Our ask price.

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

            [Pure]
            public double LatestMarketPrice()
            { return runner.LastPriceTraded.HasValue ? runner.LastPriceTraded.Value : 10000; }
            [Pure]
            public double TotalMarketAmount()
            { return runner.TotalMatched; }
            [Pure]
            public double Turnover()
            { return runner.LastPriceTraded.HasValue ? (runner.LastPriceTraded.Value * runner.TotalMatched) : 0; }
            [Pure]
            public string Name()
            { return runnerdescription.RunnerName; }
            [Pure]
            public double PNL()
            { return runnerPNL.IfWin; }
            [Pure]
            public double MyBid()
            {
                var myOrderList = (from order in orders.CurrentOrders
                                   where (order.SizeRemaining > 0 && Convert.ToInt64(order.SelectionId) == selectionID && order.Side == Side.LAY)
                                   select order.PriceSize.Price);
                return myOrderList.Count() > 0 ? myOrderList.Max() : 0;
            }

            [Pure]
            public double MyAsk()
            {
                var myOrderList = (from order in orders.CurrentOrders
                                   where (order.SizeRemaining > 0 && order.SelectionId == selectionID.ToString() && order.Side == Side.BACK)
                                   select order.PriceSize.Price);
                return myOrderList.Count() > 0 ? myOrderList.Min() : 0;
            }
            [Pure]
            public double MarketBid()
            {
                var marketBid = (from runner in runner.ExchangePrices.AvailableToBack
                                 where runner.Size > 2
                                 select runner.Price).ToList<double>();
                return marketBid.Count > 0 ? marketBid.Max() : 0;
            }
            [Pure]
            public double MarketAsk()
            {
                List<double> marketAsk = (from runner in runner.ExchangePrices.AvailableToLay
                                          where runner.Size > 2
                                          select runner.Price).ToList<double>();
                return marketAsk.Count > 0 ? marketAsk.Min() : 0;
            }

            [Pure]
            public bool IsInthemoney()
            {
                return MyBid() > MarketBid() ? true : false;
            }
            [Pure]
            public bool IsOpen()
            {
                return MyBid() > 0 ? true : false;
            }
            [Pure]
            public List<CurrentOrderSummary> LayOrders()
            {
                return (from orders_ in orders.CurrentOrders where orders_.SizeRemaining > 0 && orders_.Side == Side.LAY select orders_).ToList<CurrentOrderSummary>();
            }
            [Pure]
            public List<CurrentOrderSummary> BackOrders()
            {
            return (from orders_ in orders.CurrentOrders where orders_.SizeRemaining > 0 && orders_.Side == Side.BACK select orders_).ToList<CurrentOrderSummary>();
            }
    }
    }



