using CarRentingManagementLibrary.Pagging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Impl;

namespace RazorPages.Pages.StoreNamespace
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
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public string? ErrorMessage { get; set; } = default!;
        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string SuccessMessageCreate { get; set; }



        public PaginatedList<BusinessObjects.Store> Stores { get; set; } = default!;

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
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

            IQueryable<BusinessObjects.Store> storeIQ = await _repository.GetListStoresIQ();

            if (!String.IsNullOrEmpty(searchString))
            {
                storeIQ = storeIQ.Where(s => s.Name.Contains(searchString.Trim()));
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

            var pageSize = Configuration.GetValue("PageSize", 3);
            Stores = PaginatedList<BusinessObjects.Store>.Create(storeIQ, pageIndex ?? 1, pageSize);
        }
    }
}
