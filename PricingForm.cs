using System;
using System.Drawing;
using TourTrader.TO;
using System.Windows.Forms;
using System.Net;

namespace TourTrader
{
    public partial class PricingForm : Form
    {
        public long ID;
        
        public PricingForm(string riderName, long SelectionID)
        {
            MainProgram.Clock.Stop();
            ID = SelectionID;
            InitializeComponent();
            this.name.Text = riderName;

            string stringRequest = "https://sportsiteexweb.betfair.com/betting/LoadRunnerInfoChartAction.do?marketId=";
            stringRequest += MainProgram.marketID.ToString().Split('.')[1].ToString();
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

            string price = maximumprice.Text;
            string size = amountBox2.Text; ;

            PlaceInstruction placeInstruction = new PlaceInstruction();
            placeInstruction.OrderType = OrderType.LIMIT;

            LimitOrder order = new LimitOrder();
           
            placeInstruction.Side = Side.LAY;
            placeInstruction.SelectionId = ID;
            
            order.PersistenceType = PersistenceType.PERSIST;
            
            order.Price = Utils.ToDouble(price);
            order.Size = Convert.ToDouble(size);

            placeInstruction.LimitOrder = order;

            MainProgram.placeInstructions.Add(placeInstruction);

            MainProgram.Clock.Start();
            this.Dispose();

        }

        private void clickButtonBack(object sender, EventArgs e)
        {
            string price = maximumprice.Text;
            string size = amountBox2.Text; ;

            PlaceInstruction PlaceInstruction = new PlaceInstruction();
            PlaceInstruction.OrderType = OrderType.LIMIT;

            LimitOrder order = new LimitOrder();

            PlaceInstruction.Side = Side.BACK;
            PlaceInstruction.SelectionId = ID;

            order.PersistenceType = PersistenceType.PERSIST;

            order.Price = Utils.ToDouble(price);
            order.Size = Convert.ToDouble(size);

            PlaceInstruction.LimitOrder = order;

            MainProgram.placeInstructions.Add(PlaceInstruction);
            MainProgram.Clock.Start();
            this.Dispose();
        }
 
        private void clickEnter(object sender, EventArgs e)
        {
            for (int i = 0; i < Riders.riders.Count; i++)
            {
                if (Riders.riders[i].selectionid == ID)
                {
                    Riders.riders[i].autobet_ = true;
                    Riders.riders[i].maxprice = Utils.ToDouble(maximumprice.Text);
                    Riders.riders[i].minprice = Utils.ToDouble(minimumprice.Text);
                    Riders.riders[i].meanprice = Utils.ToDouble(startprice.Text);
                }
            }

            string price = startprice.Text;
            string size = amountBox2.Text;

            PlaceInstruction instruction = new PlaceInstruction();
            instruction.OrderType = OrderType.LIMIT;

            LimitOrder order = new LimitOrder();

            instruction.Side = Side.LAY;
            instruction.SelectionId = ID;

            order.PersistenceType = PersistenceType.PERSIST;

            order.Price = Utils.ToDouble(price);
            order.Size = Convert.ToDouble(size);

            instruction.LimitOrder = order;

            MainProgram.placeInstructions.Add(instruction);

            MainProgram.Clock.Start();
            this.Dispose();
        }

        private void Enter(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13)
            {
                if (startprice.Text == "")
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
