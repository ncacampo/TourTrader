using System;
using System.Linq;

namespace TourTrader
{
    public class Utils
    {
        public static double ToDouble(string s)
        {
            if (s.Contains('.'))
            {
                string[] split = s.Split('.');
                return Convert.ToDouble(split[0]) + Convert.ToDouble(split[1]) / (Math.Pow(10, split[1].Length));
            }

            return Convert.ToDouble(s);
        }

        public static double Increment(double d)
        {
            double Increment = 0;

            if (d < 2)
                Increment = 0.01;
            else if (d < 3)
                Increment = 0.02;
            else if (d < 4)
                Increment = 0.05;
            else if (d < 6)
                Increment = 0.1;
            else if (d < 10)
                Increment = 0.2;
            else if (d < 20)
                Increment = 0.5;
            else if (d < 30)
                Increment = 1;
            else if (d < 50)
                Increment = 2;
            else if (d < 100)
                Increment = 5;
            else if (d < 1000)
                Increment = 10;

            return Increment;
        }

        /// <summary>
        /// Round the price to what Betfair accepts.
        /// </summary>
        public static double RoundPrice(double Price)
        {

            if (Price < 2)
                Price -= Price % 0.01;
            else if (Price < 3)
                Price -= Price % 0.02;
            else if (Price < 4)
                Price -= Price % 0.05;
            else if (Price < 6)
                Price -= Price % 0.10;
            else if (Price < 10)
                Price -= Price % 0.20;
            else if (Price < 20)
                Price -= Price % 0.50;
            else if (Price < 30)
                Price -= Price % 1;
            else if (Price < 50)
                Price -= Price % 2;
            else if (Price < 100)
                Price -= Price % 5;
            else if (Price <= 1000)
                Price -= Price % 10;
            return Math.Round(Price,2);
        }

    }
}
