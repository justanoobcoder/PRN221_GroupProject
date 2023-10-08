using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects;

public class OrderDAO
{
    private static OrderDAO? _instance = null;
    private static readonly object _lock = new();

    private OrderDAO() { }
    public static OrderDAO Instance
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
}
