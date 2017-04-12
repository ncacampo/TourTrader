using System.Threading;
using TourTrader.TO;
using System.Collections.Generic;

namespace TourTrader
{
    class ApiSet
    {
        public static void CancelOrders(List<CancelInstruction> cancelInstructions)
        {
            Thread thread = new Thread(() => ThreadCancelOrder(cancelInstructions));
            thread.Start();
        }

        public static void CancelAll()
        {
            Thread thread = new Thread(() => ThreadCancelAll());
            thread.Start();
        }

        public static void PlaceOrder(List<PlaceInstruction> placeInstructions)
        {
            Thread thread = new Thread(() => ThreadPlaceOrder(placeInstructions));
            thread.Start();
        }

        public static void ReplaceOrder(List<ReplaceInstruction> replaceInstructions)
        {
            Thread thread = new Thread(() => ThreadReplaceOrder(replaceInstructions));
            thread.Start();
        }

        public static void PlaceOrder(PlaceInstruction placeInstruction)
        {
            List<PlaceInstruction> placeInstructions = new List<PlaceInstruction>();
            placeInstructions.Add(placeInstruction);
            Thread thread = new Thread(() => ThreadPlaceOrder(placeInstructions));
            thread.Start();
        }

        private static void ThreadCancelAll()
        {
            using (JsonRpcClient Client = new JsonRpcClient(Program.endPoint, Program.appKey, Program.sessionToken))
            { Client.cancelAll(Program.marketID); }
        }

        private static void ThreadCancelOrder(List<CancelInstruction> cancelInstructions)
        {
            using (JsonRpcClient Client = new JsonRpcClient(Program.endPoint, Program.appKey, Program.sessionToken))
            { Client.cancelOrders(Program.marketID, cancelInstructions); }
        }

        private static void ThreadReplaceOrder(List<ReplaceInstruction> replaceInstructions)
        {
            using (JsonRpcClient Client = new JsonRpcClient(Program.endPoint, Program.appKey, Program.sessionToken))
            { Client.replaceOrders(Program.marketID, replaceInstructions); }
        }

        public static void ThreadPlaceOrder(List<PlaceInstruction> placeInstructions)
        {
            JsonRpcClient Client_ = new JsonRpcClient(Program.endPoint, Program.appKey, Program.sessionToken);
            Client_.placeOrders(Program.marketID, placeInstructions);
        }
    }

    

}
