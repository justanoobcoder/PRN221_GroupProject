using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects;

public class MachineOrderAssignment
{
    public int Id { get; set; }
    public int MachineId { get; set; }
    public string OrderId { get; set; } = null!;
    public DateTime AssignedStartAt { get; set; }
    public DateTime AssignedEndAt { get; set; }
    public Machine Machine { get; set; } = null!;
    public Order Order { get; set; } = null!;
}
