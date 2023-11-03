using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarRentingManagementLibrary.Pagging;

namespace RazorPages.Pages.StoreNamespace
{
    public class ServiceModel : PageModel
    {
        private readonly IServiceRepository _iServiceRepository;
        private readonly IConfiguration _configuration;
        //private readonly IStoreRepository _iStoreRepository;
        public ServiceModel(IServiceRepository iServiceRepository, IConfiguration configuration, IStoreRepository iStoreRepository)
        {
            _iServiceRepository = iServiceRepository;
            /*_iStoreRepository = iStoreRepository;*/
            _configuration = configuration;
        }

        public IQueryable<Service> IService { get; set; } = default!;
        public PaginatedList<Service> Service { get; set; } = default!;


        public string PriceSort { get; set; } = "Price";
        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public List<string> SelectedMembers { get; set; }
        public List<string> listSort { get; set; } = default!;
        /*public List<SelectListItem> StoresSelectList { get; set; } = default!;*/

        public IActionResult OnGet(string sortService
            , string currentFilter, int? pageIndex, string SearchString)
        {
            /*
            CurrentSort = sortService;
            if (String.IsNullOrEmpty(CurrentSort))
            {
                NameSort = "Name";
            }
            else if (CurrentSort.Equals("Price"))
            {
                PriceSort = "Price_desc";
            }
            else if (CurrentSort.Equals("Price_desc"))
            {
                PriceSort = "Price";
            }


            if (SearchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            IService = _iServiceRepository.GetAll();
            if (!string.IsNullOrEmpty(SearchString))
            {

                IService = IService.Where(s => s.Name.Contains(SearchString));
            }
            switch (sortService)
            {
                case "Name":
                    IService = IService.OrderBy(s => s.Name);
                    break;
                case "Price_desc":
                    IService = IService.OrderByDescending(s => s.PricePerKg);
                    break;
                case "Price":
                    IService = IService.OrderBy(s => s.PricePerKg);
                    break;
                default:
                    IService = IService.OrderBy(s => s.Name);
                    break;
            }
            var pageSize = _configuration.GetValue("PageSize", 4);

            Service = PaginatedList<Service>.Create(IService.AsNoTracking(), pageIndex ?? 1, pageSize);
            */
            GetListOrderBy(sortService, currentFilter, pageIndex, SearchString);
            /*GetStoresSelecteList();*/
            return Page();
        }

        private PaginatedList<Service> GetListOrderBy(string sortService
            , string currentFilter, int? pageIndex, string SearchString)
        {
            CurrentSort = sortService;
            if (string.IsNullOrEmpty(CurrentSort))
            {
                NameSort = "Name";
            }
            else if (CurrentSort.Equals("Price"))
            {
                PriceSort = "Price_desc";
            }
            else if (CurrentSort.Equals("Price_desc"))
            {
                PriceSort = "Price";
            }


            if (SearchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            IService = _iServiceRepository.GetAll();
            if (!string.IsNullOrEmpty(SearchString))
            {

                IService = IService.Where(s => s.Name.Contains(SearchString));
            }
            switch (sortService)
            {
                case "Name":
                    IService = IService.OrderBy(s => s.Name);
                    break;
                case "Price_desc":
                    IService = IService.OrderByDescending(s => s.PricePerKg);
                    break;
                case "Price":
                    IService = IService.OrderBy(s => s.PricePerKg);
                    break;
                default:
                    IService = IService.OrderBy(s => s.Name);
                    break;
            }
            var pageSize = _configuration.GetValue("PageSize", 4);

            return Service = PaginatedList<Service>.Create(IService.AsNoTracking(), pageIndex ?? 1, pageSize);

        }

        /* void GetStoresSelecteList()
        {
            var storesData = _iStoreRepository.GetStoresAvailable();
            StoresSelectList = new List<SelectListItem>();
            foreach (var store in storesData)
            {
                StoresSelectList.Add(new SelectListItem { Text = store.Name ,Value = store.Id.ToString()});
            }
        }*/
    }

}


