using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technical_Test.Models;

namespace Technical_Test.Services
{
    public class BrandService
    {
        private readonly IMongoCollection<Brand> brands;

        public BrandService(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("TechnicalTestDDBB"));
            IMongoDatabase ddbb = client.GetDatabase("TechnicalTestDDBB");
            brands = ddbb.GetCollection<Brand>("Brands");
        }

        public List<Brand> getAll()
        {            
            return brands.Find(x => true).ToList();
        }

        public Brand getbyID(string id)
        {
            return brands.Find(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public Brand New(Brand brand)
        {
            brands.InsertOne(brand);
            return brand;
        }

        public void Update(Brand brand)
        {
            brands.ReplaceOne(x => x.Id.Equals(brand.Id), brand);
        }

        public void Delete(Brand brand)
        {
            brands.DeleteOne(x => x.Id.Equals(brand.Id));
        }

        public void DeletebyId(string id)
        {
            brands.DeleteOne(x => x.Id.Equals(id));
        }

    }
}
