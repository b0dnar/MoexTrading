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

    public class Price
    {
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public DateTime Time { get; set; }
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

    public class DataKotirovka
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public decimal Diference { get; set; }
        public decimal Percent { get; set; }
    }

    public class Glass
    {
        public decimal Price { get; set; }
        public decimal Volum { get; set; }
        public sbyte Dir { get; set; }
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
