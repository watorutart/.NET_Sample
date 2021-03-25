using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testApplication.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            return Content("こんにちは、世界！！！");
        }
    }
}
