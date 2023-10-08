using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects;

public class WashingMachine
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public decimal MaxWeight { get; set; }
    public bool IsAvailable { get; set; }
    public int StoreId { get; set; }
}
