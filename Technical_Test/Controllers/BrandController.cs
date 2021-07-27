using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Technical_Test.Services;

namespace Technical_Test.Controllers
{
    public class BrandController : Controller
    {
        private readonly IConfiguration Configuration;

        public BrandController(IConfiguration config)
        {
            Configuration = config;
        }

        public IActionResult listBrands()
        {
            var modelServices = new ModelService(Configuration);
            ViewData["Models"] = modelServices.getAll();

            var brandServices = new BrandService(Configuration);
            ViewData["Brands"] = brandServices.getAll();

            return View();
        }
    }
}