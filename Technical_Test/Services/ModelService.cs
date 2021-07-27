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
    public class ModelService
    {
        private readonly IMongoCollection<Model> models;

        public ModelService(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("TechnicalTestDDBB"));
            IMongoDatabase ddbb = client.GetDatabase("TechnicalTestDDBB");
            models = ddbb.GetCollection<Model>("Models");
        }

        public List<Model> getAll()
        {
            return models.Find(x => true).ToList();
        }

        public Model getbyID(string id)
        {
            return models.Find(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public List<Model> getbyIdBrand(string brand_id)
        {
            return models.Find(x => x.Brand_Id.Equals(brand_id)).ToList();
        }

        public List<SelectListItem> getAll_toSelectListItem()
        {
            var lSelec = new List<SelectListItem>();

            foreach (var model in getAll())
            {
                lSelec.Add(new SelectListItem() { Text = model.Descrip, Value = model.Id });
            }

            return lSelec;
        }

        public Model New(Model model)
        {
            models.InsertOne(model);
            return model;
        }

        public void Update(Model model)
        {
            models.ReplaceOne(x => x.Id.Equals(model.Id), model);
        }

        public void Delete(Model model)
        {
            models.DeleteOne(x => x.Id.Equals(model.Id));
        }

        public void DeletebyId(string id)
        {
            models.DeleteOne(x => x.Id.Equals(id));
        }

    }
}
