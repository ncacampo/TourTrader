using System.Windows.Forms;
using System.Collections.Generic;
using TourTrader.TO;
using System.Drawing;

namespace TourTrader
{   
    public partial class Login : Form
    {
        private System.Windows.Forms.TextBox ApiKeyTextBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.ComboBox DropDownMenu;
        private List<MarketCatalogue> cyclingMarkets;
        
        public Login()
        {
            InitializeComponent();
        }

        private void ConnectButtonClick(object sender, System.EventArgs e)
        {
            BackEnd.sessionToken = ApiKeyTextBox.Text.ToString();
            if (ApiGet.getCyclingMarkets(ref cyclingMarkets))
            {
                BackEnd.connected2API = true;

                for (int i = 0; i < cyclingMarkets.Count; i++)
                    DropDownMenu.Items.Add(cyclingMarkets[i].Event.Name);

                ConnectionMessage.Text = "Connected to Betfair API";
                ChooseMarket.Text = "Select market, click start!";
            }
            else
            {
                ConnectionMessage.Text = "Failed to connect to Betfair API";
                ConnectionMessage.ForeColor = Color.Red;
            }
        }

        private void StartButtonClick(object sender, System.EventArgs e)
        {
            if (BackEnd.connected2API)
            {
                try
                {
                    BackEnd.marketID = cyclingMarkets.Find(f => f.Event.Name == DropDownMenu.SelectedItem.ToString()).MarketId;
                    this.Dispose();
                }
                catch
                { }
            }
        }
    }
}
