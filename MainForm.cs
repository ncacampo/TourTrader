using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TourTrader
{
    public partial class MainForm : Form
    {
        public MainForm()
        {   
            InitializeComponent();
        }

        private void clickCancelAll(object sender, EventArgs e)
        {
            using (JsonRpcClient Client = new JsonRpcClient(MainProgram.endPoint, MainProgram.appKey, MainProgram.sessionToken))
            { Client.cancelAll(MainProgram.marketID); }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

    }
}
