using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects;

public class ServicePrice
{
    public int Id { get; set; }
    public decimal MinWeight { get; set; }
    public decimal MaxWeight { get; set; }
    public decimal? Price { get; set; }
    public decimal? PricePerUnit { get; set; }
    public int WashTimeInMinute { get; set; }
    public int ServiceId { get; set; }
}
