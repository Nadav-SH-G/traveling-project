using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using project.Model;

namespace project.Pages
{
    public class LoginModel : PageModel
    {
        public string msg { get; set; } = string.Empty;

        [BindProperty]
        public string userName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string SQLStr = "SELECT * FROM Users WHERE Username = @username AND Password = @password";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@username", userName),
        new SqlParameter("@password", Password)
            };

            Helper helper = new Helper();
            DataTable dt = helper.RetrieveTable(SQLStr, "Users", parameters);


            if (dt.Rows.Count > 0)
            {
                HttpContext.Session.SetString("Login", userName);
                HttpContext.Session.SetString("Admin", dt.Rows[0]["Admin"].ToString());
                return RedirectToPage("/Index");
            }

            msg = "שם משתמש או סיסמה שגויים.";
            return Page();
        }


    }
}
