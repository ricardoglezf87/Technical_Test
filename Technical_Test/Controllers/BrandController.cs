using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Technical_Test.DAL;

namespace Technical_Test.Controllers
{
    public class BrandController : Controller
    {
        public IActionResult listBrands()
        {            
            ViewData["Models"] = ModelManager.getAll();            
            ViewData["Brands"] = BrandManger.getAll();

            return View();
        }
    }
}