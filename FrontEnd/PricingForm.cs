using System;
using System.Drawing;
using TourTrader.TO;
using System.Windows.Forms;
using System.Net;
using System.Collections.Generic;

namespace TourTrader
{
    public partial class PricingForm : Form
    {
        public long ID;
        
        public PricingForm(string riderName, long SelectionID)
        {
            BackEnd.clock.Stop();
            ID = SelectionID;
            InitializeComponent();
            this.NameLabel.Text = riderName;

            string stringRequest = "https://sportsiteexweb.betfair.com/betting/LoadRunnerInfoChartAction.do?marketId=";
            stringRequest += Program.marketID.ToString().Split('.')[1].ToString();
            stringRequest += "&";
            stringRequest += "selectionId=";
            stringRequest += this.ID.ToString();

            var request = WebRequest.Create(stringRequest);

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                Graph.Image = Bitmap.FromStream(stream);
            }

        }

        private void clickLayButton(object sender, EventArgs e)
        {
            List<PlaceInstruction> placeInstructions = new List<PlaceInstruction>();

            string price = MaxPriceBox.Text;
            string size = SizeBox.Text; ;

            PlaceInstruction placeInstruction = new PlaceInstruction();
            placeInstruction.OrderType = OrderType.LIMIT;

            LimitOrder order = new LimitOrder();
           
            placeInstruction.Side = Side.LAY;
            placeInstruction.SelectionId = ID;
            
            order.PersistenceType = PersistenceType.PERSIST;
            
            order.Price = Utils.RoundPrice(Utils.ToDouble(price));
            order.Size = Utils.ToDouble(size);

            placeInstruction.LimitOrder = order;

            placeInstructions.Add(placeInstruction);

            ApiSet.PlaceOrder(placeInstructions);

            BackEnd.clock.Start();
            this.Dispose();


        }

        private void clickButtonBack(object sender, EventArgs e)
        {
            string price = MaxPriceBox.Text;
            string size = SizeBox.Text; ;

            PlaceInstruction PlaceInstruction = new PlaceInstruction();
            PlaceInstruction.OrderType = OrderType.LIMIT;

            LimitOrder order = new LimitOrder();

            PlaceInstruction.Side = Side.BACK;
            PlaceInstruction.SelectionId = ID;

            order.PersistenceType = PersistenceType.PERSIST;

            order.Price = Utils.RoundPrice(Utils.ToDouble(price));
            order.Size = Utils.ToDouble(size);

            PlaceInstruction.LimitOrder = order;

            ApiSet.PlaceOrder(PlaceInstruction);

            BackEnd.clock.Start();
            this.Dispose();
        }
 
        private void clickEnter(object sender, EventArgs e)
        {
            Riders.AtID(ID).hasAutoOrder = true;
            Riders.AtID(ID).maxPrice = Utils.ToDouble(MaxPriceBox.Text);
            Riders.AtID(ID).minPrice = Utils.ToDouble(MinpriceBox.Text);
            
            string price = StartpriceBox.Text;
            string size = SizeBox.Text;

            PlaceInstruction instruction = new PlaceInstruction();
            instruction.OrderType = OrderType.LIMIT;

            LimitOrder order = new LimitOrder();

            instruction.Side = Side.LAY;
            instruction.SelectionId = ID;

            order.PersistenceType = PersistenceType.PERSIST;

            order.Price = Utils.ToDouble(price);
            order.Size = Convert.ToDouble(size);

            instruction.LimitOrder = order;

            ApiSet.PlaceOrder(instruction);

            BackEnd.clock.Start();
            this.Dispose();
        }

        private void Enter(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13)
            {
                if (StartpriceBox.Text == "")
                clickLayButton(sender, e);
                else
                clickEnter(sender, e);
            }
        }

        private void Enter2(object sender, KeyPressEventArgs e)
        {
            clickLayButton(sender, e);
        }

             
    }
}
