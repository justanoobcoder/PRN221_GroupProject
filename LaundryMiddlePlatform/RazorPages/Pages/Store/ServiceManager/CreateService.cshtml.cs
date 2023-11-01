using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Repositories;
using RazorPages.Models;
using RazorPages.Utils;
using Repositories.Impl;

namespace RazorPages.Pages.Store.ServiceManager
{
    public class CreateServiceModel : PageModel
    {
        private readonly IServiceRepository _serviceRepository;

        public CreateServiceModel(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public IActionResult OnGet()
        {
            var currentUser = HttpContext.Session.GetObjectFromJson<CurrentUser>(Constants.SessionKey.CurrentUserKey);
            if (currentUser != null)
            {
                if (currentUser.Role == Constants.Role.Admin)
                    return RedirectToPage("/Admin/Index");
                else if (currentUser.Role == Constants.Role.Customer)
                    return RedirectToPage("/Customer/Index");
                else if (currentUser.Role == Constants.Role.Store)
                {
                    int storeId = (int)currentUser.Id;
                    if (storeId != null)
                    {
                        Service.StoreId = storeId;
                    }
                    return Page();
                }
            }
            return RedirectToPage("/Login");
        }

        [BindProperty]
        public Service Service { get; set; } = new Service();


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var service = _serviceRepository.GetByName(Service.Name);
            if (service != null)
            {
                ModelState.AddModelError("Service.Name", "Đã tồn tại với tên dịch vụ này!");
                return Page();
            }
            _serviceRepository.Create(Service);
            return RedirectToPage("/Store/DetailsStore");
        }
    }
}
