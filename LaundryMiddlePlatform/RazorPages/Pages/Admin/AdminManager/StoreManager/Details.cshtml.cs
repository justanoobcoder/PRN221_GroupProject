using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;

namespace RazorPages.Pages.Admin.AdminManager.StoreManager
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObjects.LaundryMiddlePlatformDbContext _context;

        public DetailsModel(BusinessObjects.LaundryMiddlePlatformDbContext context)
        {
            _context = context;
        }

      public BusinessObjects.Store Store { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores.FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }
            else 
            {
                Store = store;
            }
            return Page();
        }
    }
}
