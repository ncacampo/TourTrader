using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using TourTrader.TO;

namespace TourTrader
{   
    /// <summary>
    /// Backend of the app. Does ETL on data extracted from the Betfair API.
    /// Part of the chain Backend <=> Riders <=> riderPanels <=> UserInterface
    /// </summary>
    class BackEnd
    {
        static int counter = 0;
        
        public static Dictionary<string, List<string>> All_Layorders = new Dictionary<string, List<string>>();    // All lay orders on a specific selectionID
        public static Dictionary<string, List<string>> All_Backorders = new Dictionary<string, List<string>>();   // All back orders on a specific selectionID

        private static JsonRpcClient apiClient = new JsonRpcClient(MainProgram.endPoint, MainProgram.appKey, MainProgram.sessionToken);

        public static List<long> discardedRiders = new List<long>();

        public BackEnd()
        {
            MainProgram.Clock.Tick += new EventHandler(clockTick);      // Every n seconds an API request is send and handled by clockTick
        }

        private static void clockTick(object o, EventArgs e)
        {
            clockeventRaised();
        }

        /// <summary>
        /// Updates the app every n seconds that the clock event is raised.   
        /// </summary>
        private static void clockeventRaised()
        {   
            Riders.get();
            placeOrders(ref apiClient);
            Analytics.go();
            doIO();
            updateRiderpanels();
        }

        private static void doIO()
        {
            if (counter == 5)
                Riders.Read();
            else if (counter % 5 == 0)
                Riders.Write();
        }


        /// <summary>
        /// Place orders on the API
        /// </summary>
        private static void placeOrders(ref JsonRpcClient client)
        {
            PlaceExecutionReport PlaceReport;

            if (MainProgram.placeInstructions.Count > 0)
            {
                PlaceReport = new PlaceExecutionReport();
                PlaceReport = client.placeOrders(MainProgram.marketID, MainProgram.placeInstructions);
                MainProgram.placeInstructions.Clear();
            }

            if (MainProgram.cancelInstructions.Count > 0)
            {
                client.cancelOrders(MainProgram.marketID, MainProgram.cancelInstructions);
                MainProgram.cancelInstructions.Clear();
            }

            if (counter % 5 == 0 && counter > 0)
            {
                foreach (Rider r in Riders.riders)
                    r.Update();
            }

            if (MainProgram.replaceInstructions.Count > 0)
            {
                client.replaceOrders(MainProgram.marketID, MainProgram.replaceInstructions);
                MainProgram.replaceInstructions.Clear();
            }
        }

        private static void updateRiderpanels()
        {
            for (int i = 0; i < Math.Min(MainProgram.riderPanels.Count, Riders.riders.Count); i++)
            {

                MainProgram.riderPanels[i].selectionID = Riders.riders[i].selectionid;
                MainProgram.riderPanels[i].rider.Text = Riders.riders[i].name.Split(' ')[Riders.riders[i].name.Split(' ').Count() - 1];
                MainProgram.riderPanels[i].price.Text = Riders.riders[i].lastmarketprice.ToString();
                MainProgram.riderPanels[i].PNL.Text = Math.Round(Riders.riders[i].pl).ToString();

                if (Riders.riders[i].open)
                {
                    MainProgram.riderPanels[i].StatusButton.BackColor = Color.Green;

                    if (Riders.riders[i].inthemoney)
                        MainProgram.riderPanels[i].StatusButton.BackColor = Color.Purple;
                }
                else
                    MainProgram.riderPanels[i].StatusButton.BackColor = Color.Red;
                MainProgram.riderPanels[i].overround.Text = Math.Round(Riders.riders[i].cumulative, 2).ToString();

                Char[] temp = new Char[Riders.riders[i].turnover.ToString().Length];
                temp = Riders.riders[i].turnover.ToString().ToCharArray();

                for (int k = 2; k < temp.Length; k++)
                {
                    temp[k] = '0';
                }

                String s = new String(temp);

                MainProgram.riderPanels[i].omzet.Text = s;
                MainProgram.riderPanels[i].lay.Text = Riders.riders[i].marketlay.ToString();
                MainProgram.riderPanels[i].back.Text = Riders.riders[i].marketback.ToString();
                MainProgram.riderPanels[i].melay.Text = Riders.riders[i].melay.ToString();
                MainProgram.riderPanels[i].meback.Text = Riders.riders[i].meback.ToString();

            }

            for (int i = Riders.riders.Count; i < MainProgram.riderPanels.Count; i++)
            {
                MainProgram.riderPanels[i].Dispose();
            }
        }
    }
}


