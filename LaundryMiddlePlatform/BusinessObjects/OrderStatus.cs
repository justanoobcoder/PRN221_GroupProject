using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects;

public enum OrderStatus
{
    Denied, Pending, WaitingForPickUp, WaitingForWashing, Washing, Drying, WaitingForDropOff, Completed
}
