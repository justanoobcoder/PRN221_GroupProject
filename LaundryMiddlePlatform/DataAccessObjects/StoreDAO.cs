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
    public Store? GetByPhone(string phone)
    {
        var context = new LaundryMiddlePlatformDbContext();
        return context.Stores.SingleOrDefault(s => s.Phone == phone);
    }

    public Store? GetByEmail(string email)
    {
        var context = new LaundryMiddlePlatformDbContext();
        return context.Stores.SingleOrDefault(s => s.Email == email);
    }


    public IQueryable<Store> GetAll()
    {
        var context = new LaundryMiddlePlatformDbContext();
        return context.Stores;
    }

    public Store? GetById(int? id)
    {
        var context = new LaundryMiddlePlatformDbContext();
        return context.Stores.Find(id);
    }

    public Store Create(Store store)
    {
        var context = new LaundryMiddlePlatformDbContext();
        store.Password = BCrypt.Net.BCrypt.HashPassword(store.Password);
        context.Stores.Add(store);
        context.SaveChanges();
        return store;
    }

    public void Update(Store store)
    {
        var context = new LaundryMiddlePlatformDbContext();
        var _store = context.Stores.Find(store.Id);
        if (_store != null)
        {
            context.Entry(_store).CurrentValues.SetValues(store);
        }
        context.SaveChanges();
    }

    public void Delete(Store store)
    {
        var context = new LaundryMiddlePlatformDbContext();
        var _store = context.Stores.Find(store.Id);
        if (_store != null)
        {
            context.Remove(store);
            context.SaveChanges();
        }
    }
}
