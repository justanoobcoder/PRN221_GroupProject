using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impl;

public class StoreRepository : IStoreRepository
{
    public Store? GetByPhoneAndPassword(string phone, string password)
        => StoreDAO.Instance.GetByPhoneAndPassword(phone, password);

    public Store? GetById(int? id) => StoreDAO.Instance.GetById(id);
    public void Update(Store store) => StoreDAO.Instance.Update(store);
    public Store Create(Store store)=>StoreDAO.Instance.Create(store);
    public Store? GetByPhone(string phone)=>StoreDAO.Instance.GetByPhone(phone);    
    public Store? GetByEmail(string email)=>StoreDAO.Instance.GetByEmail(email);
    public async Task<List<Store>> GetListStores()
    {
        return await StoreDAO.Instance.GetListStoresAsync();
    }

    public async Task AddStore(Store s)
    {
        await StoreDAO.Instance.AddStoreAsync(s);
    }

    public async Task UpdateStore(Store store)
    {
        await StoreDAO.Instance.UpdateStoreAsync(store);
    }

    public async Task<Store> GetStoreById(int? id)
    {
        return await StoreDAO.Instance.GetStoreByIdAsync(id);
    }

    public async Task<bool> CheckIfStoreExist(int? id)
    {
        return await StoreDAO.Instance.CheckIfStoreExistAsync(id);
    }

    public async Task<IQueryable<Store>> GetListStoresIQ()
    {
        return await StoreDAO.Instance.GetListStoreIQAsync();
    }

    public IQueryable<Store> GetStores() => StoreDAO.Instance.GetStores();

    public Store? GetStoreFacebookUrl(string? fbUrl)
    {
        return  StoreDAO.Instance.GetStoreFacebookUrlString(fbUrl);
    }
}
