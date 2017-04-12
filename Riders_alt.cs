using System.Collections.Generic;
using TourTrader.TO;
using System.Linq;
using System.Diagnostics.Contracts;

namespace TourTrader
{
        public class Rider_alt
        {
            Runner runner;
            CurrentOrderSummaryReport orders;
            RunnerDescription runnerdescription;
            RunnerProfitAndLoss runnerPNL;

            public long selectionID { get; private set; }
            public bool hasAutoOrder{ get; set; }
            public double maxPrice { get; set; }
            public double minPrice { get; set; }
            public double startPrice { get; set; }
            public double cumulative { get; set; }
            
            public Rider_alt(Runner runner,CurrentOrderSummaryReport orders, RunnerDescription runnerdescription, RunnerProfitAndLoss runnerPNL)
            {
            this.runner = runner;
            this.orders = orders;
            this.runnerdescription = runnerdescription;
            this.runnerPNL = runnerPNL;
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
            { var myOrderList = (from order in orders.CurrentOrders
                     where (order.SizeRemaining > 0 && order.SelectionId == selectionID.ToString() && order.Side == Side.LAY)
                     select order.PriceSize.Price);
                return myOrderList == null ? myOrderList.Max() : 0; 
            }
                
            [Pure]
            public double MyAsk()
            {   var myOrderList = (from order in orders.CurrentOrders
                                   where (order.SizeRemaining > 0 && order.SelectionId == selectionID.ToString() && order.Side == Side.BACK)
                                   select order.PriceSize.Price);
                return myOrderList == null ? myOrderList.Min() : 10000;
            }
            [Pure]
            public double MarketBid()
            {
                return (from runner in runner.ExchangePrices.AvailableToBack
                        where runner.Size > 2
                        select runner.Price).Max();                      
            }
            [Pure]
            public double MarketAsk()
            {
                return (from runner in runner.ExchangePrices.AvailableToLay
                        where runner.Size > 2
                        select runner.Price).Min();
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
        }

    }


        

