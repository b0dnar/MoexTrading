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

        public static async void SetData(BsonDocument doc, ElementMongo nameTable)
        {
            string nameCollection = InfoMongo.GetElementMongo(nameTable);
            await database.GetCollection<BsonDocument>(nameCollection).InsertOneAsync(doc);
        }

        public static List<T> GetData<T>(ElementMongo nameTabl)
        {
            string nameCollection = InfoMongo.GetElementMongo(nameTabl);
            return database.GetCollection<T>(nameCollection).AsQueryable().ToList();
        }

        public static T GetDataById<T>(int id, ElementMongo nameTabl)
        {
            string nameCollection = InfoMongo.GetElementMongo(nameTabl);
            T val = default(T);

            switch (typeof(T).Name)
            {
                case "DataTools":
                    val = (T)Convert.ChangeType(database.GetCollection<DataTools>(nameCollection).AsQueryable().ToList().FirstOrDefault(x => x.Id == id), typeof(T));
                    break;
                case "DataCandlesTik":
                    val = (T)Convert.ChangeType(database.GetCollection<DataCandlesTik>(nameCollection).AsQueryable().ToList().FirstOrDefault(x => x.Id == id), typeof(T));
                    break;
                case "DataCandlesDay":
                    val = (T)Convert.ChangeType(database.GetCollection<DataCandlesDay>(nameCollection).AsQueryable().ToList().FirstOrDefault(x => x.Id == id), typeof(T));
                    break;
                case "DataGlass":
                    val = (T)Convert.ChangeType(database.GetCollection<DataGlass>(nameCollection).AsQueryable().ToList().FirstOrDefault(x => x.Id == id), typeof(T));
                    break;
                case "DataKotirovka":
                    val = (T)Convert.ChangeType(database.GetCollection<DataKotirovka>(nameCollection).AsQueryable().ToList().FirstOrDefault(x => x.Id == id), typeof(T));
                    break;
                case "DataDeal":
                    val = (T)Convert.ChangeType(database.GetCollection<DataDeal>(nameCollection).AsQueryable().ToList().FirstOrDefault(x => x.Id == id), typeof(T));
                    break;
            }

            return val;
        }

        public static async void UpdateData<T>(T data, ElementMongo nameTabl)
        {
            string nameCollection = InfoMongo.GetElementMongo(nameTabl);

            switch (typeof(T).Name)
            {
                case "DataTools":
                    var c1 = database.GetCollection<DataTools>(nameCollection);
                    DataTools d1 = (DataTools)Convert.ChangeType(data, typeof(DataTools));
                    var f1 = Builders<DataTools>.Filter.Eq("_id", d1.Id);
                    var u1 = Builders<DataTools>.Update.Set("Name", d1.Name);
                    await c1.UpdateOneAsync(f1, u1);
                    break;
                case "DataCandlesTik":
                    var c2 = database.GetCollection<DataCandlesTik>(nameCollection);
                    DataCandlesTik d2 = (DataCandlesTik)Convert.ChangeType(data, typeof(DataCandlesTik));
                    var f2 = Builders<DataCandlesTik>.Filter.Eq("_id", d2.Id);
                    var u2 = Builders<DataCandlesTik>.Update.Set("ArrayCandles", d2.ArrayCandles).Set("ArrayMin", d2.ArrayMin).Set("ArrayMax", d2.ArrayMax).Set("ArrayTime", d2.ArrayTime);
                    await c2.UpdateOneAsync(f2, u2);
                    break;
                case "DataCandlesDay":
                    var c3 = database.GetCollection<DataCandlesDay>(nameCollection);
                    DataCandlesDay d3 = (DataCandlesDay)Convert.ChangeType(data, typeof(DataCandlesDay));
                    var f3 = Builders<DataCandlesDay>.Filter.Eq("_id", d3.Id);
                    var u3 = Builders<DataCandlesDay>.Update.Set("ArrayPrices", d3.ArrayPrices);
                    await c3.UpdateOneAsync(f3, u3);
                    break;
                case "DataGlass":
                    var c4 = database.GetCollection<DataGlass>(nameCollection);
                    DataGlass d4 = (DataGlass)Convert.ChangeType(data, typeof(DataGlass));
                    var f4 = Builders<DataGlass>.Filter.Eq("_id", d4.Id);
                    var u4 = Builders<DataGlass>.Update.Set("ArrayGlass", d4.ArrayGlass);
                    await c4.UpdateOneAsync(f4, u4);
                    break;
                case "DataKotirovka":
                    var c5 = database.GetCollection<DataKotirovka>(nameCollection);
                    DataKotirovka d5 = (DataKotirovka)Convert.ChangeType(data, typeof(DataKotirovka));
                    var f5 = Builders<DataKotirovka>.Filter.Eq("_id", d5.Id);
                    var u5 = Builders<DataKotirovka>.Update.Set("Value", d5.Value).Set("Diference", d5.Diference).Set("Percent", d5.Percent);
                    await c5.UpdateOneAsync(f5, u5);
                    break;
                case "DataDeal":
                    var c6 = database.GetCollection<DataDeal>(nameCollection);
                    DataDeal d6 = (DataDeal)Convert.ChangeType(data, typeof(DataDeal));
                    var f6 = Builders<DataDeal>.Filter.Eq("_id", d6.Id);
                    var u6 = Builders<DataDeal>.Update.Set("Buy", d6.Buy).Set("Sell", d6.Sell);
                    await c6.UpdateOneAsync(f6, u6);
                    break;
            }
        }
    }
}