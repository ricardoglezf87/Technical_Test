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
    public class CarManager
    {
        /// <summary>
        /// get the collection from database
        /// </summary>
        /// <returns></returns>
        private static IMongoCollection<Car> loadCollection()
        {
            MongoClient client = new MongoClient(AppSettings.ConnectionStrings.ServerAddress);
            IMongoDatabase ddbb = client.GetDatabase(AppSettings.ConnectionStrings.DataBase);
            return ddbb.GetCollection<Car>("Cars");
        }

        /// <summary>
        /// Get a list with all rows of the collection
        /// </summary>
        /// <returns></returns>
        public static List<Car> getAll()
        {
            return loadCollection()?.Find(x => true).ToList();
        }

        /// <summary>
        /// Get a document through of identify passed by parameters
        /// </summary>
        /// <param name="id">identify of document (String)</param>
        /// <returns></returns>
        public static Car getbyID(string id)
        {
            return loadCollection()?.Find(x => x.Id.Equals(id)).FirstOrDefault();
        }

        /// <summary>
        /// Insert into collection a document passed by parameters
        /// </summary>
        /// <param name="brand">document (Car)</param>
        /// <returns></returns>
        public static Car New(Car car)
        {
            loadCollection()?.InsertOne(car);
            return car;
        }

        /// <summary>
        /// Update a document from collection, replancing a old document by the new document passed by parameters
        /// </summary>
        /// <param name="model">new document (Car)</param>
        public static void Update(Car car)
        {
            loadCollection()?.ReplaceOne(x => x.Id.Equals(car.Id), car);
        }

        /// <summary>
        /// Delete from collection the document passed by parameters
        /// </summary>
        /// <param name="model">document to delete (Car)</param>
        public static void Delete(Car car)
        {
            loadCollection()?.DeleteOne(x => x.Id.Equals(car.Id));
        }

        /// <summary>
        /// Delete from collection a document with a identify of document passed by parameters
        /// </summary>
        /// <param name="id">identify of document (String)</param>
        public static void DeletebyId(string id)
        {
            loadCollection()?.DeleteOne(x => x.Id.Equals(id));
        }

    }
}
