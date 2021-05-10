using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Security.Authentication;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;

namespace RazorComponentDemo.Services
{
    public class MongoDAL : IDAL
    {
        //private string host = "ithscosmosmongo.mongo.cosmos.azure.com";
        //private string userName = "ithscosmosmongo";
        //private string password = "RzDwyqiJUtKHxkCcxAfFhEcvnKb9GrJnKHrFEaFgWJmZSIf2lxBkvp4vJrR9NlWhaO5x4U72iutR6XlyWJgFfQ==";

        //private string dbName = "CosmosMongoDB";
        //private string collectionName = "CosmosMongoCollection3";

        private readonly IConfiguration _configuration;
        private string _myMongoDBSettings;

        public MongoDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            _myMongoDBSettings = _configuration["MyMongoDBSettings"];
        }



        private MongoClient GetClient()
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(_configuration[$"{_myMongoDBSettings}:Host"], 10255);
            settings.UseTls = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;
            settings.RetryWrites = false;

            MongoIdentity identity = new MongoInternalIdentity(_configuration[$"{_myMongoDBSettings}:DbNaMe"], _configuration[$"{_myMongoDBSettings}:UserName"]);
            MongoIdentityEvidence evidence = new PasswordEvidence(_configuration[$"{_myMongoDBSettings}:Password"]);

            settings.Credential = new MongoCredential("SCRAM-SHA-1", identity, evidence);

            MongoClient client = new MongoClient(settings);

            return client;
        }


        public IEnumerable<Models.Measurement> GetWeatherData()
        {
            var client = GetClient();
            var database = client.GetDatabase(_configuration[$"{_myMongoDBSettings}:dbName"]);
            var measurementCollection = database.GetCollection<Models.Measurement>(_configuration[$"{_myMongoDBSettings}:collectionName"]);

            return measurementCollection.Find(new BsonDocument())
                .ToList();
        }
    }
}
