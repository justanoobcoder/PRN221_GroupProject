using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories.Impl;

namespace RazorPages.Pages.Admin.AdminManager.StoreManager
{
    public class EditModel : PageModel
    {
        private StoreRepository _repository = new StoreRepository();



        [BindProperty]
        public BusinessObjects.Store Store { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _repository.GetListStores() == null)
            {
                return NotFound();
            }

            var store =  await _repository.GetStoreById(id);
            if (store == null)
            {
                return NotFound();
            }
            Store = store;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateStore(Store);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await StoreExists(Store.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> StoreExists(int id)
        {
          return await _repository.CheckIfStoreExist(id);
        }
    }
}
