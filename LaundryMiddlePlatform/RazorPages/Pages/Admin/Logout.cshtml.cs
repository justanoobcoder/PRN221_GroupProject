using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NguyenTienPhatRazorPage.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Clear the user session
            HttpContext.Session.Clear();

            // Redirect to the login page or any other desired page
            return RedirectToPage("/Login");
        }
    }
}
