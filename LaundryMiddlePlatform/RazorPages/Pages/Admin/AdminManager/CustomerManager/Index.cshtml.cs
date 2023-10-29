using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories.Impl;
using CarRentingManagementLibrary.Pagging;

namespace RazorPages.Pages.Admin.AdminManager.CustomerManager
{
    public class IndexModel : PageModel
    {
        private CustomerRepository _repository = new CustomerRepository();
        private readonly IConfiguration Configuration;
        public IndexModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<BusinessObjects.Customer> Customers { get;set; } = default!;

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

            IQueryable<BusinessObjects.Customer> customerIQ = _repository.GetListCustomersIQ();

            if (!String.IsNullOrEmpty(searchString))
            {
                customerIQ = customerIQ.Where(s => s.FullName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    customerIQ = customerIQ.OrderByDescending(s => s.FullName);
                    break;
                case "Date":
                    customerIQ = customerIQ.OrderBy(s => s.FullName);
                    break;
                case "date_desc":
                    customerIQ = customerIQ.OrderByDescending(s => s.CreatedAt);
                    break;
                default:
                    customerIQ = customerIQ.OrderBy(s => s.FullName);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            Customers = PaginatedList<BusinessObjects.Customer>.Create(customerIQ, pageIndex ?? 1, pageSize);
        }
    }
}
