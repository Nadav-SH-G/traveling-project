using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace project.Pages
{
    public class page3Model : PageModel
    {
        public IActionResult OnGet()
        {
            string login = HttpContext.Session.GetString("Login");
            if (string.IsNullOrEmpty(login))
            {
                return RedirectToPage("/AccessDenied");
            }
            return Page();
        }
    }
}
