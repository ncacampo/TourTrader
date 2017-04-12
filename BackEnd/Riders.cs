using System.Collections.Generic;
using TourTrader.TO;
using System.Linq;
using System;

namespace TourTrader
{
    /// <summary>
    /// Riders is the internal 'database' class containing all information about the riders.
    /// </summary>
    public static class Riders
    {
        public static Rider[] riders;
        private static Dictionary<long, Rider> ridersDict = new Dictionary<long, Rider>();
        private static HashSet<long> discardedRiders = new HashSet<long>();

        private static List<Runner> runners = new List<Runner>();                                           // data where the API writes into
        private static List<RunnerDescription> runnerDescription = new List<RunnerDescription>();           // Rider data where the API writes into
        private static List<MarketProfitAndLoss> marketPNL = new List<MarketProfitAndLoss>();               // Rider data where the API writes into
        private static CurrentOrderSummaryReport orders = new CurrentOrderSummaryReport();

        public static int Count()
        {
            return riders.Length;
        }

        public static Rider At(int i)
        {
            return riders[i];
        }

        public static Rider AtID(long selectionID_)
        {
            return riders.ToList<Rider>().Find(f => f.selectionID == selectionID_);
        }

        public static void discard(long SelectionID)
        {
            discardedRiders.Add(SelectionID);
        }

        public static void get()
        {
            try
            {
                ApiGet.connect(ref runners, ref runnerDescription, ref marketPNL, ref orders);

                runners = runners.FindAll(f => !discardedRiders.Contains(f.SelectionId)).OrderBy(f => f.SelectionId).ToList<Runner>();
                runnerDescription = runnerDescription.FindAll(f => !discardedRiders.Contains(f.SelectionId)).OrderBy(f => f.SelectionId).ToList<RunnerDescription>();
                var runnerPNL = marketPNL[0].ProfitAndLosses.FindAll(f => !discardedRiders.Contains(f.SelectionId)).OrderBy(f => f.SelectionId).ToList<RunnerProfitAndLoss>();

                Riders.riders = new Rider[Math.Min(Math.Min(runners.Count, runnerDescription.Count), runnerPNL.Count)];

                for (int i = 0; i < Math.Min(Math.Min(runners.Count, runnerDescription.Count), runnerPNL.Count); i++)
                {
                    Riders.riders[i] = new Rider(runners[i], orders, runnerDescription[i], runnerPNL[i],
                        (riders[i] == null) ? 0 : riders[i].minPrice, (riders[i] == null) ? 0 : riders[i].maxPrice);
                }

                riders = riders.OrderBy(f => f.latestMarketprice).ToArray<Rider>();

                for (int i = 0; i < riders.Count(); i++)
                    riders[i].cumulative = riders.Take(i).Sum(f => 1 / f.latestMarketprice);
            }
            catch { }
        }
    }
}

