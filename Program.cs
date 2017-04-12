using System.Collections.Generic;
using TourTrader.TO;

namespace TourTrader
{
    class Program
    {
        public static string endPoint;
        public static string appKey;
        public static string sessionToken;
        public static string marketID;

        static void Main()
        {
            endPoint = @"https://api.betfair.com/exchange/betting";
            appKey = "YspYojHKnoBTstpb";
            sessionToken = "g68C7J8OFhDxjriFJ2XP16wn6iHyG+07qZVyqFzyWy8=";
            marketID = "1.125814589";

            BackEnd.Run();
        }
    }
}