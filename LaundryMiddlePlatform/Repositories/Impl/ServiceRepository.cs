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
    public Service Create(Service service) => ServiceDAO.Instance.Create(service);
    public IEnumerable<Service> GetAllByStoreId(int? id) => ServiceDAO.Instance.GetAllByStoreId(id);
    public void Update(Service service) => ServiceDAO.Instance.Update(service);
    public Service? GetByName(string? name) => ServiceDAO.Instance.GetByName(name);

}
