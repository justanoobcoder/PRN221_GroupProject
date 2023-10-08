using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects;

public class Order
{
    public string Id { get; set; } = null!;
    public int CustomerId { get; set; }
    public int StoreId { get; set; }
    public int ServicePriceId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public DateTime? TakenAt { get; set; }
    public decimal Weight { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = null!;
    public string? Note { get; set; }
    public Customer Customer { get; set; } = default!;
    public Store Store { get; set; } = default!;
    public ServicePrice ServicePrice { get; set; } = default!;
}
