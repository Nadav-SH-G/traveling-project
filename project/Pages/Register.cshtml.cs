using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using project.Model;

namespace project.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string FirstName { get; set; } 

        [BindProperty]
        public string LastName { get; set; } 

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string SPassword { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Phone { get; set; }

        [BindProperty]
        public DateTime? BirthDay { get; set; }

        [BindProperty]
        public string Msg { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            // בדיקות בסיסיות
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(SPassword))
            {
                Msg = "אנא מלא את כל השדות הנדרשים.";
                return Page();
            }

            if (Password != SPassword)
            {
                Msg = "הסיסמאות לא תואמות.";
                return Page();
            }

            Helper helper = new Helper();

            // בדיקה אם שם המשתמש כבר קיים
            string checkUserSql = "SELECT COUNT(*) FROM Users WHERE Username = @username";
            SqlParameter[] checkParams = {
                new SqlParameter("@username", UserName)
            };
            DataTable dtCheck = helper.RetrieveTable(checkUserSql, "Users", checkParams);

            if (dtCheck.Rows.Count > 0 && Convert.ToInt32(dtCheck.Rows[0][0]) > 0)
            {
                Msg = "שם המשתמש כבר קיים במערכת.";
                return Page();
            }

            // הוספת משתמש חדש
            string insertSql = @"
    INSERT INTO Users (FirstName, LastName, Username, Password, Email, Phone, BirthDay, Admin)
    VALUES (@FirstName, @LastName, @Username, @Password, @Email, @Phone, @BirthDay, 0)";

            SqlParameter[] insertParams = {
    new SqlParameter("@FirstName", FirstName ?? (object)DBNull.Value),
    new SqlParameter("@LastName", LastName ?? (object)DBNull.Value),
    new SqlParameter("@Username", UserName),
    new SqlParameter("@Password", Password),
    new SqlParameter("@Email", Email ?? (object)DBNull.Value),
    new SqlParameter("@Phone", Phone ?? (object)DBNull.Value),
    new SqlParameter("@BirthDay", BirthDay ?? (object)DBNull.Value)
    // שימו לב: אין כאן פרמטר עבור Gender
};

            helper.ExecuteCommand(insertSql, insertParams);

            return RedirectToPage("/Login");
        }
    }
}
