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
            PricingForm pricingForm = new PricingForm(this.rider.Text.ToString(),selectionID);
            pricingForm.StartPosition = FormStartPosition.CenterParent;
            pricingForm.ShowDialog();
        }

        private void clickStatusButton(object sender, EventArgs e)
        {
            BackEnd.clock.Stop();
            if (Riders.AtID(selectionID).layorders == null)
                return; 

            List<CancelInstruction> cancelInstructions = new List<CancelInstruction>();
            CancelInstruction cancelInstruction;

            for (int i = 0; i < Riders.AtID(selectionID).layorders.Count ; i++)
            {   
                cancelInstruction = new CancelInstruction();
                cancelInstruction.BetId = Riders.AtID(selectionID).layorders[i].BetId;
                cancelInstructions.Add(cancelInstruction);
            }

            for (int i = 0; i < Riders.AtID(selectionID).backorders.Count; i++)
            {
                cancelInstruction = new CancelInstruction();
                cancelInstruction.BetId = Riders.AtID(selectionID).backorders[i].BetId;
                cancelInstructions.Add(cancelInstruction);
            }

            ApiSet.CancelOrders(cancelInstructions);
            BackEnd.clock.Start();
        }

        private void clickLay(object sender, EventArgs e)
        {
            BackEnd.clock.Stop();
            List<PlaceInstruction> placeInstructions = new List<PlaceInstruction>();
            PlaceInstruction Instruction = new PlaceInstruction();
            LimitOrder order = new LimitOrder();

            order.Price = Utils.ToDouble(this.back.Text);
            order.Size = 2;

            Instruction.OrderType = OrderType.LIMIT;
            Instruction.LimitOrder = order;
            Instruction.Side = Side.LAY;

            Instruction.SelectionId = this.selectionID;

            placeInstructions.Add(Instruction);

            ApiSet.PlaceOrder(placeInstructions);
            BackEnd.clock.Start();
        }

        private void clickBack(object sender, EventArgs e)
        {
            BackEnd.clock.Stop();
            List<PlaceInstruction> placeInstructions = new List<PlaceInstruction>();
            PlaceInstruction Instruction = new PlaceInstruction();
            LimitOrder order = new LimitOrder();
            
            order.Price = Utils.ToDouble(this.lay.Text);
            order.Size = 5;
            
            Instruction.OrderType = OrderType.LIMIT;
            Instruction.LimitOrder = order;
            Instruction.Side = Side.BACK;

            Instruction.SelectionId = this.selectionID;

            placeInstructions.Add(Instruction);

            ApiSet.PlaceOrder(placeInstructions);
            BackEnd.clock.Start();
        }

        private void clickDiscard(object sender, EventArgs e)
        {
            BackEnd.clock.Stop();
            Riders.discard(selectionID);
            BackEnd.clock.Start();
        }

        private void doubleclickDiscard(object sender, EventArgs e)
        {
            BackEnd.clock.Stop();
            for (int i = controlRank ; i<Riders.Count()  ; i++) 
            Riders.discard(Riders.At(i).selectionid);
            BackEnd.clock.Start();
        }
    }   
}
