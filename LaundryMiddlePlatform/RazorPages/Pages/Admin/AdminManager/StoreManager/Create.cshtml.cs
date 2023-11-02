using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Repositories.Impl;

namespace RazorPages.Pages.Admin.AdminManager.StoreManager
{
    public class CreateModel : PageModel
    {
        private readonly StoreRepository _repository = new StoreRepository();
        [TempData]
        public string? ErrorMessage { get; set; } = default!;
        [TempData]
        public string? ErrorMessageUrl { get; set; } = default!;
        [TempData]
        public string SuccessMessageCreate { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BusinessObjects.Store Store { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _repository.GetListStores == null || Store == null)
            {
                return Page();
            }

            if (_repository.GetStoreFacebookUrl(Store.FacebookUrl.Trim()) != null || _repository.GetByEmail(Store.Email.Trim()) != null)
            {
                ErrorMessage = "";
                if (_repository.GetByEmail(Store.Email.Trim()) != null)
                {
                    ModelState.AddModelError(nameof(ErrorMessage), "Email đã tồn tại, vui lòng nhập email khác!");
                }
                ErrorMessageUrl = "";
                var a = _repository.GetStoreFacebookUrl(Store.FacebookUrl.Trim());
                if (_repository.GetStoreFacebookUrl(Store.FacebookUrl.Trim()) != null)
                {
                    ModelState.AddModelError(nameof(ErrorMessageUrl), "Facebook url đã tồn tại, vui lòng nhập url khác!");
                }
                return Page();
            }

            await _repository.AddStore(Store);
            SuccessMessageCreate = "Tạo cửa hàng thành công!";
            return RedirectToPage("./Index");
        }
    }
}
