using System;
using System.Collections.Generic;
using System.Linq;
using TourTrader.TO;

namespace TourTrader
{
    public class Rider
    {   
        public double maxprice;
        public double minprice;
        public double meanprice;

        public long selectionid;

        public List<PriceSize> laymarketprice;
        public List<PriceSize> backmarketprice;

        public double lastmarketprice;
        public double totalmarketamount_old;

        public double totalmarketamount;
        public double cumulative;
        public string name;
        public double pl;
        public double order_amount;
        public double order_price;
        public bool inthemoney;
        public bool open;

        public bool autobet_;

        public double turnover;
        public double marketlay;
        public double marketback;
        public double melay;
        public double meback;

        public Dictionary<string, PriceSize> layorders_by_betID;

        ReplaceInstruction replace;

        public void ReInit()
        {
            laymarketprice = null;
            backmarketprice = null;
            lastmarketprice = 0;
            cumulative = 0;
            pl = 0;
            order_amount = 0;
            order_price = 0;
            inthemoney = false;
            open = false;
            turnover = 0;
            marketlay = 0;
            marketback = 0;
            melay = 0;
            meback = 0;
        }

        public Rider()
        {

            layorders_by_betID = new Dictionary<string, PriceSize>();
        }

        public void Update()
        {
            if (!autobet_ || layorders_by_betID == null)
                return;

            List<string> layorders_by_betID_list = new List<string>(layorders_by_betID.Keys.ToList());

            foreach (string s in layorders_by_betID_list)
                Update_Bet(s);
        }


        /// <summary>
        /// Updates the bets on the market. If the bets are outbid, it's highers the bid.
        /// </summary>
        /// <returns> </returns>
        public void Update_Bet(string betID)
        {

            double price = layorders_by_betID[betID].Price;
            double size = layorders_by_betID[betID].Size;

            if (price > Math.Max(marketlay + Utils.Increment(marketlay), minprice))
                price -= Utils.Increment(price);
            else if (price < Math.Min(marketlay, maxprice))
                price += Utils.Increment(marketlay);
            else if (laymarketprice.Count > 0 && price == laymarketprice[0].Price && ((laymarketprice[0].Size - size) >= 2))
                price += Utils.Increment(marketlay);
            else
                return;

            replace = new ReplaceInstruction();

            replace.BetId = betID;
            replace.NewPrice = Utils.CheckPrice(price);

            MainProgram.replaceInstructions.Add(replace);
        }
    }
}

