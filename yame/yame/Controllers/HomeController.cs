using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using yame.Models;

namespace yame.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NonBranded()
        {
            return View();
        }
        public IActionResult SeventySeven()
        {
            return View();
        }
        public IActionResult NoStyle()
        {
            return View();
        }
        public IActionResult ThoiTrangGiatot()
        {
            return View();
        }
        public IActionResult Phukiengiatot()
        {
            return View();
        }

        public IActionResult SaleKetThucVongDoi()
        {
            return View();
        }
        public IActionResult pass()
        {
            return View();
        }
        public IActionResult AoThunTron()
        {
            return View();
        }
        public IActionResult AoThunCoHinh()
        {
            return View();
        }
        public IActionResult AoThunCoMau()
        {
            return View();
        }
        public IActionResult SelectLogin()
        {
            return View();
        }
        public IActionResult Moi()
        {
            return View();
        }
        public IActionResult CuaHang()
        {
            return View();
        }
        public IActionResult GioiThieu()
        {
            return View();
        }
        public IActionResult ThanhToan()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize(Roles = "Admin")]
        public IActionResult admintest()
        {
            return View();
        }

    }
}
