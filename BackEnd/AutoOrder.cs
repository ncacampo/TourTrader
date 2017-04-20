using System;
using TourTrader.TO;
using System.Collections.Generic;

namespace TourTrader
{   
    /// <summary>
    /// Handles the automatic pricing feature.
    /// </summary>
    static class AutoPrice
    {   
        /// <summary>
        /// Automatically overbids competitor prices according to manual set price maxima.
        /// </summary>
        public static void Update()
        {
            List<ReplaceInstruction> replaceInstructions = new List<ReplaceInstruction>();

            for (int i = 0; i < Riders.Count(); i++)
            {
                if (Riders.At(i).hasAutoOrder && Riders.At(i).layorders != null)
                {
                    for (int j = 0; j < Riders.At(i).layorders.Count; j++)
                    {
                        if (Riders.At(i).layorders[j].SizeRemaining >= 2 && Riders.At(i).layorders[j].PriceSize.Price < Riders.At(i).marketBid)
                        {
                            LimitOrder Order = new LimitOrder();
                            Order.Price = Utils.RoundPrice(Riders.At(i).marketBid + Utils.Increment(Riders.At(i).marketBid));
                            Order.Size = Convert.ToDouble(Riders.At(i).layorders[j].SizeRemaining);
                            ReplaceInstruction replaceInstruction = new ReplaceInstruction();
                            replaceInstruction.BetId = Riders.At(i).layorders[j].BetId;
                            replaceInstruction.NewPrice = Utils.RoundPrice(Riders.At(i).marketBid + Utils.Increment(Riders.At(i).marketBid));
                            replaceInstructions.Add(replaceInstruction);
                        }

                        if (Riders.At(i).layorders[j].SizeRemaining >= 2 && Riders.At(i).layorders[j].PriceSize.Price > Riders.At(i).marketBid)
                        {
                            LimitOrder Order = new LimitOrder();
                            Order.Price = Utils.RoundPrice(Riders.At(i).marketBid );
                            Order.Size = Convert.ToDouble(Riders.At(i).layorders[j].SizeRemaining);
                            ReplaceInstruction replaceInstruction = new ReplaceInstruction();
                            replaceInstruction.BetId = Riders.At(i).layorders[j].BetId;
                            replaceInstruction.NewPrice = Utils.RoundPrice(Riders.At(i).marketBid + Utils.Increment(Riders.At(i).marketBid));
                            replaceInstructions.Add(replaceInstruction);
                        }

                    }
                }
            }

            ApiSet.ReplaceOrder(replaceInstructions);

        } 


        /*
        public static void Write()
        {

            StreamWriter writer = new StreamWriter(@"C:\Users\gebruiker\Documents\TDF\Autobet\" + Program.marketID.ToString() + ".txt");

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
                StreamReader reader = new StreamReader(@"C:\Users\gebruiker\Documents\TDF\Autobet\" + Program.marketID.ToString() + ".txt");
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
        */
    }
}
