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
using Microsoft.AspNetCore.Html;

namespace RazorPages.Pages.Admin.AdminManager.CustomerManager
{
    [BindProperties]
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
        public string CurrentFilter1 { get; set; }
        public string CurrentFilter2 { get; set; }
        public string CurrentSort { get; set; }
        public string? ErrorMessage { get; set; } = default!;

        public PaginatedList<BusinessObjects.Customer> Customers { get; set; } = default!;

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

            IQueryable<BusinessObjects.Customer> customerIQ = _repository.GetListCustomersIQ();

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
                if (String.IsNullOrEmpty(dateStart)) customerIQ = customerIQ.Where(s => s.CreatedAt <= DateTime.Parse(dateEnd.Trim()));
                else if (String.IsNullOrEmpty(dateEnd)) customerIQ = customerIQ.Where(s => s.CreatedAt >= DateTime.Parse(dateStart.Trim()));
                else customerIQ = customerIQ.Where(s => s.CreatedAt >= DateTime.Parse(dateStart.Trim()) && s.CreatedAt <= DateTime.Parse(dateEnd.Trim()));
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

            var pageSize = Configuration.GetValue("PageSize", 3);
            Customers = PaginatedList<BusinessObjects.Customer>.Create(customerIQ, pageIndex ?? 1, pageSize);
        }
    }
}
