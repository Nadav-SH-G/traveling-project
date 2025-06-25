using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace project.Pages
{
    public class LogOutModel : PageModel
    {
        public IActionResult OnGet()
        {
            
            HttpContext.Session.Clear();

            return RedirectToPage("/Index");
        }
    }
}
