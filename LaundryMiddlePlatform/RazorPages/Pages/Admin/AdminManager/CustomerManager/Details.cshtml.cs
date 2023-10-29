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
    public class DetailsModel : PageModel
    {
        private CustomerRepository _repository = new CustomerRepository();


      public BusinessObjects.Customer Customer { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || _repository.GetAll() == null)
            {
                return NotFound();
            }

            var customer =  _repository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            else 
            {
                Customer = customer;
            }
            return Page();
        }
    }
}
