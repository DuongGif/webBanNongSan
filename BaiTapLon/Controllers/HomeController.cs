using Azure;
using baitaplon.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

namespace baitaplon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        QlnongSanNewContext db = new QlnongSanNewContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int ?page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var listsp = db.NongSans.OrderBy(x => x.TenNongSan);
            PagedList<NongSan> list = new PagedList<NongSan>(listsp,pageNumber,pageSize);
            return View(list);
        }

        public IActionResult NongSanTheoLoai(string maNongSan,int ? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var listsp = db.NongSans.Where(x => x.MaLoai == maNongSan).OrderBy(x => x.TenNongSan);
            ViewBag.manongsan = maNongSan;
            PagedList<NongSan> list = new PagedList<NongSan>(listsp, pageNumber, pageSize);
            ViewBag.RecordCount = list.Count;
            return View(list);
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