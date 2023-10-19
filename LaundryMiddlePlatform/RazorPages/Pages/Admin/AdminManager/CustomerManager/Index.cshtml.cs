using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories.Impl;

namespace RazorPages.Pages.Admin.AdminManager.CustomerManager
{
    public class IndexModel : PageModel
    {
        private CustomerRepository _repository = new CustomerRepository();


        public IList<BusinessObjects.Customer> Customer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_repository.GetAll() != null)
            {
                Customer = await _repository.GetAll().ToListAsync();
            }
        }
    }
}
