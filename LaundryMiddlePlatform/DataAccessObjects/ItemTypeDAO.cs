using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects;

public class ItemTypeDAO
{
    private static ItemTypeDAO? _instance = null;
    private static readonly object _lock = new();
    private ItemTypeDAO() { }
    public static ItemTypeDAO Instance
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
