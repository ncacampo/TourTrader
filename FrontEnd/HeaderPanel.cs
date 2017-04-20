using System;
using System.Windows.Forms;
using System.Threading;

namespace TourTrader
{   
    /// <summary>
    /// Usercontrol that is screenwide on top of the userinterface. 
    /// </summary>
    public partial class HeaderPanel : UserControl
    {
        public HeaderPanel()
        {
            InitializeComponent();
        }

        private void click_CancelButton(object sender, EventArgs e)
        {
            BackEnd.clock.Stop();
            ApiSet.CancelAll();
            BackEnd.clock.Start();
        }

        private static void cancelAll()
        {   
            ThreadStart threadCancel = new ThreadStart(ThreadCancelAll);
            Thread thread = new Thread(threadCancel);
            thread.Start();
        }

        private static void ThreadCancelAll()
        {
            using (JsonRpcClient Client = new JsonRpcClient(BackEnd.endPoint, BackEnd.appKey, BackEnd.sessionToken))
            { Client.cancelAll(BackEnd.marketID); }
        }

        public void update()
        {
            this.expectedvalueLabel.Text = "E:" + Metrics.ExpectedProfit();
            this.kurtosisLabel.Text = "K:" + Metrics.kurtosis();
            this.standarddeviationLabel.Text = "V:" + Metrics.standardDeviation();
            this.latesttransactionLabel.Text = Metrics.LastTrade();
        }
    }
}


