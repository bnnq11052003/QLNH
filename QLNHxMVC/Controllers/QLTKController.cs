using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLNHxMVC.Models;

namespace QLNHxMVC.Controllers
{
    public class QLTKController : Controller
    {
        private readonly LatMvcQlnhContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QLTKController(LatMvcQlnhContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: QLTK
        public async Task<IActionResult> Index()
        {
            ViewBag.AccountTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "Quản lý" },
                new SelectListItem { Value = "1", Text = "Nhân viên" }
            };
            var ds = from x in _context.TbUserInfos
                     join y in _context.TbAccounts
                     on x.AccountId equals y.AccountId
                     select new
                     {
                         UsID = x.UserInfoId,
                         HT = x.FullName,
                         AccountType = y.AccountType == "0" ? "Quản lý" : "Nhân viên",

                     };
            var result = ds.ToList();

            // Sử dụng ViewBag để truyền danh sách ds ở trên
            ViewBag.DsNV = result;
            return View();
        }
        public async Task<IActionResult> DetailLogin(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var tbUserInfo = await _context.TbUserInfos
                .Include(t => t.Account)
                .FirstOrDefaultAsync(m => m.UserInfoId == id);
            if (tbUserInfo == null)
            {
                return NotFound();
            }

            return View(tbUserInfo);
        }

        // GET: QLTK/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbUserInfo = await _context.TbUserInfos
                .Include(t => t.Account)
                .FirstOrDefaultAsync(m => m.UserInfoId == id);
            if (tbUserInfo == null)
            {
                return NotFound();
            }

            return View(tbUserInfo);
        }

        // GET: QLTK/Create
        public IActionResult Create()
        {
            ViewBag.AccountTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "Quản lý" },
                new SelectListItem { Value = "1", Text = "Nhân viên" }
            };
            return View();
        }

        // POST: QLTK/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbUserInfo tbUserInfo, string username, string password, string accountType)
        {
            if (ModelState.IsValid)
            {
                // Tạo một đối tượng TbAccount mới
                var account = new TbAccount
                {
                    Username = username,
                    Password = password,
                    AccountType = accountType
                };

                // Thêm tài khoản vào cơ sở dữ liệu và lưu thay đổi
                _context.TbAccounts.Add(account);
                await _context.SaveChangesAsync();

                // Thiết lập AccountId của tbUserInfo với tài khoản vừa được tạo
                tbUserInfo.AccountId = account.AccountId;

                // Thêm thông tin người dùng vào cơ sở dữ liệu và lưu thay đổi
                _context.TbUserInfos.Add(tbUserInfo);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.AccountTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "Quản lý" },
                new SelectListItem { Value = "1", Text = "Nhân viên" }
            };
            return View(tbUserInfo);
        }

        // GET: QLTK/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbUserInfo = await _context.TbUserInfos.FindAsync(id);
            if (tbUserInfo == null)
            {
                return NotFound();
            }
            // Cung cấp danh sách lựa chọn cho giới tính
            ViewBag.SexList = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Nam" },
                new SelectListItem { Value = "0", Text = "Nữ" }
            };
            ViewBag.AccountTypeList = new List<SelectListItem>
            {
                new SelectListItem { Value = "2", Text = "Quản lý" },
                new SelectListItem { Value = "1", Text = "Nhân viên" }
            };
            // Cung cấp danh sách lựa chọn cho chức vụ
            
            ViewData["AccountId"] = new SelectList(_context.TbAccounts, "AccountId", "AccountId", tbUserInfo.AccountId);
            return View(tbUserInfo);
        }
        // POST: QLTK/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserInfoId,AccountId,FullName,BirthDay,Sex,Email,PhoneNumber")] TbUserInfo tbUserInfo)
        {
            if (id != tbUserInfo.UserInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbUserInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbUserInfoExists(tbUserInfo.UserInfoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SexList = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Nam" },
                new SelectListItem { Value = "0", Text = "Nữ" }
            };
            ViewBag.AccountTypeList = new List<SelectListItem>
            {
                new SelectListItem { Value = "2", Text = "Quản lý" },
                new SelectListItem { Value = "1", Text = "Nhân viên" }
            };
            ViewData["AccountId"] = new SelectList(_context.TbAccounts, "AccountId", "AccountId", tbUserInfo.AccountId);
            return View(tbUserInfo);
        }

        // GET: QLTK/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbUserInfo = await _context.TbUserInfos
                .Include(t => t.Account)
                .FirstOrDefaultAsync(m => m.UserInfoId == id);
            if (tbUserInfo == null)
            {
                return NotFound();
            }

            return View(tbUserInfo);
        }

        // POST: QLTK/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var tbUserInfo = await _context.TbUserInfos.FindAsync(id);
            if (tbUserInfo != null)
            {
                _context.TbUserInfos.Remove(tbUserInfo);
                await _context.SaveChangesAsync();  // Lưu thay đổi sau khi xóa tbUserInfo
            }

            // Xóa các tài khoản không tồn tại trong tbUserInfo
            var accountIdsInUserInfo = _context.TbUserInfos.Select(ui => ui.AccountId).ToList();
            var accountsToDelete = _context.TbAccounts.Where(a => !accountIdsInUserInfo.Contains(a.AccountId)).ToList();

            if (accountsToDelete.Any())
            {
                _context.TbAccounts.RemoveRange(accountsToDelete);
                await _context.SaveChangesAsync();  // Lưu thay đổi sau khi xóa các tài khoản
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TbUserInfoExists(int id)
        {
            return _context.TbUserInfos.Any(e => e.UserInfoId == id);
        }


        // GET: QLTK/ChangePassword
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        // POST: QLTK/ChangePassword
        // POST: QLTK/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Lấy thông tin tài khoản của người dùng đang đăng nhập từ session hoặc bất kỳ phương thức nào phù hợp
                var accountId = _httpContextAccessor.HttpContext.Session.GetInt32("AccountId");
                var account = await _context.TbAccounts.FindAsync(accountId);

                // Kiểm tra mật khẩu cũ
                if (model.OldPassword != account.Password)
                {
                    ModelState.AddModelError("OldPassword", "Mật khẩu cũ không chính xác.");
                    return View(model);
                }

                // Kiểm tra xác nhận mật khẩu mới
                if (model.NewPassword != model.ConfirmNewPassword)
                {
                    ModelState.AddModelError("ConfirmNewPassword", "Xác nhận mật khẩu mới không khớp.");
                    return View(model);
                }

                // Cập nhật mật khẩu mới
                account.Password = model.NewPassword;
                _context.TbAccounts.Update(account);
                await _context.SaveChangesAsync();


                TempData["PasswordChangeSuccess"] = "Đổi mật khẩu thành công.";
                return RedirectToAction(nameof(ChangePassword));
            }

            return View(model);
        }

    }
}