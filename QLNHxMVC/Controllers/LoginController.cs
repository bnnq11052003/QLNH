using Microsoft.AspNetCore.Mvc;
using QLNHxMVC.Models;


namespace MVC_QLNH.Controllers
{
    public class LoginController : Controller
    {
        LatMvcQlnhContext db = new LatMvcQlnhContext();
        [HttpGet]
        public IActionResult IndexLogin()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Login(TbAccount tk)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                var u = db.TbAccounts.Where(x => x.Username.Equals(tk.Username) && x.Password.Equals(tk.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("Username", u.Username.ToString());
                    HttpContext.Session.SetString("AccountType", u.AccountType.ToString());
                    HttpContext.Session.SetInt32("AccountId", u.AccountId);
                    var fn = db.TbUserInfos.Where(f => f.AccountId == u.AccountId).FirstOrDefault();
                    if(fn != null)
                    {
                        HttpContext.Session.SetString("FullName", fn.FullName);
                    }
                    


                    return RedirectToAction("Index", "Home");
                }
                // Login failed, set the ViewBag message
                ViewBag.Message = "Đăng nhập không thành công. Vui lòng kiểm tra lại tài khoản và mật khẩu.";
                return View("IndexLogin");

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa phiên làm việc
            return RedirectToAction("IndexLogin"); // Chuyển hướng về trang đăng nhập
        }
    }
}
