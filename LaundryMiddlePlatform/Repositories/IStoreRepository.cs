using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public interface IStoreRepository
{
    Store? GetByPhoneAndPassword(string phone, string password);
    public Store? GetById(int? id);
    public void Update(Store store);
    public Store Create(Store store);
    public Store? GetByPhone(string phone);
    public Store? GetByEmail(string email);
    Task<List<Store>> GetListStores ();
    Task AddStore(Store store);
    Task UpdateStore(Store store);
    Task<Store> GetStoreById(int? id);
    Task<bool> CheckIfStoreExist(int? id);
    Task<IQueryable<Store>> GetListStoresIQ();
    Store? GetStoreFacebookUrl(string? fbUrl);
}
