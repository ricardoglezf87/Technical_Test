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
    public class ModelManager : ICollectionManager<Model>
    {

        /// <summary>
        /// get the collection from database
        /// </summary>
        /// <returns></returns>
        public IMongoCollection<Model> loadCollection()
        {           
            MongoClient client = new MongoClient(AppSettings.ConnectionStrings.ServerAddress);
            IMongoDatabase ddbb = client.GetDatabase(AppSettings.ConnectionStrings.DataBase);
            return ddbb.GetCollection<Model>("Models");
        }

        /// <summary>
        /// Get a document through of identify of brand getted by parameters
        /// </summary>
        /// <param name="brand_id">Identify of brand (String)</param>
        /// <returns></returns>
        public List<Model> getbyIdBrand(string brand_id)
        {
            return loadCollection()?.Find(x => x.Brand_id.Equals(brand_id)).ToList();
        }


    }
}
