using System;
using System.Collections.Generic;

namespace TourTrader
{
    static class Analytics
    {
        public static void go()
        {
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

            MainProgram.userInterface.Sprint.Text = "S:" + Math.Round(P_sprint, 2).ToString() + "  " + Math.Round(PL_sprint, 2).ToString();


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

            MainProgram.userInterface.Punch.Text = "P" + Math.Round(P_punch, 2).ToString();

            // Long range

            /* Risk management */

            for (int i = 0; i < Riders.riders.Count; i++)
                if (!pdf.ContainsKey(Riders.riders[i].pl))
                    pdf.Add(Riders.riders[i].pl, 0);

            for (int i = 0; i < Riders.riders.Count; i++)
                pdf[Riders.riders[i].pl] += 1 / Riders.riders[i].lastmarketprice;

            double winst = 0;
            for (int i = 0; i < Riders.riders.Count; i++)
                winst += Riders.riders[i].pl / Riders.riders[i].lastmarketprice;

            double variance = 0;
            foreach (var d in pdf)
            {
                variance += d.Value * Math.Pow((d.Key - winst), 2);
            }

            double kurtosis = 0;
            foreach (var d in pdf)
            {
                kurtosis += d.Value * Math.Pow((d.Key - winst), 4);
            }

            kurtosis /= Math.Pow(variance, 2);

            MainProgram.userInterface.Profit.Text = "M:" + Math.Round(winst).ToString();
            MainProgram.userInterface.Variance.Text = "V:" + Math.Round(Math.Sqrt(variance)).ToString();
            MainProgram.userInterface.Kurtosis.Text = "K:" + Math.Round(kurtosis).ToString();

            /* End analytics */

            for (int i = 0; i < Riders.riders.Count; i++)
            {
                if (Riders.AtID(Riders.riders[i].selectionid).totalmarketamount_old != Riders.riders[i].totalmarketamount)
                {
                    MainProgram.userInterface.LastTrade.Text = Riders.riders[i].name + " " + Riders.riders[i].lastmarketprice.ToString();
                }
            }

        }

    }
}
