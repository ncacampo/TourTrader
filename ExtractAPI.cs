using System;
using System.Collections.Generic;
using System.Linq;
using TourTrader.TO;

namespace TourTrader
{

    class ExtractAPI
    {
        static int counter = 0;
        private static List<Runner> runners;                             // data where the API writes into
        private static List<RunnerDescription> runnerDescription;        // Rider data where the API writes into
        private static List<MarketProfitAndLoss> runnerPNL;              // Rider data where the API writes into
        private static CurrentOrderSummaryReport orders;                 // Order data where the API writes into

        public static Dictionary<string, List<string>> All_Layorders = new Dictionary<string, List<string>>();    // All lay orders on a specific selectionID
        public static Dictionary<string, List<string>> All_Backorders = new Dictionary<string, List<string>>();   // All back orders on a specific selectionID

        private static JsonRpcClient apiClient = new JsonRpcClient(MainProgram.endPoint, MainProgram.appKey, MainProgram.sessionToken);

        public static void go()
        {
            connectAPI();
            processOrders();
            processRunners_();
            processRunners();
            processRunnersPNL();
            sortnDiscard();
       }

        /// <summary>
        /// Connect to the API and retrieve 4 different objects: Runners_,Runners,RunnersPL and Orders
        /// </summary>
        public static void connectAPI()
        {
            PriceData pricedata1 = PriceData.EX_ALL_OFFERS;
            PriceData pricedata2 = PriceData.EX_TRADED;
            PriceData pricedata3 = PriceData.EX_BEST_OFFERS;

            HashSet<PriceData> prijsdata = new HashSet<PriceData>();
            prijsdata.Add(pricedata1);
            prijsdata.Add(pricedata2);
            prijsdata.Add(pricedata3);

            PriceProjection priceprojection = new PriceProjection();
            OrderProjection orderprojection = new OrderProjection();
            MatchProjection matchprojection = new MatchProjection();

            orderprojection = OrderProjection.ALL;
            matchprojection = MatchProjection.ROLLED_UP_BY_AVG_PRICE;
            priceprojection.PriceData = prijsdata;

            MarketFilter marketFilter = new MarketFilter();

            ISet<string> set = new HashSet<string>();
            set.Add(MainProgram.marketID);
            marketFilter.MarketIds = set;

            var marketSort = MarketSort.FIRST_TO_START;

            ISet<MarketProjection> marketProjections = new HashSet<MarketProjection>();
            marketProjections.Add(MarketProjection.RUNNER_DESCRIPTION);

            List<string> marketIds = new List<string>();
            marketIds.Add(MainProgram.marketID);

            try
            {
                runners = apiClient.listMarketBook(marketIds, priceprojection, orderprojection, matchprojection)[0].Runners;

            }
            catch { }
            try
            {
                runnerDescription = apiClient.listMarketCatalogue(marketFilter, marketProjections, marketSort)[0].Runners;
            }
            catch { }
            try
            {
                runnerPNL = apiClient.listMarketProfitAndLoss(marketIds, true, true, true).ToList<MarketProfitAndLoss>();
            }
            catch { }
            try
            {
                orders = apiClient.listCurrentOrders();
            }
            catch { }
        }

        /// <summary>
        /// Process data from runners into riders and ridersDict
        /// </summary>
        private static void processRunners_()
        {   
            for (int i = 0; i < runners.Count; i++)
            {
                if (counter == 0 || !Riders.ridersDict.ContainsKey(runners[i].SelectionId))
                {
                    Riders.riders[i].selectionid = runners[i].SelectionId;
                    Riders.ridersDict[Riders.riders[i].selectionid] = Riders.riders[i];
                }
                else
                {
                    Riders.ridersDict[runners[i].SelectionId].totalmarketamount_old = Riders.ridersDict[runners[i].SelectionId].totalmarketamount;
                    Riders.ridersDict[runners[i].SelectionId].totalmarketamount = runners[i].TotalMatched;
                    if (runners[i].LastPriceTraded.HasValue)
                        Riders.ridersDict[runners[i].SelectionId].lastmarketprice = runners[i].LastPriceTraded.Value;
                    else
                        Riders.ridersDict[runners[i].SelectionId].lastmarketprice = 10000;
                    if (runners[i].Orders != null)
                    {
                        for (int k = 0; k < runners[i].Orders.Count; k++)
                        {
                            if (runners[i].Orders[k].SizeRemaining != 0 && runners[i].Orders[k].Side == Side.LAY)
                                Riders.ridersDict[runners[i].SelectionId].melay = runners[i].Orders[k].Price;
                            if (runners[i].Orders[k].SizeRemaining != 0 && runners[i].Orders[k].Side == Side.BACK)
                                Riders.ridersDict[runners[i].SelectionId].meback = runners[i].Orders[k].Price;
                        }
                    }

                    Riders.ridersDict[runners[i].SelectionId].turnover = Math.Round(runners[i].TotalMatched * Riders.ridersDict[runners[i].SelectionId].lastmarketprice);

                    if (runners[i].ExchangePrices.AvailableToBack.Count > 0)
                    {
                        if (runners[i].ExchangePrices.AvailableToBack[0].Size > 0)
                            Riders.ridersDict[runners[i].SelectionId].marketlay = runners[i].ExchangePrices.AvailableToBack[0].Price;
                        else if (runners[i].ExchangePrices.AvailableToBack.Count > 1 && runners[i].ExchangePrices.AvailableToBack[1].Size > 1.9)
                            Riders.ridersDict[runners[i].SelectionId].marketlay = runners[i].ExchangePrices.AvailableToBack[1].Price;
                        else if (runners[i].ExchangePrices.AvailableToBack.Count > 2 && runners[i].ExchangePrices.AvailableToBack[2].Size > 1.9)
                            Riders.ridersDict[runners[i].SelectionId].marketlay = runners[i].ExchangePrices.AvailableToBack[2].Price;
                    }
                    if (runners[i].ExchangePrices.AvailableToLay.Count > 0)
                        Riders.ridersDict[runners[i].SelectionId].marketback = runners[i].ExchangePrices.AvailableToLay[0].Price;

                    Riders.ridersDict[runners[i].SelectionId].laymarketprice = runners[i].ExchangePrices.AvailableToBack;

                    Riders.ridersDict[runners[i].SelectionId].open = false;

                    if (Riders.ridersDict[runners[i].SelectionId].melay < Riders.ridersDict[runners[i].SelectionId].marketlay)
                    {
                        Riders.ridersDict[runners[i].SelectionId].inthemoney = false;
                    }
                    else if (Riders.ridersDict[runners[i].SelectionId].marketlay != 0)
                    {
                        Riders.ridersDict[runners[i].SelectionId].inthemoney = true;
                    }
                }
            }
            counter++;
        }

        private static void processRunners()
        {
            for (int i = 0; i < runnerDescription.Count; i++)
                Riders.ridersDict[runnerDescription[i].SelectionId].name = runnerDescription[i].RunnerName;
        }

        private static void processRunnersPNL()
        {
            for (int j = 0; j < runnerPNL[0].ProfitAndLosses.Count; j++)
                Riders.ridersDict[runnerPNL[0].ProfitAndLosses[j].SelectionId].pl = runnerPNL[0].ProfitAndLosses[j].IfWin;
        }


        private static void processOrders()
        {
            if (orders != null && orders.CurrentOrders != null)
            {
                for (int i = 0; i < orders.CurrentOrders.Count; i++)
                {
                    if (orders.CurrentOrders != null && orders.CurrentOrders[i].SizeRemaining == 0)
                    {
                        orders.CurrentOrders.RemoveAt(i);
                    }
                }

                for (int k = 0; k < orders.CurrentOrders.Count; k++)
                {
                    if (orders.CurrentOrders[k].MarketId == MainProgram.marketID && orders.CurrentOrders[k].SizeRemaining != 0)
                    {
                        if (orders.CurrentOrders[k].Side == Side.LAY)
                        {
                            All_Layorders[orders.CurrentOrders[k].SelectionId] = new List<string>();
                        }
                        else
                            All_Backorders[orders.CurrentOrders[k].SelectionId] = new List<string>();
                    }
                }

                for (int k = 0; k < orders.CurrentOrders.Count; k++)
                {
                    if (orders.CurrentOrders[k].MarketId == MainProgram.marketID && orders.CurrentOrders[k].SizeRemaining != 0)
                    {
                        if (orders.CurrentOrders[k].Side == Side.LAY)
                        {
                            All_Layorders[orders.CurrentOrders[k].SelectionId].Add(orders.CurrentOrders[k].BetId);
                        }
                        else
                        {
                            All_Backorders[orders.CurrentOrders[k].SelectionId].Add(orders.CurrentOrders[k].BetId);
                        }

                    }
                }

                for (int i = 0; i < orders.CurrentOrders.Count; i++)
                {
                    if (orders.CurrentOrders[i].MarketId == MainProgram.marketID && orders.CurrentOrders[i].SizeRemaining != 0)
                    {
                        if (orders.CurrentOrders[i].Side == Side.LAY)
                        {
                            Riders.ridersDict[(long)Convert.ToDouble(orders.CurrentOrders[i].SelectionId)].layorders_by_betID = new Dictionary<string, PriceSize>();
                            Riders.ridersDict[(long)Convert.ToDouble(orders.CurrentOrders[i].SelectionId)].layorders_by_betID[orders.CurrentOrders[i].BetId] = orders.CurrentOrders[i].PriceSize;
                            Riders.ridersDict[(long)Convert.ToDouble(orders.CurrentOrders[i].SelectionId)].open = true;
                        }
                    }
                }
            }
        }


        public static void sortnDiscard()
        {
            Riders.riders = Riders.ridersDict.Values.ToList().OrderBy(f => f.lastmarketprice).ToList<Rider>();

            for (int j = 0; j < MainProgram.discardedRiders.Count; j++)
                for (int i = 0; i < Riders.riders.Count; i++)
                {
                    if (Riders.riders[i].selectionid == MainProgram.discardedRiders[j])
                        Riders.riders.RemoveAt(i);
                }

            for (int i = 0; i < Riders.riders.Count; i++)
            {
                Riders.riders[i].cumulative = 0;
                for (int j = 0; j <= i; j++)
                {
                    Riders.riders[i].cumulative += 1 / (Riders.riders[j].lastmarketprice);
                }
            }
        }
    }
}
