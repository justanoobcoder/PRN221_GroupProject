using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Repositories.Impl;

namespace RazorPages.Pages.Admin.AdminManager.CustomerManager
{
    public class CreateModel : PageModel
    {
        private CustomerRepository _repository = new CustomerRepository();


        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BusinessObjects.Customer Customer { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _repository.GetAll() == null || Customer == null)
            {
                return Page();
            }

            _repository.Create(Customer);

            return RedirectToPage("./Index");
        }
    }
}
