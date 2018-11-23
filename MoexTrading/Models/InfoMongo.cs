using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoexTrading.Models
{
    public static class InfoMongo
    {
        public static string GetElementMongo(ElementMongo element)
        {
            switch(element)
            {
                case ElementMongo.ConnectionString:
                    return "mongodb://localhost:27017";
                case ElementMongo.NameBD:
                    return "BDPlaza";
                case ElementMongo.NameTableTools:
                    return "Tools";
                case ElementMongo.NameTableCandlesOnTik:
                    return "CandlesOnTiky";
                case ElementMongo.NameTableCandlesOn1Min:
                    return "CandlesOn1Min";
                case ElementMongo.NameTableCandlesOn5Min:
                    return "CandlesOn5Min";
                case ElementMongo.NameTableCandlesOn15Min:
                    return "CandlesOn15Min";
                case ElementMongo.NameTableCandlesOn1Hour:
                    return "CandlesOn1Hour";
                case ElementMongo.NameTableCandlesOn4Hour:
                    return "CandlesOn4Hour";
                case ElementMongo.NameTableCandlesOn5Hour:
                    return "CandlesOn5Hour";
                case ElementMongo.NameTableCandlesOnDays:
                    return "CandlesOnDays";
                case ElementMongo.NameTableCandlesOnWeeks:
                    return "CandlesOnWeeks";
                case ElementMongo.NameTableCandlesOnMonths:
                    return "CandlesOnMonths";
                case ElementMongo.NameTableGlass:
                    return "Glass";
                default:
                    return "";
            }
        }
    }

    public enum ElementMongo
    {
        ConnectionString,
        NameBD,
        NameTableTools,
        NameTableCandlesOnTik,
        NameTableCandlesOn1Min,
        NameTableCandlesOn5Min,
        NameTableCandlesOn15Min,
        NameTableCandlesOn1Hour,
        NameTableCandlesOn4Hour,
        NameTableCandlesOn5Hour,
        NameTableCandlesOnDays,
        NameTableCandlesOnWeeks,
        NameTableCandlesOnMonths,
        NameTableGlass
    }

}