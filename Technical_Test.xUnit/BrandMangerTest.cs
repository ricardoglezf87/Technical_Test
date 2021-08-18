using MongoDB.Driver;
using System;
using Technical_Test.Models;
using Xunit;
using FluentAssertions;
using Technical_Test.xUnit.FakeClasses;
using Technical_Test.DAL;
using LoremNETCore;
using System.Collections.Generic;
using Xunit.Sdk;
using System.Reflection;

namespace Technical_Test.xUnit
{
    [Collection("ManagersTest")]   
    public class BrandMangerTest:IDisposable
    {
        private readonly ICollectionManager<Brand> brandManager;

        public BrandMangerTest()
        {
            brandManager = new BrandManagerFake();
        }

        [Fact]
        public void addOneBrand()
        {
            var brand = new Brand() {
                Descrip = "Opel"
            };

            brand = brandManager.New(brand);
            brand.Id.Should().NotBeNull();
            brandManager.getbyID(brand.Id)?.Descrip.Should().Be("Opel");
            ((BrandManagerFake)brandManager).cleanCollection();
        }

        [Fact]
        public void addMultiplesBrands()
        {
            for (int i = 0; i < 100; i++)
            {
                string descrip = Generate.Words(1, true, true);
                var brand = new Brand()
                {
                    Descrip = descrip
                };

                brand = brandManager.New(brand);
                brand.Id.Should().NotBeNull();
                brandManager.getbyID(brand.Id)?.Descrip.Should().Be(descrip);
            }
            ((BrandManagerFake)brandManager).cleanCollection();
        }

        [Fact]
        public void updateOneBrand()
        {
            var brand = new Brand()
            {
                Descrip = "Opel"
            };

            brand = brandManager.New(brand);
            brand.Id.Should().NotBeNull();

            brand.Descrip = "Mercedes";
            brandManager.Update(brand);

            brandManager.getbyID(brand.Id)?.Descrip.Should().Be("Mercedes");
            ((BrandManagerFake)brandManager).cleanCollection();
        }

        [Fact]
        public void updateMultiplesBrands()
        {
            for (int i = 0; i < 100; i++)
            {
                string descrip = Generate.Words(1, true, true);
                var brand = new Brand()
                {
                    Descrip = descrip
                };

                brand = brandManager.New(brand);
                brand.Id.Should().NotBeNull();

                descrip = Generate.Words(1, true, true);
                brand.Descrip = descrip;
                brandManager.Update(brand);

                brandManager.getbyID(brand.Id)?.Descrip.Should().Be(descrip);
            }
            ((BrandManagerFake)brandManager).cleanCollection();
        }

        [Fact]
        public void deleteOneBrand()
        {
            var brand = new Brand();
            brand = brandManager.New(brand);

            brand.Id.Should().NotBeNull();
            brandManager.Delete(brand);

            brandManager.getbyID(brand.Id).Should().BeNull();
        }

        [Fact]
        public void deleteMultiplesBrands()
        {
            for (int i = 0; i < 100; i++)
            {                
                var brand = new Brand();
                brand = brandManager.New(brand);

                brand.Id.Should().NotBeNull();
                brandManager.Delete(brand);

                brandManager.getbyID(brand.Id)?.Should().BeNull();
            }
        }

        [Fact]
        public void deleteOneBrandbyID()
        {
            var brand = new Brand();
            brand = brandManager.New(brand);

            brand.Id.Should().NotBeNull();
            brandManager.DeletebyId(brand.Id);

            brandManager.getbyID(brand.Id).Should().BeNull();
        }

        [Fact]
        public void deleteMultiplesBrandsbyID()
        {
            for (int i = 0; i < 100; i++)
            {
                var brand = new Brand();
                brand = brandManager.New(brand);

                brand.Id.Should().NotBeNull();
                brandManager.DeletebyId(brand.Id);

                brandManager.getbyID(brand.Id)?.Should().BeNull();
            }
        }

        [Fact]
        public void getAllBrands()
        {
            var brands = new List<Brand>();
            for (int i = 0; i < 100; i++)
            {
                string descrip = Generate.Words(1, true, true);
                var brand = new Brand()
                {
                    Descrip = descrip
                };

                brands.Add(brand);
                brand = brandManager.New(brand);
                brand.Id.Should().NotBeNull();                
            }

            var brandsDDBB = brandManager.getAll();
            brands.Count.Should().Be(brandsDDBB.Count);            
            brandsDDBB.Should().BeEquivalentTo(brands);

             ((BrandManagerFake)brandManager).cleanCollection();
        }

        public void Dispose()
        {            
            ((BrandManagerFake)brandManager).dropDataBase();
        }
    }
}
