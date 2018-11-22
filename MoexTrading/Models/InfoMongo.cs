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
                case ElementMongo.NameTableDataCandlesOn1Min:
                    return "CandlesOn1Min";
                case ElementMongo.NameTableDataCandlesOn5Min:
                    return "CandlesOn5Min";
                case ElementMongo.NameTableDataCandlesOn15Min:
                    return "CandlesOn15Min";
                case ElementMongo.NameTableDataCandlesOn1Hour:
                    return "CandlesOn1Hour";
                case ElementMongo.NameTableDataCandlesOn4Hour:
                    return "CandlesOn4Hour";
                case ElementMongo.NameTableDataCandlesOn5Hour:
                    return "CandlesOn5Hour";
                case ElementMongo.NameTableDataCandlesOnDays:
                    return "CandlesOnDays";
                case ElementMongo.NameTableDataCandlesOnWeeks:
                    return "CandlesOnWeeks";
                case ElementMongo.NameTableDataCandlesOnMonths:
                    return "CandlesOnMonths";
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
        NameTableDataCandlesOn1Min,
        NameTableDataCandlesOn5Min,
        NameTableDataCandlesOn15Min,
        NameTableDataCandlesOn1Hour,
        NameTableDataCandlesOn4Hour,
        NameTableDataCandlesOn5Hour,
        NameTableDataCandlesOnDays,
        NameTableDataCandlesOnWeeks,
        NameTableDataCandlesOnMonths
    }

}