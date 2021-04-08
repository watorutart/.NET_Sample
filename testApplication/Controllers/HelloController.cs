using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testApplication.Models;

namespace testApplication.Controllers
{
    public class HelloController : Controller
    {

        private readonly MyContext _context;

        [Route("Hoge/Foo")]
        public IActionResult Index()
        {
            return Content("こんにちは、世界！！！");
        }

        public IActionResult Greet()
        {
            // ビュー変数を用意
            ViewBag.Message = "<h1>こんにちは、世界！</h1>";
            // テンプレートを呼び出し
            return View();
        }

        public HelloController(MyContext context)
        {
            this._context = context;
        }

        public IActionResult List()
        {
            return View(this._context.Book);
        }
    }
}
