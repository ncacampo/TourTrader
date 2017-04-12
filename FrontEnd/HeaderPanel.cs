using System;
using System.Windows.Forms;
using System.Threading;

namespace TourTrader
{
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
            using (JsonRpcClient Client = new JsonRpcClient(Program.endPoint, Program.appKey, Program.sessionToken))
            { Client.cancelAll(Program.marketID); }
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


