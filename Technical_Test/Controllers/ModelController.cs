using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Technical_Test.Controllers
{
    public class ModelController : Controller
    {
        public IActionResult listModels()
        {
            return View();
        }
    }
}