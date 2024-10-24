using Microsoft.AspNetCore.Mvc;
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
            if(HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.TaiKhoans.Where(x => x.TenTaiKhoan.Equals(user.TenTaiKhoan) && x.MatKhau.Equals(user.MatKhau)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.TenTaiKhoan.ToString());
                    return RedirectToAction("Index", "Home");
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
    }
}
