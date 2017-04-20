using System.Threading;
using TourTrader.TO;
using System.Collections.Generic;

namespace TourTrader
{   

    /// <summary>
    /// Functions class to place and cancel orders on the API.
    /// </summary>
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
            using (JsonRpcClient Client = new JsonRpcClient(BackEnd.endPoint, BackEnd.appKey, BackEnd.sessionToken))
            { Client.cancelAll(BackEnd.marketID); }
        }

        private static void ThreadCancelOrder(List<CancelInstruction> cancelInstructions)
        {
            using (JsonRpcClient Client = new JsonRpcClient(BackEnd.endPoint, BackEnd.appKey, BackEnd.sessionToken))
            { Client.cancelOrders(BackEnd.marketID, cancelInstructions); }
        }

        private static void ThreadReplaceOrder(List<ReplaceInstruction> replaceInstructions)
        {
            using (JsonRpcClient Client = new JsonRpcClient(BackEnd.endPoint,BackEnd.appKey,BackEnd.sessionToken))
            { Client.replaceOrders(BackEnd.marketID, replaceInstructions); }
        }

        public static void ThreadPlaceOrder(List<PlaceInstruction> placeInstructions)
        {
            JsonRpcClient Client_ = new JsonRpcClient(BackEnd.endPoint, BackEnd.appKey, BackEnd.sessionToken);
            Client_.placeOrders(BackEnd.marketID, placeInstructions);
        }
    }

    

}
