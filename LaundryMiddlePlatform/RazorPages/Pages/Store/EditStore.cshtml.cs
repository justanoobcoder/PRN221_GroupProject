using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;
using RazorPages.Models;
using RazorPages.Utils;

namespace RazorPages.Pages.StoreNamespace
{
    [BindProperties]
    public class EditStoreModel : PageModel
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ICustomerRepository _customerRepository;

        public EditStoreModel(IStoreRepository storeRepository, ICustomerRepository customerRepository)
        {
            _storeRepository = storeRepository;
            _customerRepository = customerRepository;
        }

        public UpdateStore UpdateStore { get; set; } = new UpdateStore();

        public string? ErrorMessage { get; set; }

        public IActionResult OnGet(int? id)
        {
            var currentUser = HttpContext.Session.GetObjectFromJson<CurrentUser>(Constants.SessionKey.CurrentUserKey);
            if (currentUser != null)
            {
                if (currentUser.Role == Constants.Role.Admin)
                    return RedirectToPage("/Admin/Index");
                else if (currentUser.Role == Constants.Role.Customer)
                    return RedirectToPage("/Customer/Index");
                else if (currentUser.Role == Constants.Role.Store)
                {
                    var Store = _storeRepository.GetById(currentUser.Id);
                    if (Store != null)
                    {
                        UpdateStore.Name = Store.Name;
                        UpdateStore.Email = Store.Email;
                        UpdateStore.Address = Store.Address;
                        UpdateStore.Description = Store.Description;
                        UpdateStore.Phone = Store.Phone;
                        UpdateStore.AvatarUrl = Store.AvatarUrl;
                        UpdateStore.CoverUrl = Store.CoverUrl;
                        UpdateStore.FacebookUrl = Store.FacebookUrl;
                        UpdateStore.OpenTime = Store.OpenTime;
                        UpdateStore.CloseTime = Store.CloseTime;
                    }
                    return Page();
                }
            }
            return RedirectToPage("/Login");
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
         
            //validation

            if (!ModelState.IsValid)
            {
                return Page();
            }
            var currentUser = HttpContext.Session.GetObjectFromJson<CurrentUser>(Constants.SessionKey.CurrentUserKey);
            var customer = _customerRepository.GetByPhone(UpdateStore.Phone);
            if (customer != null)
            {
                ModelState.AddModelError("UpdateStore.Phone", "Đã tồn tại với số điện thoại này!");
                return Page();
            }
            var customerEmail = _customerRepository.GetByEmail(UpdateStore.Email);
            if (customerEmail != null)
            {
                ModelState.AddModelError("UpdateStore.Email", "Đã tồn tại với email này!");
                return Page();
            }
            var storePhone = _storeRepository.GetByPhone(UpdateStore.Phone);
            if (storePhone != null)
            {
                var checkSameStore = _storeRepository.GetById(currentUser!.Id);
                if (!checkSameStore.Id.Equals(storePhone.Id))
                {
                    ModelState.AddModelError("UpdateStore.Phone", "Đã tồn tại với số điện thoại này!");
                    return Page();
                }
            }
            var storeEmail = _storeRepository.GetByEmail(UpdateStore.Email);
            if (storeEmail != null)
            {
                var checkSameStore = _storeRepository.GetById(currentUser.Id);
                if (!checkSameStore.Id.Equals(storeEmail.Id))
                {
                    ModelState.AddModelError("UpdateStore.Email", "Đã tồn tại với email này!");
                    return Page();
                }
            }
            if (TimeSpan.Compare(UpdateStore.CloseTime, UpdateStore.OpenTime) <= 0)
            {
                ModelState.AddModelError(nameof(ErrorMessage), "Giờ đóng cửa phải sau giờ mở cửa  ");
                return Page();
            }

            //Update Store
            var Store = _storeRepository.GetById(currentUser!.Id);
            Store.Name = UpdateStore.Name;
            Store.Email = UpdateStore.Email;
            Store.Address = UpdateStore.Address;
            Store.Description = UpdateStore.Description;
            Store.Phone = UpdateStore.Phone;
            Store.AvatarUrl = UpdateStore.AvatarUrl;
            Store.CoverUrl = UpdateStore.CoverUrl;
            Store.FacebookUrl = UpdateStore.FacebookUrl;
            Store.OpenTime = UpdateStore.OpenTime;
            Store.CloseTime = UpdateStore.CloseTime;
            _storeRepository.Update(Store);

            //Return Page
            return RedirectToPage("/Store/Index");
        }

    }
}
