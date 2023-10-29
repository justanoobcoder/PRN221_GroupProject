using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories.Impl;
using System.Configuration;
using CarRentingManagementLibrary.Pagging;

namespace RazorPages.Pages.Admin.AdminManager.StoreManager
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private StoreRepository _repository = new StoreRepository();
        private readonly IConfiguration Configuration;
        public IndexModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter1 { get; set; }
        public string CurrentFilter2 { get; set; }
        public string CurrentSort { get; set; }
        public string? ErrorMessage { get; set; } = default!;


        public PaginatedList<BusinessObjects.Store> Stores { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder, string currentFilter1, string currentFilter2, string dateStart, string dateEnd, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (dateStart != null || dateEnd != null)
            {
                pageIndex = 1;
            }
            else
            {
                dateStart = CurrentFilter1;
                dateEnd = CurrentFilter2;
            }

            if (currentFilter1 != null || currentFilter2 != null)
            {
                dateStart = currentFilter1;
                dateEnd = CurrentFilter2;
            }
            CurrentFilter1 = dateStart;
            CurrentFilter2 = dateEnd;

            IQueryable<BusinessObjects.Store> storeIQ = await _repository.GetListStoresIQ();

            if (!String.IsNullOrEmpty(dateStart) || !String.IsNullOrEmpty(dateEnd))
            {
                string errorMessage = "- Ngày bắt đầu phải bé hơn hoặc bằng ngày kết thúc.\n - Ngày bắt đầu và ngày kết thúc phải bé hơn hoặc bằng thời gian hiện tại.";
                ErrorMessage = errorMessage;
                if (String.IsNullOrEmpty(dateStart) && !String.IsNullOrEmpty(dateEnd)) { if (DateTime.Parse(dateEnd.Trim()) > DateTime.UtcNow) ModelState.AddModelError(nameof(ErrorMessage), errorMessage); }
                else if (!String.IsNullOrEmpty(dateStart) && String.IsNullOrEmpty(dateEnd)) { if (DateTime.Parse(dateStart.Trim()) > DateTime.UtcNow) ModelState.AddModelError(nameof(ErrorMessage), errorMessage); }
                else if (DateTime.Parse(dateEnd.Trim()) < DateTime.Parse(dateStart.Trim())
                || ((DateTime.Parse(dateEnd.Trim()) > DateTime.UtcNow || DateTime.Parse(dateStart.Trim()) > DateTime.UtcNow)))
                {
                    ModelState.AddModelError(nameof(ErrorMessage), errorMessage);
                }
                if (String.IsNullOrEmpty(dateStart)) storeIQ = storeIQ.Where(s => s.CreatedAt <= DateTime.Parse(dateEnd.Trim()));
                else if (String.IsNullOrEmpty(dateEnd)) storeIQ = storeIQ.Where(s => s.CreatedAt >= DateTime.Parse(dateStart.Trim()));
                else storeIQ = storeIQ.Where(s => s.CreatedAt >= DateTime.Parse(dateStart.Trim()) && s.CreatedAt <= DateTime.Parse(dateEnd.Trim()));
            }
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

            var pageSize = Configuration.GetValue("PageSize", 4);
            Stores = PaginatedList<BusinessObjects.Store>.Create(storeIQ, pageIndex ?? 1, pageSize);
        }
    }
}
