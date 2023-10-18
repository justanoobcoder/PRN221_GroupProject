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
}
