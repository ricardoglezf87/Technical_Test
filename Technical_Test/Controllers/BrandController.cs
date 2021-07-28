using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Technical_Test.DAL;
using Technical_Test.Models;

namespace Technical_Test.Controllers
{
    public class BrandController : Controller
    {
        [AllowAnonymous]
        public IActionResult listBrands()
        {            
            ViewData["Models"] = ModelManager.getAll();            
            ViewData["Brands"] = BrandManger.getAll();

            return View();
        }
    }
}