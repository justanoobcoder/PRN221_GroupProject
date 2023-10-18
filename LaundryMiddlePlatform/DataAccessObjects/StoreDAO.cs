using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects;

public class StoreDAO
{
    private static StoreDAO? _instance = null;
    private static readonly object _lock = new();
    private StoreDAO() { }
    public static StoreDAO Instance
    {
        get
        {
            lock (_lock)
            {
                _instance ??= new();
                return _instance;
            }
        }
    }

    public Store? GetByPhoneAndPassword(string phone, string password)
    {
        var context = new LaundryMiddlePlatformDbContext();
        var store = context.Stores.SingleOrDefault(s => s.Phone == phone);
        if (store != null)
        {
            if (BCrypt.Net.BCrypt.Verify(password, store.Password))
            {
                return store;
            }
        }
        return null;
    }
}
