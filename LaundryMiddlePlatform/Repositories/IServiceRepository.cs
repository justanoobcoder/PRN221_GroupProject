using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;

public interface IServiceRepository
{
    public IEnumerable<Service> GetAllByStoreId(int? id);

    public Service Create(Service service);
    public void Update(Service service);
}
