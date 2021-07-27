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

        public CarService(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("TechnicalTestDDBB"));
            IMongoDatabase ddbb = client.GetDatabase("TechnicalTestDDBB");
            cars = ddbb.GetCollection<Car>("Cars");
        }

        public List<Car> getAll()
        {
            return cars.Find(x => true).ToList();
        }

        public Car getbyID(string id)
        {
            return cars.Find(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public Car New(Car car)
        {            
            cars.InsertOne(car);
            return car;
        }

        public void Update(Car car)
        {
            cars.ReplaceOne(x => x.Id.Equals(car.Id), car);
        }

        public void Delete(Car car)
        {
            cars.DeleteOne(x => x.Id.Equals(car.Id));
        }

        public void DeletebyId(string id)
        {
            cars.DeleteOne(x => x.Id.Equals(id));
        }

    }
}
