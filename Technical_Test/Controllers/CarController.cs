using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
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

        [HttpGet]
        public JsonResult LoadModelsByIdBrand(string brand_id)
        {
            var modelServices = new ModelService(Configuration);
            
            var lSelec = new List<SelectListItem>();

            foreach (var model in modelServices.getbyIdBrand(brand_id))
            {
                lSelec.Add(new SelectListItem() { Text = model.Descrip, Value = model.Id });
            }

            return Json(lSelec);
            
        }

        public IActionResult newCar()
        {            
            ViewBag.Brands = getBrands();

            return View();
        }

        public IActionResult listCars()
        {
            return View();
        }
    }
}