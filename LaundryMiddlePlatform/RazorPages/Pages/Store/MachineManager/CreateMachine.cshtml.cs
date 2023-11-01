using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using RazorPages.Models;
using RazorPages.Utils;
using Repositories.Impl;
using System.Reflection.PortableExecutable;
using Repositories;

namespace RazorPages.Pages.Store.MachineManager
{

    [BindProperties]
    public class CreateMachineModel : PageModel
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IStoreRepository _storeRepository;
        public CreateMachineModel(IMachineRepository machineRepository, IStoreRepository storeRepository)
        {
            _machineRepository = machineRepository;
            _storeRepository = storeRepository;
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
                        Machine.StoreId = storeId; 
                    }
                    return Page();
                }
            }
            return RedirectToPage("/Login");
        }


        public BusinessObjects.Machine Machine { get; set; } = new BusinessObjects.Machine()!;
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var machine = _machineRepository.GetByName(Machine.Name);
            if (machine != null)
            {
                ModelState.AddModelError("Machine.Name", "Đã tồn tại với tên máy giặt này!");
                return Page();
            }

            _machineRepository.Create(Machine);

            return RedirectToPage("/Store/DetailsStore");
        }
    }
}
