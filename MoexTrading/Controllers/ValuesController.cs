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
            var T = new TT();

            List<TT> lis = new List<TT>();
            lis.Add(new TT { Time = 1538778600000, Arr = new float[4] { 6629.81f, 6650.5f, 6623.04f, 6633.33f } });
            lis.Add(new TT { Time = 1538780400000, Arr = new float[4] { 6632.01f, 6643.59f, 6620, 6630.11f } });
            lis.Add(new TT { Time = 1538782200000, Arr = new float[4] { 6630.71f, 6648.95f, 6623.34f, 6635.65f } });
            lis.Add(new TT { Time = 1538784000000, Arr = new float[4] { 6635.65f, 6651, 6629.67f, 6638.24f } });
            lis.Add(new TT { Time = 1538785800000, Arr = new float[4] { 6638.24f, 6640, 6620, 6624.47f } });

            return lis;

            //int id = (int)value["Id"];
            //var list = APIMongo.GetCandlesTikById(id, ElementMongo.NameTableCandlesOnTik);

         //   return list;
        }

        public object GetDataKotirovka()
        {
            var listCotir = APIMongo.GetKotirovka();// .GetCotirovka();
            var listInfo = APIMongo.GetTools();// .GetData("NameInstruments");


            var result = from a in listCotir
                         join b in listInfo on a.Id equals b.Id
                         select new { Name = b.Name, Kotir = a.Value, Diference = a.Diference, Percent = a.Percent };

            return result;
        }

        public object PostSetStakan([FromBody]JObject value)
        {
            int id = (int)value["Id"];
            var data = APIMongo.GetGlassById(id);

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
