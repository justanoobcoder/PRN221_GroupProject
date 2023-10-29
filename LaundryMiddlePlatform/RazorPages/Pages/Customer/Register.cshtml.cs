using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using NToastNotify;
using Repositories;

namespace RazorPages.Pages.CustomerNamespace;

public class RegisterModel : PageModel
{
    private readonly ICustomerRepository _iCustomerRepository;
    private readonly IToastNotification _toastNotification;

    public RegisterModel(ICustomerRepository customerRepository, IToastNotification toastNotification)
    {
        _iCustomerRepository = customerRepository;
        _toastNotification = toastNotification;
    }
    [BindProperty]
    public string ConfirmPassword { get; set; } = default!;
    [BindProperty]
    public BusinessObjects.Customer Customer { get; set; } = default!;


    public IActionResult OnGet()
    {

        return Page();
    }
    public IActionResult OnPost()
    {
        try
        {
            if (_iCustomerRepository.GetByPhone(Customer.Phone) != null)
            {
                ModelState.AddModelError("Customer.Phone", "Trùng số điện thoại");
            }
            if (_iCustomerRepository.GetByEmail(Customer.Email) != null)
            {
                ModelState.AddModelError("Customer.Email", "Trùng email");
            }
            if (!Customer.Password.Equals(ConfirmPassword))
            {
                ModelState.AddModelError("ConfirmPassword", "Mật khẩu không trùng khớp - Vui lòng nhập lại");
            }
            if (Customer.AvatarUrl.IsNullOrEmpty())
            {
                Customer.AvatarUrl = string.Empty;
            }
            if (ModelState.IsValid != true)
            {
                _toastNotification.AddErrorToastMessage("Đăng ký tài khoản thất bại");
                return Page();
            }
            var check = _iCustomerRepository.Create(Customer);
            if (check != null)
            {
                _toastNotification.AddSuccessToastMessage("Đăng ký tài khoản thành công");
                Customer = default;
                ModelState.Clear();
            }
            return Page();

        }
        catch (Exception ex)
        {

            _toastNotification.AddErrorToastMessage("Đăng ký tài khoản thất bại");
        }
        return Page();
    }

}
