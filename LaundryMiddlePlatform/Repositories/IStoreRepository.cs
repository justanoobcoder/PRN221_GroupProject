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
    Task<List<Store>> GetListStores ();
    Task AddStore(Store store);
    Task UpdateStore(Store store);
    Task<Store> GetStoreById(int? id);
    Task<bool> CheckIfStoreExist(int? id);
}
