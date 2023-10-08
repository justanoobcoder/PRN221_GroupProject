using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ItemTypeId { get; set; }
    public int StoreId { get; set; }
    public ItemType ItemType { get; set; } = default!;
    public ICollection<ServicePrice> ServicePrices { get; set; } = new List<ServicePrice>();
}
