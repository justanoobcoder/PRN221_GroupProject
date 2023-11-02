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
        private readonly IMachineRepository machineRepository;
        public DetailsStoreModel(IStoreRepository _storeRepository, IServiceRepository _serviceRepository, IMachineRepository _machineRepository)
        {
            storeRepository = _storeRepository;
            serviceRepository = _serviceRepository;
            machineRepository = _machineRepository;
        }
        public BusinessObjects.Store Store { get; set; } = default!;
        public IList<Service> Services { get; set; } = default!;
        public IList<Machine> Machines { get; set; }
        public Machine Machine { get; set; }
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
                    var machineList = machineRepository.GetAllByStoreId(currentUser.Id);

                    if (storeDetails != null)
                    {
                        Store = storeDetails;

                    }
                    if (serviceList != null)
                    {
                        Services = (IList<Service>)serviceList;

                    }
                    if (machineList != null)
                    {
                        Machines = (IList<Machine>)machineList;
                    }
                    return Page();
                }
            }
            return RedirectToPage("/Login");
        }


        public IActionResult OnPostCloseAsync()
        {
            var currentUser = HttpContext.Session.GetObjectFromJson<CurrentUser>(Constants.SessionKey.CurrentUserKey);
            var store = storeRepository.GetById(currentUser.Id);
            if (store != null)
            {
                Store = store;
                Store.IsOpening = false;
            };
            storeRepository.Update(Store);
            return RedirectToPage("/Store/DetailsStore");
        }

        public IActionResult OnPostOpenAsync()
        {
            var currentUser = HttpContext.Session.GetObjectFromJson<CurrentUser>(Constants.SessionKey.CurrentUserKey);
            var store = storeRepository.GetById(currentUser.Id);
            if (store != null)
            {
                Store = store;
                Store.IsOpening = true;
            };
            storeRepository.Update(Store);
            return RedirectToPage("/Store/DetailsStore");
        }

        public IActionResult OnPostStopAsync(int id)
        {
            var machine = machineRepository.GetById(id);
            if (machine == null)
            {
                return NotFound();
            }
            machine.IsAvailable = false;
            machineRepository.Update(machine);  
            return RedirectToPage("/Store/DetailsStore");
        }

        public IActionResult OnPostRunAsync(int id)
        {
            var machine = machineRepository.GetById(id);
            if (machine == null)
            {
                return NotFound();
            }
            machine.IsAvailable = true;
            machineRepository.Update(machine);
            return RedirectToPage("/Store/DetailsStore");
        }
    }
}
