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
    public class CarMangerTest : IDisposable
    {
        private readonly ICarManager carManager;       
        private readonly List<Brand> brands;
        private readonly List<Model> models;

        public CarMangerTest()
        {
            carManager = new CarManagerFake();
            IBrandManager brandManager = new BrandManagerFake();

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


            IModelManager modelManager = new ModelManagerFake();            
            models = new List<Model>();

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
                models.Add(model);
            }
        }

        /// <summary>
        /// Get a random brand and random model of that brand passed by reference
        /// </summary>
        /// <param name="brand">Brand by referencia (Brand)</param>
        /// <param name="model">Model by referencia (Model)</param>
        private void getRandomBrandModelbyIdModel(ref Brand brand,ref Model model)
        {
            IModelManager modelManager = new ModelManagerFake();                        
            List<Model> listModel;

            do {
                brand = brands[(int)Generate.Number(0, 99)];
                listModel = modelManager.getbyIdBrand(brand.Id);
            } while (listModel.Count == 0) ;

            model = modelManager.getbyID(listModel[(int)Generate.Number(0, listModel.Count - 1)].Id);
        }

        [Fact]
        public void addOneCar()
        {
            Brand brand = new Brand();
            Model model = new Model();
            getRandomBrandModelbyIdModel(ref brand,ref model);

            var car = new Car() {                
                NumberPlate = "3214-CFR",
                Brand_id = brand.Id,
                Model_id = model.Id,
                Price = 1250.54
            };

            car = carManager.New(car);
            car.Id.Should().NotBeNull();

            car = carManager.getbyID(car.Id);
            car.NumberPlate.Should().Be("3214-CFR");
            car.Brand_id.Should().Be(brand.Id);
            car.Model_id.Should().Be(model.Id);
            car.Price.Should().Be(1250.54);

            ((CarManagerFake)carManager).cleanCollection();
        }

        [Fact]
        public void addMultiplesCars()
        {
            for (int i = 0; i < 100; i++)
            {
                string numberplate = Generate.Words(1, true, true);
                Brand brand = new Brand();
                Model model = new Model();
                getRandomBrandModelbyIdModel(ref brand, ref model);
                double price = Generate.Number(0, 10000);
                var car = new Car()
                {
                    NumberPlate = numberplate,
                    Brand_id = brand.Id,
                    Model_id = model.Id,
                    Price = price
                };

                car = carManager.New(car);
                car.Id.Should().NotBeNull();
                car = carManager.getbyID(car.Id);
                car.NumberPlate.Should().Be(numberplate);
                car.Brand_id.Should().Be(brand.Id);
                car.Model_id.Should().Be(model.Id);
                car.Price.Should().Be(price);
            }
            ((CarManagerFake)carManager).cleanCollection();
        }

        [Fact]
        public void updateOneCar()
        {
            Brand brand = new Brand();
            Model model = new Model();
            getRandomBrandModelbyIdModel(ref brand, ref model);

            var car = new Car()
            {
                NumberPlate = "3214-CFR",
                Brand_id = brand.Id,
                Model_id = model.Id,
                Price = 1250.54
            };

            car = carManager.New(car);
            car.Id.Should().NotBeNull();

            getRandomBrandModelbyIdModel(ref brand, ref model);
            car.NumberPlate = "2584-YHN";
            car.Brand_id = brand.Id;
            car.Model_id = model.Id;
            car.Price = 45000.34;
            carManager.Update(car);

            car = carManager.getbyID(car.Id);
            car.NumberPlate.Should().Be("2584-YHN");
            car.Brand_id.Should().Be(brand.Id);
            car.Model_id.Should().Be(model.Id);
            car.Price.Should().Be(45000.34);

            ((CarManagerFake)carManager).cleanCollection();
        }

        [Fact]
        public void updateMultiplesCars()
        {
            for (int i = 0; i < 100; i++)
            {
                string numberplate = Generate.Words(1, true, true);
                Brand brand = new Brand();
                Model model = new Model();
                getRandomBrandModelbyIdModel(ref brand, ref model);
                double price = Generate.Number(0, 10000);
                var car = new Car()
                {
                    NumberPlate = numberplate,
                    Brand_id = brand.Id,
                    Model_id = model.Id,
                    Price = price
                };

                car = carManager.New(car);
                car.Id.Should().NotBeNull();

                numberplate = Generate.Words(1, true, true);
                getRandomBrandModelbyIdModel(ref brand, ref model);                
                price = Generate.Number(0, 10000);

                car.NumberPlate = numberplate;
                car.Brand_id = brand.Id;
                car.Model_id = model.Id;
                car.Price = price;
                carManager.Update(car);

                car = carManager.getbyID(car.Id);
                car.NumberPlate.Should().Be(numberplate);
                car.Brand_id.Should().Be(brand.Id);
                car.Model_id.Should().Be(model.Id);
                car.Price.Should().Be(price);
            }
            ((CarManagerFake)carManager).cleanCollection();
        }

        [Fact]
        public void deleteOneCar()
        {
            var car = new Car();
            car = carManager.New(car);

            car.Id.Should().NotBeNull();
            carManager.Delete(car);

            carManager.getbyID(car.Id).Should().BeNull();
        }

        [Fact]
        public void deleteMultiplesCars()
        {
            for (int i = 0; i < 100; i++)
            {                
                var car = new Car();
                car = carManager.New(car);

                car.Id.Should().NotBeNull();
                carManager.Delete(car);

                carManager.getbyID(car.Id)?.Should().BeNull();
            }
        }

        [Fact]
        public void deleteOneCarbyID()
        {
            var car = new Car();

            car = carManager.New(car);
            car.Id.Should().NotBeNull();
            carManager.DeletebyId(car.Id);

            carManager.getbyID(car.Id).Should().BeNull();
        }

        [Fact]
        public void deleteMultiplesCarsbyID()
        {
            for (int i = 0; i < 100; i++)
            {
                string descrip = Generate.Words(1, true, true);
                var car = new Car();

                car = carManager.New(car);
                car.Id.Should().NotBeNull();
                carManager.DeletebyId(car.Id);

                carManager.getbyID(car.Id)?.Should().BeNull();
            }
        }

        [Fact]
        public void getAllCars()
        {
            var cars = new List<Car>();
            for (int i = 0; i < 100; i++)
            {
                string descrip = Generate.Words(1, true, true);
                Brand brand = brands[(int)Generate.Number(0, 99)];
                var car = new Car()
                {
                    //Descrip = descrip,
                    Brand_id = brand.Id
                };

                cars.Add(car);
                car = carManager.New(car);
                car.Id.Should().NotBeNull();                
            }

            var carsDDBB = carManager.getAll();
            cars.Count.Should().Be(carsDDBB.Count);            
            carsDDBB.Should().BeEquivalentTo(cars);

             ((CarManagerFake)carManager).cleanCollection();
        }

        public void Dispose()
        {            
            ((CarManagerFake)carManager).dropDataBase();
        }
    }
}
