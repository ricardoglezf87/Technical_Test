using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Technical_Test.Models;
using Technical_Test.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Technical_Test.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly UserManager<SiteUser> _userManager;

        public CarController(UserManager<SiteUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult listCars()
        {            

            var cars = CarManager.getAll()?.Where(x=> x.User_id.Equals(_userManager.GetUserId(User)));

            ViewData["Models"] = ModelManager.getAll();
            ViewData["Brands"] = BrandManger.getAll();

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
                    car.NumberPlate = car.NumberPlate.ToUpper();
                    car.User_id = _userManager.GetUserId(User);
                    CarManager.New(car);
                    ViewBag.Message = "Car added Suscessfully!";
                    ModelState.Clear();
                }
                else
                {
                    //Classify the errors cast
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
            var car = CarManager.getbyID(car_id);
            return View(car);
        }

        [HttpPost]
        public IActionResult deleteCar(Car car)
        {            
            CarManager.Delete(car);
            return RedirectToAction("listCars","Car");
        }

        public IActionResult updateCar(string car_id)
        {            
            var car = CarManager.getbyID(car_id);
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
                    car.NumberPlate = car.NumberPlate.ToUpper();
                    car.User_id = _userManager.GetUserId(User);
                    CarManager.Update(car);
                    ViewBag.Message = "Car modify Suscessfully!";
                    ModelState.Clear();
                }
                else
                {
                    //Classify the errors cast
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

        /// <summary>
        /// Get a list of SelectListItem of brands, that it'll be used for the DropBoxList
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> getBrands()
        {
            var lSelec = new List<SelectListItem>();            

            lSelec.Add(new SelectListItem() { Text = "Select a brand", Value = String.Empty });

            foreach (var brand in BrandManger.getAll())
            {
                lSelec.Add(new SelectListItem() { Text = brand.Descrip, Value = brand.Id });
            }

            return lSelec;
        }

        /// <summary>
        /// Get a list of SelectListItem of models filtering by id brand, that it'll be used for the DropBoxList
        /// </summary>
        /// <param name="brand_id">id of brand (string)</param>
        /// <returns></returns>
        private List<SelectListItem> getModels(string brand_id)
        {            
            var lSelec = new List<SelectListItem>();

            lSelec.Add(new SelectListItem() { Text = "Select a model", Value = String.Empty });

            foreach (var model in ModelManager.getbyIdBrand(brand_id))
            {
                lSelec.Add(new SelectListItem() { Text = model.Descrip, Value = model.Id });
            }
            return lSelec;
        }

        /// <summary>
        ///  Get a Json of list of SelectListItem of models filtering by id brand, that it'll be used for the DropBoxList
        ///  this
        /// </summary>
        /// <param name="brand_id">id of brand (string)</param>
        /// <returns></returns>
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

    }
}