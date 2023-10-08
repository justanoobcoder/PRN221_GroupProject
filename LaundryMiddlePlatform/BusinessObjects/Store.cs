using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects;

public class Store
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? LogoUrl { get; set; } = null!;
    public string? FacebookUrl { get; set; }
    public string Password { get; set; } = null!;
    public string OwnerName { get; set; } = null!;
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }
    public bool IsOpened { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsBanned { get; set; }
    public DateTime? DeletedAt { get; set; }
    public ICollection<WashingMachine> WashingMachines { get; set; } = new List<WashingMachine>();
    public ICollection<Service> Services { get; set; } = new List<Service>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
