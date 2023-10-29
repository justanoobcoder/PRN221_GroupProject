using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects;

public class ServiceDAO
{
    private static ServiceDAO? _instance = null;
    private static readonly object _lock = new();
    private ServiceDAO() { }
    public static ServiceDAO Instance
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

    public IEnumerable<Service> GetAllByStoreId(int? id)
    {
        var context = new LaundryMiddlePlatformDbContext();
        return context.Services.Where(s=>s.StoreId.Equals(id)).ToList();
    }
}
