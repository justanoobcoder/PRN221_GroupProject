using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impl
{
    public class MachineRepository : IMachineRepository
    {
        public Machine Create(Machine machine)=>MachineDAO.Instance.Create(machine);    

        public IEnumerable<Machine> GetAllByStoreId(int? id)=>MachineDAO.Instance.GetAllByStoreId(id);

        public Machine? GetById(int? id)=>MachineDAO.Instance.GetById(id);

        public Machine? GetByName(string? name)=>MachineDAO.Instance.GetByName(name);   

        public void Update(Machine machine)=>MachineDAO.Instance.Update(machine);
       
    }
}
