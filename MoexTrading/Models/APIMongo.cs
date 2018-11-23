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

        public static async void UpdateTools(DataTools newData)
        {
            string nameCollection = InfoMongo.GetElementMongo(ElementMongo.NameTableTools);

            try
            {
                var collection = database.GetCollection<DataTools>(nameCollection);

                var filter = Builders<DataTools>.Filter.Eq("_id", newData.Id);
                var update = Builders<DataTools>.Update.Set("Name", newData.Name);

                var result = await collection.UpdateOneAsync(filter, update);
            }
            catch { }
        }

        #endregion

        #region Methodes from DataCandlesTick

        public static async void SetCandles(BsonDocument doc, ElementMongo nameTable)
        {
            string nameCollection = InfoMongo.GetElementMongo(nameTable);
            var collection = database.GetCollection<BsonDocument>(nameCollection);
            await collection.InsertOneAsync(doc);
        }

        public static List<DataCandlesTik> GetCandlesTikForPeriod(ElementMongo nameTableCandlesPeriod)
        {
            string nameCollection = InfoMongo.GetElementMongo(nameTableCandlesPeriod);
            var collection = database.GetCollection<DataCandlesTik>(nameCollection).AsQueryable();

            return collection.ToList();
        }

        public static List<DataCandlesDay> GetCandlesDayForPeriod(ElementMongo nameTableCandlesPeriod)
        {
            string nameCollection = InfoMongo.GetElementMongo(nameTableCandlesPeriod);
            var collection = database.GetCollection<DataCandlesDay>(nameCollection).AsQueryable();

            return collection.ToList();
        }

        public static async Task<DataCandlesTik> GetCandlesTikById(int id, ElementMongo nameTable)
        {
            string nameCollection = InfoMongo.GetElementMongo(nameTable);

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

                        foreach (var doc in list)
                        {
                            var temp = BsonSerializer.Deserialize<DataCandlesTik>(doc).Id;

                            if (temp == id)
                                return BsonSerializer.Deserialize<DataCandlesTik>(doc);
                        }
                    }
                }
            }
            catch { }

            return null;
        }

        public static async Task<DataCandlesDay> GetCandlesDayById(int id, ElementMongo nameTable)
        {
            string nameCollection = InfoMongo.GetElementMongo(nameTable);

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

                        foreach (var doc in list)
                        {
                            var temp = BsonSerializer.Deserialize<DataCandlesDay>(doc).Id;

                            if (temp == id)
                                return BsonSerializer.Deserialize<DataCandlesDay>(doc);
                        }
                    }
                }
            }
            catch { }

            return null;
        }

        public static async void UpdateCandlesTik(DataCandlesTik newData, ElementMongo nameTable)
        {
            string nameCollection = InfoMongo.GetElementMongo(nameTable);

            try
            {
                var collection = database.GetCollection<DataCandlesTik>(nameCollection);

                var filter = Builders<DataCandlesTik>.Filter.Eq("_id", newData.Id);
                var update = Builders<DataCandlesTik>.Update.Set("ArrayCandles", newData.ArrayCandles).Set("ArrayMin", newData.ArrayMin).Set("ArrayMax", newData.ArrayMax).Set("ArrayTime", newData.ArrayTime);

                var result = await collection.UpdateOneAsync(filter, update);
            }
            catch { }
        }

        public static async void UpdateCandlesDay(DataCandlesDay newData, ElementMongo nameTable)
        {
            string nameCollection = InfoMongo.GetElementMongo(nameTable);

            try
            {
                var collection = database.GetCollection<DataCandlesDay>(nameCollection);

                var filter = Builders<DataCandlesDay>.Filter.Eq("_id", newData.Id);
                var update = Builders<DataCandlesDay>.Update.Set("ArrayPrices", newData.ArrayPrices);

                var result = await collection.UpdateOneAsync(filter, update);
            }
            catch { }
        }

        #endregion

        #region Methodes from DataGlass

        public static async void SetGlass(BsonDocument doc)
        {
            string nameCollection = InfoMongo.GetElementMongo(ElementMongo.NameTableGlass);
            var collection = database.GetCollection<BsonDocument>(nameCollection);
            await collection.InsertOneAsync(doc);
        }

        public static List<DataGlass> GetGlass()
        {
            string nameCollection = InfoMongo.GetElementMongo(ElementMongo.NameTableGlass);
            var collection = database.GetCollection<DataGlass>(nameCollection).AsQueryable();

            return collection.ToList();
        }

        public static async Task<DataGlass> GetGlassById(int id)
        {
            string nameCollection = InfoMongo.GetElementMongo(ElementMongo.NameTableGlass);

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

                        foreach (var doc in list)
                        {
                            var temp = BsonSerializer.Deserialize<DataGlass>(doc).Id;

                            if (temp == id)
                                return BsonSerializer.Deserialize<DataGlass>(doc);
                        }
                    }
                }
            }
            catch(Exception e) { }

            return null;
        }

        public static async void UpdateGlass(DataGlass newData)
        {
            string nameCollection = InfoMongo.GetElementMongo(ElementMongo.NameTableGlass);

            try
            {
                var collection = database.GetCollection<DataGlass>(nameCollection);

                var filter = Builders<DataGlass>.Filter.Eq("_id", newData.Id);
                var update = Builders<DataGlass>.Update.Set("ArrayGlass", newData.ArrayGlass);

                var result = await collection.UpdateOneAsync(filter, update);
            }
            catch { }
        }

        #endregion
    }
}