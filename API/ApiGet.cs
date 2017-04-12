using System.Collections.Generic;
using System.Linq;
using TourTrader.TO;

namespace TourTrader
{   
    public static class ApiGet
    {
        private static JsonRpcClient apiClient = new JsonRpcClient(Program.endPoint, Program.appKey, Program.sessionToken);

        public static void connect(ref List<Runner> runners, ref List<RunnerDescription> runnerDescription, ref List<MarketProfitAndLoss> runnerPNL, ref CurrentOrderSummaryReport orders)
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
            set.Add(Program.marketID);
            marketFilter.MarketIds = set;

            var marketSort = MarketSort.FIRST_TO_START;

            ISet<MarketProjection> marketProjections = new HashSet<MarketProjection>();
            marketProjections.Add(MarketProjection.RUNNER_DESCRIPTION);

            List<string> marketIds = new List<string>();
            marketIds.Add(Program.marketID);

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

        /*
        public static void connect(ref List<Runner> runners)
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
        */

    }
}
