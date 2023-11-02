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
    public IQueryable<Service> GetAll() => ServiceDAO.Instance.GetAll();
    public Service? GetById(int? id) => ServiceDAO.Instance.GetById(id);

}
