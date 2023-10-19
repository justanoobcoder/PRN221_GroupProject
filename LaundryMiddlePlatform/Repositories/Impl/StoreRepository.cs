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
}
