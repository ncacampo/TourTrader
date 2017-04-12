using System;
using System.Collections.Generic;

namespace TourTrader
{
    static class Metrics
    {
        private static Dictionary<double, double> pdf;
        private static double expectedProfit_ = 0;
        private static double kurtosis_ = 0 ;
        private static double standardDeviation_ = 0;
        private static string lastTrade = "";

        static public string LastTrade()
        {
            return lastTrade;
        }

        static public double ExpectedProfit()
        {
            return Math.Round(expectedProfit_,2);
        }

        static public double standardDeviation()
        {
            return Math.Round(standardDeviation_,2);
        }

        static public double kurtosis() 
        {
            return Math.Round(kurtosis_,2);
        }

        static public void Init()
        {
            Update();
        }

        static public void Update()
        {
            pdf = new Dictionary<double, double>();

            for (int i = 0; i < Riders.Count(); i++)
                if (!pdf.ContainsKey(Riders.At(i).pnl))
                    pdf.Add(Riders.At(i).pnl, 0);

            for (int i = 0; i < Riders.Count(); i++)
                pdf[Riders.At(i).pnl] += 1 / Riders.At(i).latestMarketprice;

            updateExpectedProfit();
            updateStandardDeviation();
            updateKurtosis();
        }

        static private void updateKurtosis()
        {
            double kurtosis = 0;
            foreach (var d in pdf)
            {
                kurtosis += d.Value * Math.Pow((d.Key - expectedProfit_), 4);
            }

            kurtosis /= Math.Pow(standardDeviation_, 4);

            kurtosis_ = kurtosis;
        }

        static private void updateStandardDeviation()
        {
            double variance = 0;

            foreach (var d in pdf)
            {
                variance += d.Value * Math.Pow((d.Key - expectedProfit_), 2);
            }
            
            standardDeviation_ = Math.Sqrt(variance);
        }

        static private void updateExpectedProfit()
        {
            double expectedProfit = 0;
            for (int i = 0; i < Riders.Count(); i++)
                expectedProfit += Riders.At(i).pnl / Riders.At(i).latestMarketprice;

            expectedProfit_ = expectedProfit;
        }
    }
}


/*
            Dictionary<double, double> pdf = new Dictionary<double, double>();
            String[] climb = { "Froome", "Valverde", "Quintana", "Mollema", "Porte", "Aru", "Bardet", "Yates", "Daniel Martin", "Meintjes", "Rodriquez", "Kreuziger" };
            String[] sprint = { "Boonen", "Ewan", "Viviani", "Lay", "Demare", "Degenkolb", "Sagan", "Kristof", "Kittel", "Greipel", "Groenewegen", "Gaviria", "Cavendish", "Bouhanni", "Nizzolo" };
            HashSet<double> sprint_ids = new HashSet<double>();
            double P_sprint = 0;
            double PL_sprint = 0;
            foreach (string s in sprint)
                for (int i = 0; i < Riders.riders.Count; i++)
                {
                    if (Riders.riders[i].name.ToString().Contains(s))
                    {
                        P_sprint += 1 / Riders.riders[i].lastmarketprice;
                        //sprint_ids.Add(API_static.riders[i].selectionid);
                        PL_sprint += 1 / Riders.riders[i].lastmarketprice * Riders.riders[i].pl;
                    }
                }
            MainProgram.Frontend.Sprint.Text = "S:" + Math.Round(P_sprint, 2).ToString() + "  " + Math.Round(PL_sprint, 2).ToString();
            String[] punch = { "Avermaet", "Gilbert", "Valverde", "Vanmarcke", "Kwiatkowski", "Stybar", "Terpstra", "Ulissu", "Nibali", "Gallopin", "Kreuziger", "Costa", "Navardauksas", "Dumoulin", "Stuyven" };
            HashSet<double> punch_ids = new HashSet<double>();
            double P_punch = 0;
            foreach (string s in punch)
                for (int i = 0; i < Riders.riders.Count; i++)
                {
                    if (Riders.riders[i].name.ToString().Contains(s))
                    {
                        P_punch += 1 / Riders.riders[i].lastmarketprice;
                        //sprint_ids.Add(API_static.riders[i].selectionid);    
                    }
                }
            MainProgram.Frontend.Punch.Text = "P" + Math.Round(P_punch, 2).ToString();
            // Long range
    */
