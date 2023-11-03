using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impl;

public class ServiceRepository : IServiceRepository
{
    public List<Service> GetListServices(int? id)
    {
        return ServiceDAO.Instance.GetListServices(id);
    }
}
