﻿using BusinessObjects;
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
}
