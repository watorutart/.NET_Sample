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

        public IActionResult Greet()
        {
            // ビュー変数を用意
            ViewBag.Message = "こんにちは、世界！";
            // テンプレートを呼び出し
            return View();
        }
    }
}
