using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Technical_Test.Models;
using Technical_Test.Services;

namespace Technical_Test.Controllers
{
    public class CarController : Controller
    {
        private readonly IConfiguration Configuration;

        public CarController(IConfiguration config)
        {
            Configuration = config;
        }

        private List<SelectListItem> getBrands()
        {
            var lSelec = new List<SelectListItem>();
            var brandServices = new BrandService(Configuration);

            lSelec.Add(new SelectListItem() { Text = "Select a brand", Value = String.Empty });

            foreach (var brand in brandServices.getAll())
            {
                lSelec.Add(new SelectListItem() { Text = brand.Descrip, Value = brand.Id });
            }

            return lSelec;
        }

        private List<SelectListItem> getModelbyIdBrand()
        {
            var lSelec = new List<SelectListItem>();
            var brandServices = new BrandService(Configuration);

            foreach (var brand in brandServices.getAll())
            {
                lSelec.Add(new SelectListItem() { Text = brand.Descrip, Value = brand.Id });
            }

            return lSelec;
        }

        private List<SelectListItem> getModels(string brand_id)
        {
            var modelServices = new ModelService(Configuration);

            var lSelec = new List<SelectListItem>();            

            lSelec.Add(new SelectListItem() { Text = "Select a model", Value = String.Empty });

            foreach (var model in modelServices.getbyIdBrand(brand_id))
            {
                lSelec.Add(new SelectListItem() { Text = model.Descrip, Value = model.Id });
            }
            return lSelec;
        }



        [HttpPost]
        public JsonResult LoadModelsByIdBrand(string brand_id)
        {
            if (!String.IsNullOrEmpty(brand_id))
            {                
                return Json(new SelectList(getModels(brand_id), "Value", "Text"));
            }
            else
            {
                return null;
            }
            
        }

        public IActionResult listCars()
        {
            var carServices = new CarService(Configuration);
            var cars = carServices.getAll();

            var modelServices = new ModelService(Configuration);
            ViewData["Models"] = modelServices.getAll();

            var brandServices = new BrandService(Configuration);
            ViewData["Brands"] = brandServices.getAll();

            return View(cars);
        }

        public IActionResult newCar()
        {            
            ViewData["Brands"] = getBrands();            
            return View();
        }

        [HttpPost]
        public ActionResult newCar(Car car)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var carService = new CarService(Configuration);
                    car.NumberPlate = car.NumberPlate.ToUpper();
                    carService.New(car);
                    ViewBag.Message = "Car added Suscessfully!";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.MessageError = "Errors: ";
                    ViewBag.MessageError += "<lu>";
                    foreach (var modelState in ViewData.ModelState.Values)
                    {
                        foreach (var error in modelState.Errors)
                        {
                            ViewBag.MessageError += $"<li>{error.ErrorMessage}</li>";
                        }
                    }

                    ViewBag.MessageError += "</lu>";

                    ViewData["Models"] = getModels(car.Brand_id);
                }

                ViewData["Brands"] = getBrands();

                return View();
            }
            catch (Exception e)
            {
                ViewBag.MessageError = $"Error adding the car: {e.Message}";
                return View();
            }

        }

        public IActionResult deleteCar(string car_id)
        {
            var carService = new CarService(Configuration);
            var car = carService.getbyID(car_id);
            return View(car);
        }

        [HttpPost]
        public IActionResult deleteCar(Car car)
        {
            var carService = new CarService(Configuration);
            carService.Delete(car);
            return RedirectToAction("listCars","Car");
        }

        public IActionResult updateCar(string car_id)
        {
            var carService = new CarService(Configuration);
            var car = carService.getbyID(car_id);
            ViewData["Brands"] = getBrands();
            ViewData["Models"] = getModels(car.Brand_id);
            return View(car);
        }

        [HttpPost]
        public ActionResult updateCar(Car car)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var carService = new CarService(Configuration);
                    car.NumberPlate = car.NumberPlate.ToUpper();
                    carService.Update(car);
                    ViewBag.Message = "Car modify Suscessfully!";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.MessageError = "Errors: ";
                    ViewBag.MessageError += "<lu>";
                    foreach (var modelState in ViewData.ModelState.Values)
                    {
                        foreach (var error in modelState.Errors)
                        {
                            ViewBag.MessageError += $"<li>{error.ErrorMessage}</li>";
                        }
                    }

                    ViewBag.MessageError += "</lu>";
                    ViewData["Models"] = getModels(car.Brand_id);
                }

                ViewData["Brands"] = getBrands();

                return View(car);
            }
            catch (Exception e)
            {
                ViewBag.MessageError = $"Error modifying the car: {e.Message}";
                return View(car);
            }

        }

    }
}