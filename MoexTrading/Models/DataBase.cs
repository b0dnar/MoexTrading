using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoexTrading.Models
{
    public class DataTools
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DataCandlesTik
    {
        public int Id { get; set; }
        public List<decimal> ArrayCandles { get; set; }
        public List<decimal> ArrayMin { get; set; }
        public List<decimal> ArrayMax { get; set; }
        public List<DateTime> ArrayTime { get; set; }

        public DataCandlesTik()
        {
            ArrayCandles = new List<decimal>();
            ArrayMin = new List<decimal>();
            ArrayMax = new List<decimal>();
            ArrayTime = new List<DateTime>();
        }
    }

    public struct Price
    {
        public decimal Open;
        public decimal Close;
        public decimal Min;
        public decimal Max;
        public DateTime Time;
    }

    public class DataCandlesDay
    {
        public int Id { get; set; }
        public List<Price> ArrayPrices { get; set; }

        public DataCandlesDay()
        {
            ArrayPrices = new List<Price>();
        }
    }

    public struct Glass
    {
        public decimal Price;
        public decimal Volum;
        public sbyte Dir;
    }

    public class DataGlass
    {
        public int Id { get; set; }
        public List<Glass> ArrayGlass { get; set; }

        public DataGlass()
        {
            ArrayGlass = new List<Glass>();
        }
    }
}
