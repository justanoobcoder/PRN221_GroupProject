using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Admin;
using RazorPages.Models;
using RazorPages.Utils;
using Repositories;

namespace RazorPages.Pages;

[BindProperties]
public class LoginModel : PageModel
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IStoreRepository _storeRepository;

    public LoginModel(ICustomerRepository customerRepository, IStoreRepository storeRepository)
    {
        _customerRepository = customerRepository;
        _storeRepository = storeRepository;
    }

    public string Phone { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? ErrorMessage { get; set; } = default!;

    public IActionResult OnGet()
    {
        var currentUser = HttpContext.Session.GetObjectFromJson<CurrentUser>(Constants.SessionKey.CurrentUserKey);
        if (currentUser != null)
        {
            if (currentUser.Role == Constants.Role.Admin)
                return RedirectToPage("/Admin/AdminPage");
            else if (currentUser.Role == Constants.Role.Customer)
                return RedirectToPage("/Customer/Index");
            else if (currentUser.Role == Constants.Role.Store)
                return RedirectToPage("/Store/Index");
            else return RedirectToPage("/Login");
        }
        return Page();
    }

    public IActionResult OnPost()
    {
        if (AdminAccount.IsAdmin(Phone, Password))
        {
            HttpContext.Session.SetObjectAsJson(Constants.SessionKey.CurrentUserKey, new CurrentUser
            {
                Name = "Admin",
                Role = Constants.Role.Admin,
            });
            return RedirectToPage("/Admin/AdminPage");
        }
        else
        {
            var customer = _customerRepository.GetByPhoneAndPassword(Phone, Password);
            if (customer == null)
            {
                var store = _storeRepository.GetByPhoneAndPassword(Phone, Password);
                if (store == null)
                    ModelState.AddModelError(nameof(ErrorMessage), "Số điện thoại hoặc mật khẩu không đúng");
                else if (store.IsBanned)
                    ModelState.AddModelError(nameof(ErrorMessage), "Tài khoản này không được phép đăng nhập");
                else
                {
                    HttpContext.Session.SetObjectAsJson(Constants.SessionKey.CurrentUserKey, new CurrentUser
                    {
                        Id = store.Id,
                        Name = store.Name,
                        Role = Constants.Role.Store,
                    });
                    return RedirectToPage("/Store/Index");
                }
            }
            else if (customer.IsBanned)
                ModelState.AddModelError(nameof(ErrorMessage), "Tài khoản này không được phép đăng nhập");
            else
            {
                HttpContext.Session.SetObjectAsJson(Constants.SessionKey.CurrentUserKey, new CurrentUser
                {
                    Id = customer.Id,
                    Name = customer.FullName,
                    Role = Constants.Role.Customer,
                });
                return RedirectToPage("/Customer/Index");

            }
        }

        return Page();
    }

    public IActionResult OnPostLogout()
    {
        HttpContext.Session.Remove(Constants.SessionKey.CurrentUserKey);
        return RedirectToPage("/Login");
    }
}
