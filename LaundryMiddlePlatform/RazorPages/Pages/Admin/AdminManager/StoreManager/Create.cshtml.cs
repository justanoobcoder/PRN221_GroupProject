using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Repositories.Impl;

namespace RazorPages.Pages.Admin.AdminManager.StoreManager
{
    public class CreateModel : PageModel
    {
        private StoreRepository _repository = new StoreRepository();



        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BusinessObjects.Store Store { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _repository.GetListStores() == null || Store == null)
            {
                return Page();
            }

            await _repository.AddStore(Store);

            return RedirectToPage("./Index");
        }
    }
}
