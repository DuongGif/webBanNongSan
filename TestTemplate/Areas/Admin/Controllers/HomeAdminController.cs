using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
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
        public IActionResult DanhMucNongSan(int? page)
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
            // Tạo danh sách các lựa chọn cho dropdown list
            ViewBag.MaLoai = new SelectList(db.LoaiNongSans.ToList(), "MaLoai", "TenLoai");
            ViewBag.MaNhaCungCap = new SelectList(db.NhaCungCaps.ToList(), "MaNhaCungCap", "TenNhaCungCap");
            return View();
        }

        [Route("ThemNongSanMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNongSanMoi(NongSan nongsan)
        {
            if (ModelState.IsValid) // Kiểm tra nếu model hợp lệ
            {
                db.NongSans.Add(nongsan); // Thêm nông sản vào cơ sở dữ liệu
                db.SaveChanges(); // Lưu thay đổi
                return RedirectToAction("DanhMucNongSan"); // Chuyển hướng tới danh mục nông sản
            }

            // Nếu có lỗi validation, quay lại trang thêm nông sản và hiển thị lỗi
            ViewBag.MaLoai = new SelectList(db.LoaiNongSans.ToList(), "MaLoai", "TenLoai");
            ViewBag.MaNhaCungCap = new SelectList(db.NhaCungCaps.ToList(), "MaNhaCungCap", "TenNhaCungCap");
            return View(nongsan); // Trả về view với model chứa lỗi
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
                return RedirectToAction("DanhMucNongSan", "HomeAdmin");
            }
            return View(nongsan);
        }

        [Route("XoaNongSan")]
        [HttpGet]
        public IActionResult XoaNongSan(string maNongSan)
        {
            TempData["Message"] = "";
            var chiTietNongSan = db.Khos.Where(x => x.MaNongSan == maNongSan).ToList();
            if (chiTietNongSan.Count > 0)
            {
                TempData["Message"] = "Không xóa được nông sản này";
                return RedirectToAction("DanhMucNongSan", "HomeAdmin");
            }
            var anhNongSan = db.AnhNongSans.Where(x => x.MaNongSan == maNongSan);
            if (anhNongSan.Any()) db.RemoveRange(anhNongSan);
            db.Remove(db.NongSans.Find(maNongSan));
            db.SaveChanges();
            TempData["Message"] = "Nông sản đã được xóa";
            return RedirectToAction("DanhMucNongSan", "HomeAdmin");


        }

        [Route("ThemAnhNongSan")]
        [HttpGet]
        public IActionResult ThemAnhNongSan(string maNongSan)
        {
            var nongSan = db.NongSans.Find(maNongSan);
            if (nongSan == null)
            {
                ViewBag.Error = "Không tìm thấy nông sản.";
                return View();
            }
            ViewBag.maNongSan = maNongSan;
            return View(nongSan);
        }

        [Route("ThemAnhNongSan")]
        [HttpPost]
        public IActionResult ThemAnhNongSan(string maNongSan, IFormFile fileAnh)
        {
            // Tìm nông sản theo maNongSan
            var nongSan = db.NongSans.Find(maNongSan);
            if (nongSan == null)
            {
                ViewBag.Error = "Không tìm thấy nông sản.";
                return View();
            }

            // 1. Kiểm tra xem file có null hoặc có độ lớn = 0 hay không
            if (fileAnh == null || fileAnh.Length == 0)
            {
                ViewBag.Error = fileAnh == null ? "Chưa chọn file" : "File không có nội dung";
                return View(nongSan);
            }

            // 2. Xác định đường dẫn lưu file
            var relativePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");

            // Đảm bảo thư mục tồn tại
            if (!Directory.Exists(relativePath))
            {
                Directory.CreateDirectory(relativePath);
            }

            // Tạo tên file với tên nông sản và đuôi file thực tế
            string fileExtension = Path.GetExtension(fileAnh.FileName);
            string fileName = $"{nongSan.TenNongSan}{fileExtension}"; // Tên nông sản + định dạng
            string absolutePath = Path.Combine(relativePath, fileName);

            // 3. Lưu file
            using (var stream = new FileStream(absolutePath, FileMode.Create))
            {
                fileAnh.CopyTo(stream);
            }

            // 4. Cập nhật đường dẫn ảnh vào cơ sở dữ liệu
            string imageUrl = $"{nongSan.TenNongSan}{fileExtension}"; // Đường dẫn ảnh chỉ gồm tên nông sản + định dạng
            nongSan.DuongDanAnh = imageUrl; // Cập nhật đường dẫn ảnh

            // Lưu thay đổi cho nông sản
            db.SaveChanges();
            return RedirectToAction("DanhMucNongSan", "HomeAdmin");
        }

        // nha cung cap
        [Route("DanhMucNhaCungCap")]
        public IActionResult DanhMucNhaCungCap(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var listncc = db.NhaCungCaps.OrderBy(x => x.TenNhaCungCap);
            PagedList<NhaCungCap> list = new PagedList<NhaCungCap>(listncc, pageNumber, pageSize);
            return View(list);
        }
        [Route("ThemNhaCungCapMoi")]
        [HttpGet]
        public IActionResult ThemNhaCungCapMoi()
        {
            return View();
        }

        [Route("ThemNhaCungCapMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNhaCungCapMoi(NhaCungCap NhaCungCap)
        {
            if (ModelState.IsValid)
            {
                db.NhaCungCaps.Add(NhaCungCap); 
                db.SaveChanges(); 
                return RedirectToAction("DanhMucNhaCungCap");
            }
            return View(NhaCungCap); 
        }

        [Route("SuaNhaCungCap")]
        [HttpGet]
        public IActionResult SuaNhaCungCap(string maNhaCungCap)
        {
           
            var nhacungcap = db.NhaCungCaps.Find(maNhaCungCap);
            return View(nhacungcap);
        }

        [Route("SuaNhaCungCap")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNhaCungCap(NhaCungCap nhacungcap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhacungcap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucNhaCungCap", "HomeAdmin");
            }
            return View(nhacungcap);
        }


        [Route("XoaNhaCungCap")]
        [HttpGet]
        public IActionResult XoaNhaCungCap(string maNhaCungCap)
        {
            TempData["Message"] = "";
            
            var nhacungcap = db.NongSans.Where(x => x.MaNhaCungCap == maNhaCungCap).ToList();
            if (nhacungcap.Count > 0)
            {
                TempData["Message"] = "Không xóa được nhà cung cấp này vì vẫn đang có nông sản đang bán";
                return RedirectToAction("DanhMucNhaCungCap", "HomeAdmin");
            }

            db.Remove(db.NhaCungCaps.Find(maNhaCungCap));
            db.SaveChanges();
            TempData["Message"] = "Nhà cung cấp đã được xóa";
            return RedirectToAction("DanhMucNhaCungCap", "HomeAdmin");


        }
    }
}
