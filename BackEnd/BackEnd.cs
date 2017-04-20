using System.Windows.Forms;

namespace TourTrader
{
    /// <summary>
    /// Backend of the app. Does ETL on data extracted from the Betfair API.
    /// Part of the chain Backend <=> Riders <=> riderPanels <=> UserInterface
    /// </summary>
    public static class BackEnd
    {
        public static FrontEnd frontEnd;
        static public Timer clock = new Timer();
        public static string endPoint { get; private set; }
        public static string appKey { get; private set; }
        public static string sessionToken;
        public static string marketID;
        public static bool connected2API = false;

        /// <summary>
        /// Starts the backend and indirectly the UI to run.
        /// </summary>
        public static void Run()
        {
            if (!connected2API)
                Application.Exit();

            endPoint = @"https://api.betfair.com/exchange/betting";
            appKey = "YspYojHKnoBTstpb";
            
            Login login = new Login();
            login.ShowDialog();

            frontEnd = new FrontEnd();
            Update();
            clock.Interval = 2;
            clock.Tick += Clock_Tick;
            clock.Start();
            Application.Run(frontEnd);
        }

        /// <summary>
        /// Every n seconds a timer event is raised to receive an update from the API.
        /// </summary>
        public static void Clock_Tick(object sender, System.EventArgs e)
        {
            Update();
        }

        /// <summary>
        /// Updates the app every n seconds that the clock event is raised.   
        /// </summary>
        private static void Update()
        {   
            frontEnd.Update();
            Riders.get();           
            RiderPanels.Update();
            Metrics.Update();
            AutoPrice.Update();
        }

    }
}