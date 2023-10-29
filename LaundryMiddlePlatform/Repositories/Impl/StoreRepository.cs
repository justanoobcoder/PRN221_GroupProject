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
}
