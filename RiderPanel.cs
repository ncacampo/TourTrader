using System;
using System.Collections.Generic;
using TourTrader.TO;
using System.Windows.Forms;

namespace TourTrader
{
    public partial class RiderPanel : UserControl
    {
        protected int controlRank;
        
        public long selectionID;

        public RiderPanel(int n)
        {  
            InitializeComponent();
            controlRank = n;
        }
       
        private void clickRider(object sender, EventArgs e)
        {   
            MainProgram.betform = new PricingForm(this.rider.Text.ToString(),selectionID);
            MainProgram.betform.ShowDialog();
        }

        private void clickStatusButton(object sender, EventArgs e)
        {
            CancelInstruction cancel;

            if(BackEnd.All_Layorders.ContainsKey(Convert.ToString(selectionID)))
            {
            for (int i = 0; i < BackEnd.All_Layorders[Convert.ToString(selectionID)].Count; i++)
            {
                cancel = new CancelInstruction();
                cancel.BetId = BackEnd.All_Layorders[Convert.ToString(selectionID)][i];
                MainProgram.cancelInstructions.Add(cancel);
            }
            }

            if (BackEnd.All_Backorders.ContainsKey(Convert.ToString(selectionID)))
            {
                for (int i = 0; i < BackEnd.All_Backorders[Convert.ToString(selectionID)].Count; i++)
                {
                    cancel = new CancelInstruction();
                    cancel.BetId = BackEnd.All_Backorders[Convert.ToString(selectionID)][i];
                    MainProgram.cancelInstructions.Add(cancel);
                }
            }   
        }

        private void clickBack(object sender, EventArgs e)
        {
            List<PlaceInstruction> list = new List<PlaceInstruction>();
            PlaceInstruction Instruction = new PlaceInstruction();
            LimitOrder order = new LimitOrder();

            order.Price = Utils.ToDouble(this.back.Text);
            order.Size = 2;

            Instruction.OrderType = OrderType.LIMIT;
            Instruction.LimitOrder = order;
            Instruction.Side = Side.LAY;

            Instruction.SelectionId = this.selectionID;

            list.Add(Instruction);

            JsonRpcClient Client_ = new JsonRpcClient(MainProgram.endPoint, MainProgram.appKey, MainProgram.sessionToken);
            Client_.placeOrders(MainProgram.marketID, list);
            MainProgram.placeInstructions.Clear();
        }

        private void clickLay(object sender, EventArgs e)
        {
            List<PlaceInstruction> list = new List<PlaceInstruction>();
            PlaceInstruction Instruction = new PlaceInstruction();
            LimitOrder order = new LimitOrder();
            
            order.Price = Utils.ToDouble(this.lay.Text);
            order.Size = 5;
            
            Instruction.OrderType = OrderType.LIMIT;
            Instruction.LimitOrder = order;
            Instruction.Side = Side.BACK;

            Instruction.SelectionId = this.selectionID;

            list.Add(Instruction);

            JsonRpcClient Client_ = new JsonRpcClient(MainProgram.endPoint, MainProgram.appKey, MainProgram.sessionToken);
            Client_.placeOrders(MainProgram.marketID, list);
            MainProgram.placeInstructions.Clear();
        }

        private void clickDiscard(object sender, EventArgs e)
        {  
            MainProgram.discardedRiders.Add(selectionID);
        }

        private void doubleclickDiscard(object sender, EventArgs e)
        {   
            for(int i = controlRank ; i<Riders.riders.Count  ; i++) 
            MainProgram.discardedRiders.Add(Riders.riders[i].selectionid);    
        }
    }   
}
