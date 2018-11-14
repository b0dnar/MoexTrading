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

    public class DataCandlesticksOnTik
    {
        public int Id { get; set; }
        public List<decimal> ArrayCandlesticks { get; set; }
        public List<decimal> ArrayMin { get; set; }
        public List<decimal> ArrayMax { get; set; }
        public List<DateTime> ArrayTime { get; set; }

        public DataCandlesticksOnTik()
        {
            ArrayCandlesticks = new List<decimal>();
            ArrayMin = new List<decimal>();
            ArrayMax = new List<decimal>();
            ArrayTime = new List<DateTime>();
        }
    }

    public class DataCandlesticksOn1Min
    {
        public int Id { get; set; }
        public List<decimal> ArrayCandlesticks { get; set; }
        public List<decimal> ArrayMin { get; set; }
        public List<decimal> ArrayMax { get; set; }
        public List<DateTime> ArrayTime { get; set; }

        public DataCandlesticksOn1Min()
        {
            ArrayCandlesticks = new List<decimal>();
            ArrayMin = new List<decimal>();
            ArrayMax = new List<decimal>();
            ArrayTime = new List<DateTime>();
        }
    }

    public class DataCandlesticksOn5Min
    {
        public int Id { get; set; }
        public List<decimal> ArrayCandlesticks { get; set; }
        public List<decimal> ArrayMin { get; set; }
        public List<decimal> ArrayMax { get; set; }
        public List<DateTime> ArrayTime { get; set; }

        public DataCandlesticksOn5Min()
        {
            ArrayCandlesticks = new List<decimal>();
            ArrayMin = new List<decimal>();
            ArrayMax = new List<decimal>();
            ArrayTime = new List<DateTime>();
        }
    }

    public class DataCandlesticksOn15Min
    {
        public int Id { get; set; }
        public List<decimal> ArrayCandlesticks { get; set; }
        public List<decimal> ArrayMin { get; set; }
        public List<decimal> ArrayMax { get; set; }
        public List<DateTime> ArrayTime { get; set; }

        public DataCandlesticksOn15Min()
        {
            ArrayCandlesticks = new List<decimal>();
            ArrayMin = new List<decimal>();
            ArrayMax = new List<decimal>();
            ArrayTime = new List<DateTime>();
        }
    }

    public class DataCandlesticksOn1Hour
    {
        public int Id { get; set; }
        public List<decimal> ArrayCandlesticks { get; set; }
        public List<decimal> ArrayMin { get; set; }
        public List<decimal> ArrayMax { get; set; }
        public List<DateTime> ArrayTime { get; set; }

        public DataCandlesticksOn1Hour()
        {
            ArrayCandlesticks = new List<decimal>();
            ArrayMin = new List<decimal>();
            ArrayMax = new List<decimal>();
            ArrayTime = new List<DateTime>();
        }
    }

    public class DataCandlesticksOn4Hour
    {
        public int Id { get; set; }
        public List<decimal> ArrayCandlesticks { get; set; }
        public List<decimal> ArrayMin { get; set; }
        public List<decimal> ArrayMax { get; set; }
        public List<DateTime> ArrayTime { get; set; }

        public DataCandlesticksOn4Hour()
        {
            ArrayCandlesticks = new List<decimal>();
            ArrayMin = new List<decimal>();
            ArrayMax = new List<decimal>();
            ArrayTime = new List<DateTime>();
        }
    }

    public class DataCandlesticksOn5Hour
    {
        public int Id { get; set; }
        public List<decimal> ArrayCandlesticks { get; set; }
        public List<decimal> ArrayMin { get; set; }
        public List<decimal> ArrayMax { get; set; }
        public List<DateTime> ArrayTime { get; set; }

        public DataCandlesticksOn5Hour()
        {
            ArrayCandlesticks = new List<decimal>();
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

    public class DataCandlesticksOnDays
    {
        public int Id { get; set; }
        public List<Price> ArrayPrices { get; set; }

        public DataCandlesticksOnDays()
        {
            ArrayPrices = new List<Price>();
        }
    }

    public class DataCandlesticksOnWeeks
    {
        public int Id { get; set; }
        public List<Price> ArrayPrices { get; set; }

        public DataCandlesticksOnWeeks()
        {
            ArrayPrices = new List<Price>();
        }
    }

    public class DataCandlesticksOnMonths
    {
        public int Id { get; set; }
        public List<Price> ArrayPrices { get; set; }

        public DataCandlesticksOnMonths()
        {
            ArrayPrices = new List<Price>();
        }
    }
}