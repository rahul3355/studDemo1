using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using studDemo1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace studDemo1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //StudentContext db = new StudentContext();

        public IActionResult Index()
        {
            //return View(db.StudentTable.Where(x => x.Name.Contains(searching) || searching == null).ToList());
            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
