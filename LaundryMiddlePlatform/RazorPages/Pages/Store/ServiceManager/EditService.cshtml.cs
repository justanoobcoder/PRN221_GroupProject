using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;
using RazorPages.Models;
using Repositories.Impl;
using RazorPages.Utils;

namespace RazorPages.Pages.Store.ServiceManager
{
    public class EditServiceModel : PageModel
    {
        private readonly IServiceRepository _serviceRepository;

        public EditServiceModel(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [BindProperty]
        public Service Service { get; set; } = default!;
        public int IdCheck { get; set; } = -1;
        public IActionResult OnGet(int? id)
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
                    var service = _serviceRepository.GetById(id);
                    if (service != null)
                    {
                        Service = service;
                        IdCheck = (int)id;
                    }
                    else
                    {
                        return NotFound();
                    }
                    return Page();
                }
            }
            return RedirectToPage("/Login");
        }

        public  IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var service = _serviceRepository.GetByName(Service.Name);
            if (service != null)
            {
                var checkSameService = _serviceRepository.GetById(Service.Id);
                if (!checkSameService.Id.Equals(service.Id))
                {
                    ModelState.AddModelError("Service.Name", "Đã tồn tại với dịch vụ này!");
                    return Page();
                }
            }
            _serviceRepository.Update(Service);

            return RedirectToPage("/Store/DetailsStore");
        }

       
    }
}
