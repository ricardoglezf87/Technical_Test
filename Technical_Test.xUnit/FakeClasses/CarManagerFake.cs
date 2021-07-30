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
    class CarManagerFake : ICarManager
    {
        private const string BBDD = "DBTest";

        /// <summary>
        /// get the collection from test database 
        /// </summary>
        /// <returns></returns>
        public IMongoCollection<Car> loadCollection()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase ddbb = client.GetDatabase(BBDD);
            return ddbb.GetCollection<Car>("Cars");
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
    }
}
