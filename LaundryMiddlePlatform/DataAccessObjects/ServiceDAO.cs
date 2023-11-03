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

    public List<Service> GetListServices(int? id)
    {
        var context = new LaundryMiddlePlatformDbContext();
        List<Service> services;

        if (id != null)
        {
            // Filter services by StoreId if id is not null
            services = context.Services.Where(s => s.StoreId == id.Value).ToList();
        }
        else
        {
            // If id is null, fetch all services
            services = context.Services.ToList();
        }

        return services;
    }
}
