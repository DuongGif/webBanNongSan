using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestTemplate.Models;
using TestTemplate.Models.Authentication;
using X.PagedList;
using X.PagedList.Extensions;

namespace TestTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        QlnongSanNewContext db = new QlnongSanNewContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authentication]

        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var listsp = db.NongSans.OrderBy(x => x.TenNongSan);
            PagedList<NongSan> list = new PagedList<NongSan>(listsp, pageNumber, pageSize);
            return View(list);
        }

        [Authentication]

        public IActionResult NongSanTheoLoai(string maNongSan, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listsp = db.NongSans.Where(x => x.MaLoai == maNongSan).OrderBy(x => x.TenNongSan);
            ViewBag.manongsan = maNongSan;
            PagedList<NongSan> list = new PagedList<NongSan>(listsp, pageNumber, pageSize);
            return View(list);
        }

        public IActionResult ChiTietNongSan(string maNongSan)
        {
            var nongSan = db.NongSans.FirstOrDefault(x => x.MaNongSan == maNongSan);
            return View(nongSan);
        }

        public IActionResult LoadNongSan(int page = 1)
        {
            var listNongSan = db.NongSans.ToPagedList(page, 10); // 10 sản phẩm mỗi trang
            return PartialView("_NongSanPartial", listNongSan);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}