using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Admin;
using RazorPages.Models;
using RazorPages.Utils;
using Repositories;
using System.ComponentModel.DataAnnotations;

namespace RazorPages.Pages.StoreNamespace;

[BindProperties]
public class RegisterModel : PageModel
{
    private readonly IStoreRepository _storeRepository;
    private readonly ICustomerRepository _customerRepository;

    public RegisterModel(IStoreRepository storeRepository, ICustomerRepository customerRepository)
    {
        _storeRepository = storeRepository;
        _customerRepository = customerRepository;
    }

    //make
    public BusinessObjects.Store Store { get; set; } = default!;
    [Display(Name = "Nhập lại Mật khẩu")]
    [Required(ErrorMessage = "Nhập lại Mật khẩu không được để trống")]
    [StringLength(30, MinimumLength = 6, ErrorMessage = "Nhập lại Mật khẩu phải từ 6 kí tự và không được quá 30 ký tự")]
    public string ConfirmPassword { get; set; }
    public string? ErrorMessage { get; set; } = default!;
    //

    public void OnGet()
    {

    }
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Validate the form data
        var customer = _customerRepository.GetByPhone(Store.Phone);
        if (customer != null)
        {
            ModelState.AddModelError("Store.Phone", "Đã tồn tại khách hàng với số điện thoại này!");
            return Page();
        }
        var customerEmail = _customerRepository.GetByEmail(Store.Email);
        if (customerEmail != null)
        {
            ModelState.AddModelError("Store.Email", "Đã tồn tại khách hàng với email này!");
            return Page();
        }
        var storePhone = _storeRepository.GetByPhone(Store.Phone);
        if (storePhone != null)
        {
            ModelState.AddModelError("Store.Phone", "Đã tồn tại cửa hàng với số điện thoại này!");
            return Page();
        }
        var storeEmail = _storeRepository.GetByEmail(Store.Email);
        if (storeEmail != null)
        {
            ModelState.AddModelError("Store.Email", "Đã tồn tại cửa hàng với email này!");
            return Page();
        }
        if (Store.Password != ConfirmPassword)
        {
            ModelState.AddModelError("ConfirmPassword", "Hãy nhập đúng Confirm Password ");
            return Page();
        }
        if (TimeSpan.Compare(Store.CloseTime, Store.OpenTime) <= 0)
        {
            ModelState.AddModelError(nameof(ErrorMessage), "Giờ đóng cửa phải sau giờ mở cửa  ");
            return Page();
        }
      

        // Register the user
        _storeRepository.Create(Store);
        // Redirect to the login page
        return RedirectToPage("/Login");

    }
}


