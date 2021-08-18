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
    public interface ICollectionManager<T> where T: Models.ICollection 
    {
        /// <summary>
        /// get the collection from database
        /// </summary>
        /// <returns></returns>
        public abstract IMongoCollection<T> loadCollection();

        /// <summary>
        /// Get a list with all rows of the collection
        /// </summary>
        /// <returns></returns>
        public List<T> getAll()
        {            
            return loadCollection()?.Find(x => true).ToList();
        }

        /// <summary>
        /// Get a document through of identify passed by parameters
        /// </summary>
        /// <param name="id">identify of document (String)</param>
        /// <returns></returns>
        public T getbyID(string id)
        {
            return (loadCollection()?.Find(x => x.Id.Equals(id))).FirstOrDefault();
        }

        /// <summary>
        /// Insert into collection a document passed by parameters
        /// </summary>
        /// <param name="document">document (Brand)</param>
        /// <returns></returns>
        public T New(T document)
        {
            loadCollection()?.InsertOne(document);
            return document;
        }

        /// <summary>
        /// Update a document from collection, replancing a old document by the new document passed by parameters
        /// </summary>
        /// <param name="model">new document (Brand)</param>
        public void Update(T document)
        {
            loadCollection()?.ReplaceOne(x => x.Id.Equals(document.Id), document);
        }

        /// <summary>
        /// Delete from collection the document passed by parameters
        /// </summary>
        /// <param name="model">document to delete (Brand)</param>
        public void Delete(T document)
        {
            loadCollection()?.DeleteOne(x => x.Id.Equals(document.Id));
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
