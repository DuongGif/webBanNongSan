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
            // Kiểm tra cookie "UserName"
            /* var userName = Request.Cookies["UserName"];
             if (userName != null)
             {
                 // Nếu cookie tồn tại, tự động đăng nhập và chuyển hướng
                 HttpContext.Session.SetString("UserName", userName);
                 return RedirectToAction("Index", "Home");
             }

             if (HttpContext.Session.GetString("UserName") == null)
             {
                 return View();
             }
             else
             {
                 return RedirectToAction("Index", "Home");
             }*/
            if (HttpContext.Session.GetString("UserName") == null)
            {
                // Kiểm tra cookie "UserName"
                var usernameCookie = Request.Cookies["UserName"];
                if (usernameCookie != null)
                {
                    // Nếu có cookie, tự động đăng nhập
                    HttpContext.Session.SetString("UserName", usernameCookie);
                    return RedirectToAction("Index", "Home");
                }

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Login(TaiKhoan user, bool RememberMe)
        {
            /*if (HttpContext.Session.GetString("UserName") == null)
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
            return View();*/
            
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan.Equals(user.TenTaiKhoan));
                if (u != null)
                {
                    if (u.MatKhau.Equals(user.MatKhau))
                    {
                        HttpContext.Session.SetString("UserName", u.TenTaiKhoan.ToString());

                        // Nếu RememberMe được chọn, tạo cookie
                        if (RememberMe) {
                            var cookieOptions = new CookieOptions
                            {
                                Expires = DateTime.Now.AddDays(7), // Đặt cookie hết hạn sau 7 ngày
                                HttpOnly = true, // Đảm bảo cookie chỉ có thể được truy cập qua HTTP
                                Secure = Request.IsHttps, // Đảm bảo cookie chỉ được gửi qua HTTPS
                                SameSite = SameSiteMode.Strict // Đảm bảo cookie không bị gửi trong các request cross-site

                            };
                            // Lưu tên tài khoản vào cookie
                            Response.Cookies.Append("UserName", u.TenTaiKhoan, cookieOptions);

                        }
                           
                        
                        if (u.TenTaiKhoan == "admin")
                        {
                            return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("MatKhau", "Mật khẩu không đúng.");
                    }
                }
                else
                {
                    ModelState.AddModelError("TenTaiKhoan", "Bạn chưa có tài khoản, vui lòng đăng ký tài khoản mới.");
                }
            }
            return View(user);
        }

        public IActionResult Logout()
        {
            // Xóa cookie và session
            Response.Cookies.Delete("UserName");
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
            /* // Lấy danh sách khách hàng khi POST
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

             return View(model);*/
            // Lấy danh sách khách hàng khi POST
            var khachHangs = db.KhachHangs.ToList();
            ViewBag.KhachHangs = khachHangs;

            // Kiểm tra nếu MaTaiKhoan đã tồn tại
            if (db.TaiKhoans.Any(t => t.MaTaiKhoan == model.MaTaiKhoan))
            {
                ModelState.AddModelError("MaTaiKhoan", "Mã tài khoản đã tồn tại. Vui lòng nhập mã khác.");
                return View(model);
            }

            // Kiểm tra độ mạnh của mật khẩu
            if (string.IsNullOrWhiteSpace(model.MatKhau) ||
                !model.MatKhau.Any(char.IsUpper) ||
                !model.MatKhau.Any(char.IsLower) ||
                !model.MatKhau.Any(char.IsDigit) ||
                !model.MatKhau.Any(ch => !char.IsLetterOrDigit(ch)) ||
                model.MatKhau.Length < 6)
            {
                ModelState.AddModelError("MatKhau", "Mật khẩu phải có ít nhất 6 ký tự, bao gồm chữ hoa, chữ thường, chữ số và ký tự đặc biệt.");
                return View(model);
            }

            // Kiểm tra mật khẩu và xác nhận mật khẩu có khớp không
            if (model.MatKhau != model.ConfirmMatKhau)
            {
                ModelState.AddModelError("ConfirmMatKhau", "Mật khẩu xác nhận không khớp.");
                return View(model);
            }

            // Kiểm tra mã khách hàng
            if (model.MaKhachHang == null)
            {
                ModelState.AddModelError("MaKhachHang", "Vui lòng chọn mã khách hàng.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                // Gán mã kiểu tài khoản mặc định là KT02
                model.MaKieuTaiKhoan = "KT02";

                // Gán ngày tạo là ngày hiện tại
                model.NgayTao = DateTime.Now;

                // Lưu tài khoản vào cơ sở dữ liệu
                db.TaiKhoans.Add(model);
                db.SaveChanges();

                return RedirectToAction("Login"); // Redirect đến trang đăng nhập sau khi đăng ký thành công
            }

            return View(model);
        }




    }
}
