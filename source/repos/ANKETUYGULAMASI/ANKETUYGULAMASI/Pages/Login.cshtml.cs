using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ANKETUYGULAMASI.Models;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace ANKETUYGULAMASI.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _db;
        public string ErrorMessage { get; set; }

        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public LoginModel(AppDbContext db)
        {
            _db = db;
        }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Kullan�c� ad� ve �ifre gereklidir.";
                return Page();
            }

            string passwordHash = GetSha256Hash(Password);
            var user = _db.ManagerUsers.FirstOrDefault(u => u.UserName == UserName && u.PasswordHash == passwordHash);
            if (user == null)
            {
                ErrorMessage = "Kullan�c� ad� veya �ifre hatal�.";
                return Page();
            }

            // Giri� ba�ar�l�, session/cookie i�lemleri burada yap�labilir
            HttpContext.Session.SetString("UserName", user.UserName);

            // LOG EKLE
            _db.ManagerUserLogs.Add(new ManagerUserLog
            {
                UserName = user.UserName,
                Action = "Login",
                LogTime = DateTime.Now
            });
            _db.SaveChanges();

            return RedirectToPage("/Index");
        }

        private string GetSha256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}
