using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using NToastNotify;
using Repositories;
using Microsoft.IdentityModel.Tokens;
using RazorPages.Models;
using RazorPages.Utils;

namespace RazorPages.Pages.Customer
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public BusinessObjects.Customer Customer { get; set; } = default!;
        private readonly ICustomerRepository _iCustomerRepository;
        private readonly IToastNotification _iToastNotification;
        private static string checkEmail;
        private static string checkPhone;
        private static string password;


        public UpdateModel(ICustomerRepository iCustomerRepository, IToastNotification toastNotification)
        {
            _iCustomerRepository = iCustomerRepository;
            _iToastNotification = toastNotification;
        }
        public IActionResult OnGet(int? id)
        {
            try
            {
                var currentUser = HttpContext.Session.GetObjectFromJson<CurrentUser>(Constants.SessionKey.CurrentUserKey);
                if (currentUser != null)
                {
                    if (currentUser.Role == Constants.Role.Customer)
                    {
                        if (id == null)
                        {

                            _iToastNotification.AddErrorToastMessage("Id bị null");
                            return NotFound();
                        }
                        else
                        {
                            Customer = _iCustomerRepository.GetById((int)id);
                            if (Customer == null)
                            {
                                _iToastNotification.AddErrorToastMessage("Customer bị null");
                                return Page();
                            }
                            checkEmail = Customer.Email.ToString();
                            checkPhone = Customer.Phone.ToString();
                            password = Customer.Password;
                        }
                    }
                }
                else
                {
                    return RedirectToPage("/Login");
                }
            }
            catch (Exception ex)
            {
                _iToastNotification.AddErrorToastMessage("Load dữ liệu không thành công");
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            try
            {
                if (_iCustomerRepository.GetByPhone(Customer.Phone) != null && !Customer.Phone.Equals(checkPhone))
                {
                    ModelState.AddModelError("Customer.Phone", "Trùng số điện thoại");
                }

                if (_iCustomerRepository.GetByEmail(Customer.Email) != null && !Customer.Email.Equals(checkEmail))
                {
                    ModelState.AddModelError("Customer.Email", "Trùng email");
                }
                if (Customer.AvatarUrl.IsNullOrEmpty())
                {
                    Customer.AvatarUrl = string.Empty;
                }
                Customer.Password = password;

                _iCustomerRepository.Update(Customer);
                _iToastNotification.AddSuccessToastMessage("Cập nhật thông tin thành công");



            }

            catch (Exception ex)
            {


                _iToastNotification.AddErrorToastMessage("Cập nhật thông tin thất bại");
                _iToastNotification.AddErrorToastMessage(ex.ToString());
            }
            return Page();
        }



    }
}
