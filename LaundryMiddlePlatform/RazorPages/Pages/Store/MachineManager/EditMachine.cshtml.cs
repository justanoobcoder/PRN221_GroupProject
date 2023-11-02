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
using RazorPages.Utils;
using Repositories.Impl;

namespace RazorPages.Pages.Store.MachineManager
{
    [BindProperties]
    public class EditMachineModel : PageModel
    {
        private readonly IMachineRepository _machineRepository;

        public EditMachineModel(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository;
        }


        public Machine Machine { get; set; } = default!;

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = HttpContext.Session.GetObjectFromJson<CurrentUser>(Constants.SessionKey.CurrentUserKey);
            if (currentUser != null)
            {
                if (currentUser.Role == Constants.Role.Admin)
                    return RedirectToPage("/Admin/Index");
                else if (currentUser.Role == Constants.Role.Customer)
                    return RedirectToPage("/Customer/Index");
                else if (currentUser.Role == Constants.Role.Store)
                {
                    var machine = _machineRepository.GetById(id);
                    if (machine != null)
                    {
                        Machine = machine;
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var machine = _machineRepository.GetByName(Machine.Name);
            if (machine != null)
            {
                var checkSameMachine = _machineRepository.GetById(Machine.Id);
                if (!checkSameMachine.Id.Equals(machine.Id))
                {
                    ModelState.AddModelError("Service.Name", "Đã tồn tại với tên máy giặt này!");
                    return Page();
                }
            }
            _machineRepository.Update(Machine);

            return RedirectToPage("/Store/DetailsStore");
        }
        public IActionResult OnPostCloseAsync()
        {
            Machine.IsAvailable = false;
            _machineRepository.Update(Machine);
            return RedirectToPage("/Store/MachineManager/EditMachine");
        }

        public IActionResult OnPostOpenAsync()
        {
            Machine.IsAvailable= true;
            _machineRepository.Update(Machine);
            return RedirectToPage("/Store/MachineManager/EditMachine");
        }

    }
}
