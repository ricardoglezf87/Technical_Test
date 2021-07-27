using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technical_Test.Models;

namespace Technical_Test.Services
{
    public class CarService
    {
        private readonly IMongoCollection<Car> cars;

        /// <summary>
        /// Initialization the connection
        /// </summary>
        /// <param name="config"></param>
        public CarService(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("TechnicalTestDDBB"));
            IMongoDatabase ddbb = client.GetDatabase("TechnicalTestDDBB");
            cars = ddbb.GetCollection<Car>("Cars");
        }

        /// <summary>
        /// Get a list with all rows of the collection
        /// </summary>
        /// <returns></returns>
        public List<Car> getAll()
        {
            return cars.Find(x => true).ToList();
        }

        /// <summary>
        /// Get a document through of identify passed by parameters
        /// </summary>
        /// <param name="id">identify of document (String)</param>
        /// <returns></returns>
        public Car getbyID(string id)
        {
            return cars.Find(x => x.Id.Equals(id)).FirstOrDefault();
        }

        /// <summary>
        /// Insert into collection a document passed by parameters
        /// </summary>
        /// <param name="brand">document (Car)</param>
        /// <returns></returns>
        public Car New(Car car)
        {            
            cars.InsertOne(car);
            return car;
        }

        /// <summary>
        /// Update a document from collection, replancing a old document by the new document passed by parameters
        /// </summary>
        /// <param name="model">new document (Car)</param>
        public void Update(Car car)
        {
            cars.ReplaceOne(x => x.Id.Equals(car.Id), car);
        }

        /// <summary>
        /// Delete from collection the document passed by parameters
        /// </summary>
        /// <param name="model">document to delete (Car)</param>
        public void Delete(Car car)
        {
            cars.DeleteOne(x => x.Id.Equals(car.Id));
        }

        /// <summary>
        /// Delete from collection a document with a identify of document passed by parameters
        /// </summary>
        /// <param name="id">identify of document (String)</param>
        public void DeletebyId(string id)
        {
            cars.DeleteOne(x => x.Id.Equals(id));
        }

    }
}
