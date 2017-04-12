using System.Windows.Forms;

namespace TourTrader
{
    /// <summary>
    /// Backend of the app. Does ETL on data extracted from the Betfair API.
    /// Part of the chain Backend <=> Riders <=> riderPanels <=> UserInterface
    /// </summary>
    class BackEnd
    {
        public static FrontEnd frontEnd;
        static public Timer clock = new Timer();

        public static void Run()
        {
            OtherPanels.Init();
            frontEnd = new FrontEnd();
            Update();
            clock.Interval = 2;
            clock.Tick += Clock_Tick;
            clock.Start();
            Application.Run(frontEnd);
        }

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
            OtherPanels.Update();
            AutoOrder.Update();
        }

    }
}