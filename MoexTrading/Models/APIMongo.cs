using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;

namespace MoexTrading.Models
{
    public static class APIMongo
    {
        static MongoClient client;
        static IMongoDatabase database;

        static APIMongo()
        {
            string connStr = InfoMongo.GetElementMongo(ElementMongo.ConnectionString);
            string nameBD = InfoMongo.GetElementMongo(ElementMongo.NameBD);

            client = new MongoClient(connStr);
            database = client.GetDatabase(nameBD);
        }

        #region Methodes from DataTools
        public static async void SetTools(BsonDocument doc)
        {
            string nameCollection = InfoMongo.GetElementMongo(ElementMongo.NameTableTools);
            var collection = database.GetCollection<BsonDocument>(nameCollection);
            await collection.InsertOneAsync(doc);
        }

        public static List<DataTools> GetTools()
        {
            string nameCollection = InfoMongo.GetElementMongo(ElementMongo.NameTableTools);
            var collection = database.GetCollection<DataTools>(nameCollection).AsQueryable<DataTools>();

            return collection.ToList();
        }

        public static async Task<DataTools> GetToolsById(int id)
        {
            string nameCollection = InfoMongo.GetElementMongo(ElementMongo.NameTableTools);

            try
            {
                var collection = database.GetCollection<BsonDocument>(nameCollection);
                var filter = new BsonDocument();
                using (var cursor = await collection.FindAsync(filter))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        var list = cursor.Current.ToList();
                        if (list.Count == 0)
                            return null;

                        //  return (DataTools)list.Where(x => BsonSerializer.Deserialize<DataTools>(x).Id == id);//First(x => BsonSerializer.Deserialize<DataTools>(x).Id == id);//    Where(x => BsonSerializer.Deserialize<DataTools>(x).Id == id).Select();

                        foreach (var doc in list)
                        {
                            var temp = BsonSerializer.Deserialize<DataTools>(doc).Id;

                            if (temp == id)
                                return BsonSerializer.Deserialize<DataTools>(doc);
                        }
                    }
                }
            }
            catch { }

            return null;
        }

        private static async void UpdateTools(DataTools newData)
        {
            string nameCollection = InfoMongo.GetElementMongo(ElementMongo.NameTableTools);

            try
            {
                var collection = database.GetCollection<DataTools>(nameCollection);

                var filter = Builders<DataTools>.Filter.Eq("_id", newData.Id);
                var update = Builders<DataTools>.Update.Set("Name", newData.Name);

                var result = await collection.UpdateOneAsync(filter, update);
            }
            catch (Exception e) { }
        }

        #endregion

        //public async void SetData(BsonDocument doc, string nameCollection)
        //{
        //    var collection = database.GetCollection<BsonDocument>(nameCollection);
        //    await collection.InsertOneAsync(doc);
        //}

        ////get info for instrumetn. Return collection with have name and id instrument
        //public List<InfoTools> GetData(string nameCollection)
        //{
        //    var collection = database.GetCollection<InfoTools>(nameCollection).AsQueryable().ToList();//Select(x=>x.InstrumentName).

        //    return collection;

        //}

        ////get info for candles by id instrument
        //public List<DataCommon> GetCandlesById(int id)
        //{
        //    var collection = database.GetCollection<DataCommon>("DatasCandles").AsQueryable().Where(x => x.IdSess == id).ToList();//

        //    return collection;
        //}

        //public List<DataCotirovks> GetCotirovka()
        //{
        //    var collection = database.GetCollection<DataCotirovks>("BDCotirovka").AsQueryable().ToList();//

        //    return collection;
        //}

        //public DataOrder GetStakanById(int id)
        //{
        //    var collection = database.GetCollection<DataOrder>("BDStakan").AsQueryable().Where(x => x.Id == id).ToList();

        //    if (collection.Count == 0)
        //        return null;

        //    return collection[0];
        //}
    }
}