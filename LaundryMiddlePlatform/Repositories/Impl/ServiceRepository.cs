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
    public IEnumerable<Service> GetAllByStoreId(int? id)=>ServiceDAO.Instance.GetAllByStoreId(id);
}
