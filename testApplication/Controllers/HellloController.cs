using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testApplication.Controllers
{
    public class HellloController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
