using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoexTrading.Models;
using Newtonsoft.Json.Linq;

namespace MoexTrading.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public object PostDataCandles([FromBody]JObject value)
        {
            int id = (int)value["Id"];
            var list = APIMongo.GetDataById<DataCandlesTik>(id, ElementMongo.NameTableCandlesOnTik);

            return list;
        }

        public object GetDataKotirovka()
        {
            var listCotir = APIMongo.GetData<DataKotirovka>(ElementMongo.NameTableKotirovka);
            var listInfo = APIMongo.GetData<DataTools>(ElementMongo.NameTableTools);


            var result = from a in listCotir
                         join b in listInfo on a.Id equals b.Id
                         select new { Name = b.Name, Kotir = a.Value, Diference = a.Diference, Percent = a.Percent };

            return result;
        }

        public object PostSetStakan([FromBody]JObject value)
        {
            int id = (int)value["Id"];
            var data = APIMongo.GetDataById<DataGlass>(id, ElementMongo.NameTableGlass);

            if (data == null)
                return null;

            return data.ArrayGlass;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
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
