using MongoDB.Driver;
using System;
using Technical_Test.Models;
using Xunit;
using FluentAssertions;
using Technical_Test.xUnit.FakeClasses;
using Technical_Test.DAL;
using LoremNETCore;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace Technical_Test.xUnit
{
    [Collection("ManagersTest")]    
    public class ModelMangerTest: IDisposable
    {
        private readonly ICollectionManager<Model> modelManager;       
        private readonly List<Brand> brands;

        public ModelMangerTest()
        {
            modelManager = new ModelManagerFake();
            ICollectionManager<Brand> brandManager = new BrandManagerFake();
            brands = new List<Brand>();

            for (int i = 0; i < 100; i++)
            {
                string descrip = Generate.Words(1, true, true);
                var brand = new Brand()
                {
                    Descrip = descrip
                };

                brand = brandManager.New(brand);
                brands.Add(brand);
            }
        }

        [Fact]
        public void addOneModel()
        {
            Brand brand = brands[(int)Generate.Number(0, 99)];
            var model = new Model() {
                Descrip = "Corsa",
                Brand_id = brand.Id
            };

            model = modelManager.New(model);
            model.Id.Should().NotBeNull();

            model = modelManager.getbyID(model.Id);
            model.Descrip.Should().Be("Corsa");
            model.Brand_id.Should().Be(brand.Id);            

            ((ModelManagerFake)modelManager).cleanCollection();
        }

        [Fact]
        public void addMultiplesModels()
        {
            for (int i = 0; i < 100; i++)
            {
                Brand brand = brands[(int)Generate.Number(0, 99)];
                string descrip = Generate.Words(1, true, true);
                var model = new Model()
                {
                    Descrip = descrip,
                    Brand_id = brand.Id
                };

                model = modelManager.New(model);
                model.Id.Should().NotBeNull();
                model = modelManager.getbyID(model.Id);
                model.Descrip.Should().Be(descrip);
                model.Brand_id.Should().Be(brand.Id);               
            }
            ((ModelManagerFake)modelManager).cleanCollection();
        }

        [Fact]
        public void updateOneModel()
        {
            Brand brand = brands[(int)Generate.Number(0, 99)];
            var model = new Model()
            {
                Descrip = "Corsa",
                Brand_id = brand.Id
            };

            model = modelManager.New(model);
            model.Id.Should().NotBeNull();

            model.Descrip = "Mercedes";
            brand = brands[(int)Generate.Number(0, 99)];
            model.Brand_id = brand.Id;
            modelManager.Update(model);

            model = modelManager.getbyID(model.Id);
            model.Descrip.Should().Be("Mercedes");
            model.Brand_id.Should().Be(brand.Id);

            ((ModelManagerFake)modelManager).cleanCollection();
        }

        [Fact]
        public void updateMultiplesModels()
        {
            for (int i = 0; i < 100; i++)
            {
                string descrip = Generate.Words(1, true, true);
                Brand brand = brands[(int)Generate.Number(0, 99)];
                var model = new Model()
                {
                    Descrip = descrip,
                    Brand_id = brand.Id
                };

                model = modelManager.New(model);
                model.Id.Should().NotBeNull();

                descrip = Generate.Words(1, true, true);
                model.Descrip = descrip;
                brand = brands[(int)Generate.Number(0, 99)];
                model.Brand_id = brand.Id;
                modelManager.Update(model);

                model = modelManager.getbyID(model.Id);
                model.Descrip.Should().Be(descrip);
                model.Brand_id.Should().Be(brand.Id);
            }
            ((ModelManagerFake)modelManager).cleanCollection();
        }

        [Fact]
        public void deleteOneModel()
        {
            var model = new Model();
            model = modelManager.New(model);

            model.Id.Should().NotBeNull();
            modelManager.Delete(model);

            modelManager.getbyID(model.Id).Should().BeNull();
        }

        [Fact]
        public void deleteMultiplesModels()
        {
            for (int i = 0; i < 100; i++)
            {                
                var model = new Model();
                model = modelManager.New(model);

                model.Id.Should().NotBeNull();
                modelManager.Delete(model);

                modelManager.getbyID(model.Id)?.Should().BeNull();
            }
        }

        [Fact]
        public void deleteOneModelbyID()
        {
            var model = new Model();
            model = modelManager.New(model);

            model.Id.Should().NotBeNull();
            modelManager.DeletebyId(model.Id);

            modelManager.getbyID(model.Id).Should().BeNull();
        }

        [Fact]
        public void deleteMultiplesModelsbyID()
        {
            for (int i = 0; i < 100; i++)
            {                
                var model = new Model();
                model = modelManager.New(model);

                model.Id.Should().NotBeNull();
                modelManager.DeletebyId(model.Id);

                modelManager.getbyID(model.Id)?.Should().BeNull();
            }
        }

        [Fact]
        public void getModelsbyIdBrand()
        {
            Brand brand = new Brand();
            string descrip = Generate.Words(1, true, true);
            brand.Descrip = descrip;

            brand = ((ICollectionManager<Brand>)new BrandManagerFake()).New(brand);

            var models = new List<Model>();
            for (int i = 0; i < 100; i++)
            {
                descrip = Generate.Words(1, true, true);
               
                var model = new Model()
                {
                    Descrip = descrip,
                    Brand_id = brand.Id
                };

                models.Add(model);
                model = modelManager.New(model);
                model.Id.Should().NotBeNull();
            }

            var modelsDDBB = ((ModelManagerFake)modelManager).getbyIdBrand(brand.Id);
            models.Count.Should().Be(modelsDDBB.Count);
            modelsDDBB.Should().BeEquivalentTo(models);

            ((ModelManagerFake)modelManager).cleanCollection();
        }

        [Fact]
        public void getAllModels()
        {
            var models = new List<Model>();
            for (int i = 0; i < 100; i++)
            {
                string descrip = Generate.Words(1, true, true);
                Brand brand = brands[(int)Generate.Number(0, 99)];
                var model = new Model()
                {
                    Descrip = descrip,
                    Brand_id = brand.Id
                };

                models.Add(model);
                model = modelManager.New(model);
                model.Id.Should().NotBeNull();                
            }

            var modelsDDBB = modelManager.getAll();
            models.Count.Should().Be(modelsDDBB.Count);            
            modelsDDBB.Should().BeEquivalentTo(models);

             ((ModelManagerFake)modelManager).cleanCollection();
        }

        public void Dispose()
        {            
            ((ModelManagerFake)modelManager).dropDataBase();
        }
    }
}
