using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;

namespace RazorPages.Pages.Store
{
    public class CreateMachineModel : PageModel
    {
        private readonly BusinessObjects.LaundryMiddlePlatformDbContext _context;

        public CreateMachineModel(BusinessObjects.LaundryMiddlePlatformDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Machine Machine { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Machines == null || Machine == null)
            {
                return Page();
            }

            _context.Machines.Add(Machine);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
