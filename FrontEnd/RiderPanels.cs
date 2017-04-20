using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;

namespace TourTrader
{   
    /// <summary>
    /// Container of all rider panels.
    /// </summary>
    class RiderPanels
    {
        static public List<RiderPanel> riderPanels = new List<RiderPanel>();
        static public HeaderPanel headerPanel = new HeaderPanel();

        static RiderPanels()
        {
            headerPanel.update();
            
            for (int j = 0; j < 4; j++) // 4(wide screen) or 3(small screen)
                for (int i = 0; i < 48; i++)  //48(wide screen) or 33(small screen)
                {
                    RiderPanel panel = new RiderPanel(i);
                    Point p = new Point(panel.Width * j, 25 + panel.Height * i);
                    panel.Location = p;
                    riderPanels.Add(panel);
                }
        }

        public static int Count()
        {
            return riderPanels.Count;
        }

        public static RiderPanel At(int i)
        {
            return riderPanels[i];
        }

        public static void Update()
        {
            headerPanel.update();

            for (int i = 0; i < Math.Min(riderPanels.Count, Riders.Count()); i++)
            {
                riderPanels[i].selectionID = Riders.At(i).selectionID;
                riderPanels[i].rider.Text = Riders.At(i).name.Split(' ')[Riders.At(i).name.Split(' ').Count() - 1];
                riderPanels[i].price.Text = Riders.At(i).latestMarketprice.ToString();
                riderPanels[i].PNL.Text = Math.Round(Riders.At(i).pnl).ToString();

                if (Riders.At(i).isOpen)
                {
                    riderPanels[i].StatusButton.BackColor = Color.Green;

                    if (Riders.At(i).isInthemoney)
                        riderPanels[i].StatusButton.BackColor = Color.Purple;
                }
                else
                {
                    riderPanels[i].StatusButton.BackColor = Color.Red;
                }

                riderPanels[i].overround.Text = Math.Round(Riders.At(i).overround, 2).ToString();
                riderPanels[i].turnover.Text = Math.Round(Riders.At(i).Turnover(),0).ToString();
                riderPanels[i].lay.Text = Riders.At(i).marketBid.ToString();
                riderPanels[i].back.Text = Riders.At(i).marketAsk.ToString();
                riderPanels[i].melay.Text = Riders.At(i).myBid.ToString();
                riderPanels[i].meback.Text = Riders.At(i).myAsk.ToString();

            }

            for (int i = Riders.Count(); i < riderPanels.Count; i++)
            {
                riderPanels[i].Dispose();
            }
        }
    }
}

