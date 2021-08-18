using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technical_Test.Models;
using Technical_Test.Services.Setting;

namespace Technical_Test.DAL
{
    public class BrandManager : ICollectionManager<Brand>
    {

        /// <summary>
        /// get the collection from database
        /// </summary>
        /// <returns></returns>
        public IMongoCollection<Brand> loadCollection()
    {
            MongoClient client = new MongoClient(AppSettings.ConnectionStrings.ServerAddress);
            IMongoDatabase ddbb = client.GetDatabase(AppSettings.ConnectionStrings.DataBase);
            return ddbb.GetCollection<Brand>("Brands");
        }

    }
}
