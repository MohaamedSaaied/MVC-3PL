using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_3PL.Models;
using MVC_3PL.Services;
using System.Diagnostics;
using System.Text;

namespace MVC_3PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IScopedService sco1;
        //private readonly IScopedService sco2;
        //private readonly ITransientService tra1;
        //private readonly ITransientService tra2;
        //private readonly ISingletonService sin1;
        //private readonly ISingletonService sin2;

        public HomeController(
            ILogger<HomeController> logger
            //,IScopedService sco1,
            //IScopedService sco2,
            //ITransientService tra1,
            //ITransientService tra2,
            //ISingletonService sin1,
            //ISingletonService sin2
            )
        {
            _logger = logger;
            //this.sco1 = sco1;
            //this.sco2 = sco2;
            //this.tra1 = tra1;
            //this.tra2 = tra2;
            //this.sin1 = sin1;
            //this.sin2 = sin2;
        }
        //public string TestLifeTime()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append($"sco1::{sco1.GetGuid()}\n");
        //    sb.Append($"sco2::{sco2.GetGuid()}\n\n");
        //    sb.Append($"tra1::{tra1.GetGuid()}\n");
        //    sb.Append($"tra2::{tra2.GetGuid()}\n\n");
        //    sb.Append($"sin1::{sin1.GetGuid()}\n");
        //    sb.Append($"sin2::{sin2.GetGuid()}\n\n");
        //    return sb.ToString();
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
