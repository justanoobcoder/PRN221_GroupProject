using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;
using NToastNotify;
using RazorPages.Models;
using RazorPages.Utils;

namespace RazorPages.Pages.CustomerNamespace
{
    public class DetailsModel : PageModel
    {
        private readonly ICustomerRepository _iCustomerRepository;
        private readonly IToastNotification _iToastNotification;

        public DetailsModel(ICustomerRepository iCustomerRepository, IToastNotification toastNotification)
        {
            _iCustomerRepository = iCustomerRepository;
            _iToastNotification = toastNotification;
        }

      public BusinessObjects.Customer Customer { get; set; } = default!; 

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
                            return NotFound();
                        }
                        else
                        {
                            Customer = _iCustomerRepository.GetById((int)id);
                            if (Customer == null)
                            {
                                return Page();
                            }
                        }
                    }
                }
                else
                {
                    return RedirectToPage("/Login");
                }
                
            }catch (Exception ex)
            {
                _iToastNotification.AddErrorToastMessage("Load dữ liệu không thành công");
            }
            return Page();
        }
    }
}
