using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories.Impl;
using Microsoft.Data.SqlClient;
using CarRentingManagementLibrary.Pagging;
using System.Configuration;

namespace RazorPages.Pages.Store
{
    public class ViewDetailModel : PageModel
    {
        private StoreRepository _repository = new StoreRepository();
        private ServiceRepository _serviceRepository = new ServiceRepository();
        private readonly IConfiguration Configuration;
        public ViewDetailModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public BusinessObjects.Store Store { get; set; } = default!;
        public bool IsOpening { get; set; }
        public List<BusinessObjects.Service> Services { get; set; } = default!;
        public PaginatedList<BusinessObjects.Store> Stores { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            searchString = "";
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = CurrentFilter;
            }

            if (currentFilter != null)
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;
            if (id == null || _repository.GetListStores() == null)
            {
                return NotFound();
            }

            var store = await _repository.GetStoreById(id);
            var services = _serviceRepository.GetListServices(id);
            if (store == null)
            {
                return NotFound();
            }
            else
            {
                Services = services;
                Store = store;
                IsOpening = store.IsOpening;
            }
            IQueryable<BusinessObjects.Store> storeIQ = await _repository.GetListStoresIQ();
            storeIQ = storeIQ.Where(s => s.Name.Contains(searchString.Trim()));
            switch (sortOrder)
            {
                case "name_desc":
                    storeIQ = storeIQ.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    storeIQ = storeIQ.OrderBy(s => s.Name);
                    break;
                case "date_desc":
                    storeIQ = storeIQ.OrderByDescending(s => s.CreatedAt);
                    break;
                default:
                    storeIQ = storeIQ.OrderBy(s => s.Name);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 3);
            Stores = PaginatedList<BusinessObjects.Store>.Create(storeIQ, pageIndex ?? 1, pageSize);
            return Page();
            
        }
    }
}
