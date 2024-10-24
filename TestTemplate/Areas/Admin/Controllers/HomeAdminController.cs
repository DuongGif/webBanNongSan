using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestTemplate.Models;
using X.PagedList;

namespace TestTemplate.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Admin/HomeAdmin")]
    public class HomeAdminController : Controller
    {
        QlnongSanNewContext db = new QlnongSanNewContext();

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("DanhMucNongSan")]
        public IActionResult DanhMucNongSan(int ? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var listsp = db.NongSans.OrderBy(x => x.TenNongSan);
            PagedList<NongSan> list = new PagedList<NongSan>(listsp, pageNumber, pageSize);
            return View(list);
        }

        [Route("ThemNongSanMoi")]
        [HttpGet]
        public IActionResult ThemNongSanMoi()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiNongSans.ToList(), "MaLoai", "TenLoai");
            ViewBag.MaNhaCungCap = new SelectList(db.NhaCungCaps.ToList(), "MaNhaCungCap", "TenNhaCungCap");
            return View();
        }

        [Route("ThemNongSanMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNongSanMoi(NongSan nongsan)
        {
            if (ModelState.IsValid)
            {
                db.NongSans.Add(nongsan);
                db.SaveChanges();
                return RedirectToAction("DanhMucNongSan");
            }
            return View(nongsan);
        }


        [Route("SuaNongSan")]
        [HttpGet]
        public IActionResult SuaNongSan(string maNongSan)
        {
            ViewBag.MaLoai = new SelectList(db.LoaiNongSans.ToList(), "MaLoai", "TenLoai");
            ViewBag.MaNhaCungCap = new SelectList(db.NhaCungCaps.ToList(), "MaNhaCungCap", "TenNhaCungCap");
            var nongSan = db.NongSans.Find(maNongSan);  
            return View(nongSan);
        }

        [Route("SuaNongSan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNongSan(NongSan nongsan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nongsan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucNongSan","HomeAdmin");
            }
            return View(nongsan);
        }

        [Route("XoaNongSan")]
        [HttpGet]
        public IActionResult XoaNongSan(string maNongSan)
        {
            TempData["Message"] = "";
            var chiTietNongSan = db.Khos.Where(x=>x.MaNongSan == maNongSan).ToList();
            if (chiTietNongSan.Count > 0)
            {
                TempData["Message"] = "Không xóa được nông sản này";
                return RedirectToAction("DanhMucNongSan", "HomeAmin");
            }
            var anhNongSan = db.AnhNongSans.Where(x=>x.MaNongSan==maNongSan);
            if (anhNongSan.Any()) db.RemoveRange(anhNongSan);
            db.Remove(db.NongSans.Find(maNongSan));
            db.SaveChanges();
            TempData["Message"] = "Nông sản đã được xóa";
            return RedirectToAction("DanhMucNongSan", "HomeAdmin");
             

        }
    }
}
