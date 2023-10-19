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
    public class IndexModel : PageModel
    {
        private StoreRepository _repository = new StoreRepository();

        public IndexModel()
        {
        }

        public IList<BusinessObjects.Store> Store { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_repository.GetListStores() != null)
            {
                Store = await _repository.GetListStores();
            }
        }
    }
}
