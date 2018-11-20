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
                default:
                    return "";
            }
        }
    }

    public enum ElementMongo
    {
        ConnectionString,
        NameBD,
        NameTableTools
    }

}