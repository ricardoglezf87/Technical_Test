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
    public class CarManager : ICollectionManager<Car>
    {

        /// <summary>
        /// get the collection from database
        /// </summary>
        /// <returns></returns>
        public IMongoCollection<Car> loadCollection()
        {
            MongoClient client = new MongoClient(AppSettings.ConnectionStrings.ServerAddress);
            IMongoDatabase ddbb = client.GetDatabase(AppSettings.ConnectionStrings.DataBase);
            return ddbb.GetCollection<Car>("Cars");
        }

        /// <summary>
        /// Get a list with all rows of the collection
        /// </summary>
        /// <returns></returns>
        public List<Car> getAll()
        {
            List<Car> cars = loadCollection()?.Find(x => true).ToList();

            if (cars != null)
            {            
                for(int i= 0;i< cars.Count;i++)
                {
                    Car car = cars[i];
                    fillExtraProperties(ref car);
                    cars[i] = car;
                }
            }

            return cars;
        }

        /// <summary>
        /// Get a document through of identify passed by parameters
        /// </summary>
        /// <param name="id">identify of document (String)</param>
        /// <returns></returns>
        public Car getbyID(string id)
        {
            Car car = (loadCollection()?.Find(x => x.Id.Equals(id))).FirstOrDefault();
            if (car != null)
            {
                fillExtraProperties(ref car);
            }
            return car;
        }

        private void fillExtraProperties(ref Car car)
        {
            car.PriceIVA = car.Price * 1.21;
            car.Brand_Descrip = ((ICollectionManager<Brand>)new BrandManager()).getbyID(car.Brand_id)?.Descrip;
            car.Model_Descrip = ((ICollectionManager<Model>)new ModelManager()).getbyID(car.Model_id)?.Descrip;
        }

    }
}
