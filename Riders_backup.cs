using System;
using System.Collections.Generic;
using TourTrader.TO;
using System.Linq;

namespace TourTrader.Backup
{
    /// <summary>
    /// Riders is the internal 'database' class containing all information about the riders.
    /// </summary>
    public static class Riders
    {
        private static List<Rider> riders;
        private static Dictionary<long, Rider> ridersDict = new Dictionary<long, Rider>();
        private static List<long> discardedRiders = new List<long>();

        public class Rider
        {

            public double maxprice;                         // Sets the maximum price that we want to bid.
            public double minprice;                         // Sets the minumum price that we want to bid.
            public double startPrice;                       // Set the starting price of the bid.  
            public long selectionid;                        // The selectionID of the rider.
            public double latestMarketprice;                // The latest price settled in the market.    
            public double totalmarketamount_old;            // The total amount matched before updating.
            public double totalmarketamount;                // The total amount matched after updating.
            public double cumulative;                       // The cumulative overround up to this rider
            public string name;                             // The name of the rider
            public double pnl;                              // The profit or loss given that the rider wins
            public bool isInthemoney;                       // True if our price is higher than the market price.
            public bool isOpen;                             // True is we have an unmatched order on this rider. 
            public bool hasAutoOrder;                       // True if we have placed an auto order on this rider
            public double turnover;                         // Total amount matched on this rider * price of the rider.
            public double marketBid;                        // Highest bid price in the market.
            public double marketAsk;                        // Highest ask price in the market.
            public double myBid;                            // Our bid price.
            public double myAsk;                            // Our ask price.

            public List<CurrentOrderSummary> layorders;

            public Rider()
            {
                Update();
            }

            public void Update()
            {
               

            }

        }

        public static int Count()
        {
            return riders.Count;
        }

        public static Rider At(int i)
        {
            return riders[i];
        }

        public static Rider AtID(long selectionID_)
        {
            IEnumerable<Rider> select = from rider in riders where rider.selectionid == selectionID_ select rider;
            return select.ElementAt(0);
        }

        public static void discard(long SelectionID)
        {
            discardedRiders.Add(SelectionID);
        }

        /// <summary>
        /// Update rider data, remove discarded riders, sort and calculate some metrics.
        /// </summary>
        public static void Update()
        {
            try
            {
                extractAPI();
                removeDiscarded();
                sortbyOdds();
                calculateOverround();
            }
            catch { }
        }

        private static void extractAPI()
        {
            List<Runner> runners = new List<Runner>();                                           // data where the API writes into
            List<RunnerDescription> runnerDescription = new List<RunnerDescription>();           // Rider data where the API writes into
            List<MarketProfitAndLoss> runnerPNL = new List<MarketProfitAndLoss>();               // Rider data where the API writes into
            CurrentOrderSummaryReport orders = new CurrentOrderSummaryReport();

            ApiGet.connect(ref runners, ref runnerDescription, ref runnerPNL, ref orders);

            List<RunnerProfitAndLoss> marketPNL = runnerPNL.ElementAt(0).ProfitAndLosses;
            Dictionary<long, double> pnlIfWin = marketPNL.ToDictionary(t => t.SelectionId, t => t.IfWin);
            Dictionary<long, string> riderName = runnerDescription.ToDictionary(t => t.SelectionId, t => t.RunnerName);
            var openOrders = (from order in orders.CurrentOrders where order.SizeRemaining > 0 select order).ToList<CurrentOrderSummary>();

            // Reset rider properties.
            if (Riders.riders == null)
            {
                Riders.riders = new List<Rider>();

                for (int i = 0; i < runners.Count; i++)
                {
                    Riders.riders.Add(new Rider());
                }
            }
            else
            {
                foreach (Rider r in Riders.riders)
                {
                    r.myAsk = 0;
                    r.myBid = 0;
                    r.isOpen = false;
                    r.layorders = new List<CurrentOrderSummary>();
                    r.cumulative = 0;
                }
            }

            for (int i = 0; i < Math.Min(runners.Count, riders.Count); i++)
            {
                riders[i].selectionid = runners[i].SelectionId;
                riders[i].totalmarketamount_old = riders[i].totalmarketamount;
                riders[i].totalmarketamount = runners[i].TotalMatched;

                if (runners[i].LastPriceTraded.HasValue)
                {
                    riders[i].latestMarketprice = runners[i].LastPriceTraded.Value;
                }
                else
                {
                    riders[i].latestMarketprice = 10000;
                }

                riders[i].turnover = Math.Round(riders[i].totalmarketamount * riders[i].latestMarketprice);

                // Get market price by taking into account my own orders. 
                if (runners[i].ExchangePrices.AvailableToBack.Count > 0)
                {
                    if (runners[i].ExchangePrices.AvailableToBack[0].Price == riders[i].myBid && (runners[i].ExchangePrices.AvailableToBack[0].Price - riders[i].myBid >= 2))
                        riders[i].marketBid = runners[i].ExchangePrices.AvailableToBack[0].Price;
                    else
                        riders[i].marketBid = runners[i].ExchangePrices.AvailableToBack[1].Price;
                }

                if (runners[i].ExchangePrices.AvailableToLay.Count > 0)
                {
                    riders[i].marketAsk = runners[i].ExchangePrices.AvailableToLay[0].Price;
                }


                if (riderName.ContainsKey(riders[i].selectionid))
                {
                    riders[i].name = riderName[riders[i].selectionid];
                }

                if (pnlIfWin.ContainsKey(riders[i].selectionid))
                {
                    riders[i].pnl = pnlIfWin[riders[i].selectionid];
                }
            }

            for (int i = 0; i < openOrders.Count; i++)
            {
                Riders.AtID(Convert.ToInt32(openOrders[i].SelectionId)).isOpen = true;
                Riders.AtID(Convert.ToInt32(openOrders[i].SelectionId)).myBid = openOrders[i].PriceSize.Price;
                Riders.AtID(Convert.ToInt32(openOrders[i].SelectionId)).layorders = openOrders;
            }

            foreach (Rider r in Riders.riders)
            {
                if (r.myBid >= r.marketBid)
                    r.isInthemoney = true;
            }

        }

        private static void removeDiscarded()
        {
            riders = (from rider in riders where !discardedRiders.Contains(rider.selectionid) select rider).ToList<Rider>();
        }

        private static void sortbyOdds()
        {
            riders = riders.OrderBy(f => f.latestMarketprice).ToList<Rider>();
        }

        private static void calculateOverround()
        {
            for (int i = 0; i < Riders.riders.Count; i++)
            {

                for (int j = 0; j <= i; j++)
                {
                    Riders.riders[i].cumulative += 1 / (Riders.riders[j].latestMarketprice);
                }
            }
        }
    }
}

