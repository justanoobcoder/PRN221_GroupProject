﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories.Impl;

namespace RazorPages.Pages.Admin.AdminManager.CustomerManager
{
    public class EditModel : PageModel
    {
        private CustomerRepository _repository = new CustomerRepository();


        [BindProperty]
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
            Customer = customer;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id, string ban)
        {


            try
            {
                var customer = _repository.GetById(id);
                Customer = customer;
                if (ban.Trim().Equals("Ban"))
                    Customer.IsBanned = true;
                else
                    Customer.IsBanned = false;
                _repository.Update(Customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CustomerExists(int id)
        {
          return (_repository.GetById(id) != null);
        }
    }
}