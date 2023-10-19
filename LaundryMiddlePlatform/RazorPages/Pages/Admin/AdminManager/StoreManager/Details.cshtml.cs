using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories.Impl;

namespace RazorPages.Pages.Admin.AdminManager.StoreManager
{
    public class DetailsModel : PageModel
    {
        private StoreRepository _repository = new StoreRepository();


      public BusinessObjects.Store Store { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _repository.GetListStores() == null)
            {
                return NotFound();
            }

            var store = await _repository.GetStoreById(id);
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
