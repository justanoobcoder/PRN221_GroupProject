using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorPages.Pages.StoreNamespace
{
    public class DetailsServiceModel : PageModel
    {
        private readonly IServiceRepository _iServiceRepository;
        private readonly IStoreRepository _iStoreRepository;


        public DetailsServiceModel(IServiceRepository iServiceRepository, IStoreRepository iStoreRepository)
        {
            _iServiceRepository = iServiceRepository;
            _iStoreRepository = iStoreRepository;
        }

        public Service Service { get; set; } = default!;
        public List<SelectListItem> StoresSelectList { get; set; } = default!;
        public BusinessObjects.Store Store { get; set; } = default!;


        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Service = _iServiceRepository.GetById(id);
            //var storesData = _iStoreRepository.GetStoresAvailable();
            var storeData = _iStoreRepository.GetStores();
            Store = storeData.Where(s => s.Services.Where(s => s.Id == id).FirstOrDefault() != null).FirstOrDefault();

            return Page();
        }

    }
}
