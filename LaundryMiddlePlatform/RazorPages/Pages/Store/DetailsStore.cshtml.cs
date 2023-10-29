using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;
using RazorPages.Models;
using RazorPages.Utils;
using Repositories.Impl;

namespace RazorPages.Pages.StoreNamespace
{
    [BindProperties]
    public class DetailsStoreModel : PageModel
    {
        private readonly IStoreRepository storeRepository;
        private readonly IServiceRepository serviceRepository;

        public DetailsStoreModel(IStoreRepository _storeRepository, IServiceRepository _serviceRepository)
        {
            storeRepository = _storeRepository;
            serviceRepository = _serviceRepository;
        }

        public BusinessObjects.Store Store { get; set; } = default!;
        public IList<Service> Services { get; set; } = default!;


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
                    var storeDetails = storeRepository.GetById(currentUser.Id);
                    var serviceList = serviceRepository.GetAllByStoreId(currentUser.Id);

                    if (storeDetails != null)
                    {
                        Store = storeDetails;

                    }
                    if (serviceList != null)
                    {
                        Services = (IList<Service>)serviceList;
                    }

                    return Page();
                }
            }
            return RedirectToPage("/Login");
        }

    }
}
