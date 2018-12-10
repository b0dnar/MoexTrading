using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoexTrading.Models;
using MoexTrading.Plaza.Job;

namespace MoexTrading.Controllers
{
    public class ValuesController : ApiController
    {
        // GET 
        public object GetDataKotirovka()
        {
            var listCotir = APIMongo.GetData<DataKotirovka>(ElementMongo.NameTableKotirovka);
            var listInfo = APIMongo.GetData<DataTools>(ElementMongo.NameTableTools);


            var result = from a in listCotir
                         join b in listInfo on a.Id equals b.Id
                         select new { Name = b.Name, Kotir = a.Value, Diference = a.Diference, Percent = a.Percent };

            return result;
        }

        public object GetDataCandlesTikById(int id)
        {
            return APIMongo.GetDataById<DataCandlesTik>(id, ElementMongo.NameTableCandlesOnTik);
        }

        public object GetStakanById(int id)
        {
            var data = APIMongo.GetDataById<DataGlass>(id, ElementMongo.NameTableGlass);
            if (data == null)
                return null;

            return data.ArrayGlass;
        }

        public object GetDealById(int id)
        {
            return APIMongo.GetDataById<DataDeal>(id, ElementMongo.NameTableDeal);
        }

        // POST api/values
        public void PostPublish()
        {
            UserInfo.Publish();
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    public class TT
    {
        public decimal Time { get; set; }
        public  float[] Arr { get; set; }

       
    }

}
