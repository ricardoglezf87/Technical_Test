using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical_Test.DAL;
using Technical_Test.Models;

namespace Technical_Test.xUnit.FakeClasses
{
    class ModelManagerFake : ICollectionManager<Model>
    {
        private const string BBDD = "DBTest";

        /// <summary>
        /// get the collection from test database 
        /// </summary>
        /// <returns></returns>
        public IMongoCollection<Model> loadCollection()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase ddbb = client.GetDatabase(BBDD);
            return ddbb.GetCollection<Model>("Models");
        }

        /// <summary>
        /// Delete all documents from the collection
        /// </summary>
        public void cleanCollection()
        {
            loadCollection()?.DeleteMany(x => true);
        }

        /// <summary>
        /// Delete database from server *Be carefull with this method*
        /// </summary>
        public void dropDataBase()
        {
            loadCollection()?.Database.Client.DropDatabase(BBDD);
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
