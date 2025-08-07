using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ANKETUYGULAMASI.Models;
using System;

namespace ANKETUYGULAMASI.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly AppDbContext _db;
        public LogoutModel(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult OnPost()
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (!string.IsNullOrEmpty(userName))
            {
                _db.ManagerUserLogs.Add(new ManagerUserLog
                {
                    UserName = userName,
                    Action = "Logout",
                    LogTime = DateTime.Now
                });
                _db.SaveChanges();
            }
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }
        public IActionResult OnGet()
        {
            return RedirectToPage("/Login");
        }
    }
}
