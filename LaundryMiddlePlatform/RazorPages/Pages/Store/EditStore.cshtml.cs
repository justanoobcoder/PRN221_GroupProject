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

namespace RazorPages.Pages.StoreNamespace
{
    public class EditStoreModel : PageModel
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ICustomerRepository _customerRepository;

        public EditStoreModel(IStoreRepository storeRepository, ICustomerRepository customerRepository)
        {
            _storeRepository = storeRepository;
            _customerRepository = customerRepository;
        }

        [BindProperty]
        public BusinessObjects.Store Store { get; set; } = default!;

        public IActionResult  OnGet(int? id)
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
                    Store = _storeRepository.GetById(currentUser.Id);
                    return Page();
                }
            }
            return RedirectToPage("/Login");
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            //validation

            //Update Store
            try
            {
               _storeRepository.Update(Store);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(Store.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //Return Page
            return RedirectToPage("/Store/Index");
        }

        private bool StoreExists(int id)
        {
          return (_storeRepository.GetById(id)!=null);
        }
    }
}
