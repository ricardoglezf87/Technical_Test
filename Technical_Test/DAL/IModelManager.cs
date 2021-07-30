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
    public interface IModelManager
    {
        /// <summary>
        /// get the collection from database
        /// </summary>
        /// <returns></returns>
        public IMongoCollection<Model> loadCollection();

        /// <summary>
        /// Get a list with all rows of the collection
        /// </summary>
        /// <returns></returns>
        public List<Model> getAll()
        {
            return loadCollection()?.Find(x => true).ToList();
        }

        /// <summary>
        /// Get a document through of identify getted by parameters
        /// </summary>
        /// <param name="id">identify of document (String)</param>
        /// <returns></returns>
        public Model getbyID(string id)
        {
            return loadCollection()?.Find(x => x.Id.Equals(id)).FirstOrDefault();
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

        /// <summary>
        /// Insert into collection a document passed by parameters
        /// </summary>
        /// <param name="brand">document (Model)</param>
        /// <returns></returns>
        public Model New(Model model)
        {
            loadCollection()?.InsertOne(model);
            return model;
        }

        /// <summary>
        /// Update a document from collection, replancing a old document by the new document passed by parameters
        /// </summary>
        /// <param name="model">new document (Model)</param>
        public void Update(Model model)
        {
            loadCollection()?.ReplaceOne(x => x.Id.Equals(model.Id), model);
        }

        /// <summary>
        /// Delete from collection the document passed by parameters
        /// </summary>
        /// <param name="model">document to delete (Model)</param>
        public void Delete(Model model)
        {
            loadCollection()?.DeleteOne(x => x.Id.Equals(model.Id));
        }

        /// <summary>
        /// Delete from collection a document with a identify of document passed by parameters
        /// </summary>
        /// <param name="id">identify of document (String)</param>
        public void DeletebyId(string id)
        {
            loadCollection()?.DeleteOne(x => x.Id.Equals(id));
        }


    }
}
