using System;
using System.Collections.Generic;
using System.Drawing;
using TourTrader.TO;
using System.Windows.Forms;

namespace TourTrader
{
    static class MainProgram
    {
        public static System.Windows.Forms.Timer Clock;

        static void Main()
        {
            endPoint = @"https://api.betfair.com/exchange/betting";
            appKey = "YspYojHKnoBTstpb";
            sessionToken = "XKqE+2Jh2N74xYpIb2nYlFsoJLDwpQrMhWjpQXYWyrE=";
            marketID = "1.125814589"; 

            Clock = new System.Windows.Forms.Timer();
            Clock.Interval = 3000;
            Clock.Tick += new EventHandler(clockTick);
            Clock.Start();

            BackEnd backEnd = new BackEnd();

            initUI();   
        }

        public static void initUI()
        {
            userInterface = new MainForm();

            MainProgram.riderPanels = new List<RiderPanel>();

            for (int j = 0; j < 3; j++) // 4 of 3
                for (int i = 0; i < 33; i++)  // 48 of 33
                {
                    RiderPanel panel = new RiderPanel(i);
                    Point p = new Point(panel.Width * j, 25 + panel.Height * i);
                    panel.Location = p;
                    riderPanels.Add(panel);
                }

            for (int i = 0; i < riderPanels.Count; i++)
            {
                userInterface.Controls.Add(riderPanels[i]);
            }

            Application.Run(userInterface);
        }



        private static void clockTick(object o, EventArgs e)
        {   
            userInterface.Update();
        }

        public static MainForm userInterface;
        public static PricingForm betform;

        public static List<long> discardedRiders = new List<long>();

        public static List<RiderPanel> riderPanels;
        public static IList<PlaceInstruction> placeInstructions = new List<PlaceInstruction>();
        public static List<CancelInstruction> cancelInstructions = new List<CancelInstruction>();
        public static List<ReplaceInstruction> replaceInstructions = new List<ReplaceInstruction>();

        public static string endPoint;
        public static string appKey;
        public static string sessionToken;
        public static string marketID;
    }

}
