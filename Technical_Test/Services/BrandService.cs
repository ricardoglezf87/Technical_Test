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

        /// <summary>
        /// Initialization the connection
        /// </summary>
        /// <param name="config"></param>
        public BrandService(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("TechnicalTestDDBB"));
            IMongoDatabase ddbb = client.GetDatabase("TechnicalTestDDBB");
            brands = ddbb.GetCollection<Brand>("Brands");
        }

        /// <summary>
        /// Get a list with all rows of the collection
        /// </summary>
        /// <returns></returns>
        public List<Brand> getAll()
        {            
            return brands.Find(x => true).ToList();
        }

        /// <summary>
        /// Get a document through of identify passed by parameters
        /// </summary>
        /// <param name="id">identify of document (String)</param>
        /// <returns></returns>
        public Brand getbyID(string id)
        {
            return brands.Find(x => x.Id.Equals(id)).FirstOrDefault();
        }

        /// <summary>
        /// Insert into collection a document passed by parameters
        /// </summary>
        /// <param name="brand">document (Brand)</param>
        /// <returns></returns>
        public Brand New(Brand brand)
        {
            brands.InsertOne(brand);
            return brand;
        }

        /// <summary>
        /// Update a document from collection, replancing a old document by the new document passed by parameters
        /// </summary>
        /// <param name="model">new document (Brand)</param>
        public void Update(Brand brand)
        {
            brands.ReplaceOne(x => x.Id.Equals(brand.Id), brand);
        }

        /// <summary>
        /// Delete from collection the document passed by parameters
        /// </summary>
        /// <param name="model">document to delete (Brand)</param>
        public void Delete(Brand brand)
        {
            brands.DeleteOne(x => x.Id.Equals(brand.Id));
        }

        /// <summary>
        /// Delete from collection a document with a identify of document passed by parameters
        /// </summary>
        /// <param name="id">identify of document (String)</param>
        public void DeletebyId(string id)
        {
            brands.DeleteOne(x => x.Id.Equals(id));
        }

    }
}
