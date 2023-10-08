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
}
