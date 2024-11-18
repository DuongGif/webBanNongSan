using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using TestTemplate.Models;

namespace TestTemplate.Controllers
{
    public class AccessController : Controller
    {
        QlnongSanNewContext db = new QlnongSanNewContext();



        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public IActionResult Login(TaiKhoan user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.TaiKhoans.Where(x => x.TenTaiKhoan.Equals(user.TenTaiKhoan) && x.MatKhau.Equals(user.MatKhau)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.TenTaiKhoan.ToString());

                    
                    if (u.TenTaiKhoan == "admin") 
                    {
                        return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }
        [HttpGet]
        public IActionResult Register()
        {
            // Lấy danh sách khách hàng từ cơ sở dữ liệu
            var khachHangs = db.KhachHangs.ToList(); // Lấy danh sách khách hàng
            if (khachHangs == null || !khachHangs.Any())
            {
                ModelState.AddModelError("", "Không có khách hàng nào trong hệ thống.");
                return View();
            }

            // Đảm bảo bạn truyền danh sách khách hàng vào ViewBag
            ViewBag.KhachHangs = khachHangs;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(TaiKhoan model)
        {
            // Lấy danh sách khách hàng khi POST
            var khachHangs = db.KhachHangs.ToList();
            ViewBag.KhachHangs = khachHangs;

            if (ModelState.IsValid)
            {
                // Kiểm tra mật khẩu và xác nhận mật khẩu có khớp không
                if (model.MatKhau != model.ConfirmMatKhau)
                {
                    ModelState.AddModelError("ConfirmMatKhau", "Mật khẩu xác nhận không khớp.");
                    return View(model);
                }

                // Gán mã kiểu tài khoản mặc định là KT02
                model.MaKieuTaiKhoan = "KT02";

                // Gán ngày tạo là ngày hiện tại
                model.NgayTao = DateTime.Now;

                // Kiểm tra mã khách hàng
                if (model.MaKhachHang == null)
                {
                    ModelState.AddModelError("MaKhachHang", "Vui lòng chọn mã khách hàng.");
                    return View(model);
                }

                // Lưu tài khoản vào cơ sở dữ liệu
                db.TaiKhoans.Add(model);
                db.SaveChanges();

                return RedirectToAction("Login"); // Redirect đến trang đăng nhập sau khi đăng ký thành công
            }

            return View(model);
        }




    }
}
