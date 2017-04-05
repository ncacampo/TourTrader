using System;
using System.Collections.Generic;
using TourTrader.TO;
using System.Linq;
using System.IO;

namespace TourTrader
{
    static class Riders
    {
        public static List<Rider> riders;
        public static Dictionary<long, Rider> ridersDict;
        private static List<long> discardedRiders = new List<long>();

        public static void get()
        {
            extractAPI();
            removeDiscarded();
            sortbyOdds();
            calculateOverround();
        }

        public static void discardRider(long SelectionID)
        {
            discardedRiders.Add(SelectionID);
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

        public static void removeDiscarded()
        {
            riders = (from rider in riders where !discardedRiders.Contains(rider.selectionid) select rider).ToList<Rider>();
        }

        public static void sortbyOdds() 
        {
            riders = riders.OrderBy(f => f.lastmarketprice).ToList<Rider>();
        }

        public static void calculateOverround()
        {
            for (int i = 0; i < Riders.riders.Count; i++)
            {
                Riders.riders[i].cumulative = 0;
                for (int j = 0; j <= i; j++)
                {
                    Riders.riders[i].cumulative += 1 / (Riders.riders[j].lastmarketprice);
                }
            }
        }

        public static void extractAPI()
        {
            List<Runner> runners = new List<Runner>();                                           // data where the API writes into
            List<RunnerDescription> runnerDescription = new List<RunnerDescription>();           // Rider data where the API writes into
            List<MarketProfitAndLoss> runnerPNL = new List<MarketProfitAndLoss>();               // Rider data where the API writes into
            CurrentOrderSummaryReport orders = new CurrentOrderSummaryReport();

            API.connect(ref runners, ref runnerDescription, ref runnerPNL,ref orders);

                if (Riders.riders != null)
                {
                    foreach (Rider r in riders)
                    {
                        r.ReInit();
                    }
                }
                else
                {
                    Riders.riders = new List<Rider>();
                    Riders.ridersDict = new Dictionary<long, Rider>();

                    for (int i = 0; i < runners.Count; i++)
                    {
                        Riders.riders.Add(new Rider());
                    }
                }

            for (int i = 0; i < runners.Count; i++)
            {
                Riders.At(i).selectionid = runners[i].SelectionId;
                Riders.At(i).totalmarketamount_old = Riders.At(i).totalmarketamount;
                Riders.At(i).totalmarketamount = runners[i].TotalMatched;
                if (runners[i].LastPriceTraded.HasValue)
                {
                    Riders.At(i).lastmarketprice = runners[i].LastPriceTraded.Value;
                }
                else
                {
                    Riders.At(i).lastmarketprice = 10000;
                }
                Riders.At(i).turnover = Math.Round(Riders.At(i).totalmarketamount * Riders.At(i).lastmarketprice);

                if (runners[i].ExchangePrices.AvailableToBack.Count > 0)
                {
                    Riders.At(i).marketlay = runners[i].ExchangePrices.AvailableToBack[0].Price;
                }
                if (runners[i].ExchangePrices.AvailableToLay.Count > 0)
                {
                    Riders.At(i).marketback = runners[i].ExchangePrices.AvailableToLay[0].Price;
                }

                Riders.At(i).laymarketprice = runners[i].ExchangePrices.AvailableToBack;

                if (Riders.At(i).melay < Riders.At(i).marketlay)
                {
                    Riders.At(i).inthemoney = false;
                }
                else if (Riders.At(i).marketlay != 0)
                {
                    Riders.At(i).inthemoney = true;
                }
            }

            List<RunnerProfitAndLoss> marketPNL = runnerPNL.ElementAt(0).ProfitAndLosses;
            Dictionary<long, double> pnlIfWin = marketPNL.ToDictionary(t => t.SelectionId, t => t.IfWin);
            Dictionary<long, string> riderName = runnerDescription.ToDictionary(t => t.SelectionId, t => t.RunnerName);

            foreach(Rider r in riders) 
            {
                if (pnlIfWin.ContainsKey(r.selectionid))
                {
                    r.pl = pnlIfWin[r.selectionid];
                }
                if (riderName.ContainsKey(r.selectionid))
                {
                    r.name = riderName[r.selectionid];
                }
            }

        }

        public static void Write()
        {

            StreamWriter writer = new StreamWriter(@"C:\Users\gebruiker\Documents\TDF\Autobet\" + MainProgram.marketID.ToString() + ".txt");

            foreach (Rider r in riders)
            {
                if (r.autobet_ == true)
                {
                    string s = "";
                    s += r.selectionid + ",";
                    s += "2,";
                    s += r.maxprice;
                    writer.WriteLine(s);
                }
            }

            writer.Close();
        }

        public static void Read()
        {
            try
            {
                StreamReader reader = new StreamReader(@"C:\Users\gebruiker\Documents\TDF\Autobet\" + MainProgram.marketID.ToString() + ".txt");
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    ridersDict[Convert.ToInt64(line.Split(',')[0])].autobet_ = true;
                    ridersDict[Convert.ToInt64(line.Split(',')[0])].minprice = Convert.ToDouble(line.Split(',')[1]);
                    ridersDict[Convert.ToInt64(line.Split(',')[0])].maxprice = Convert.ToDouble(line.Split(',')[2]);
                }
                reader.Close();
            }
            catch
            {
            }
        }
      }
    }
    



